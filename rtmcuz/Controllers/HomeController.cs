using Microsoft.AspNetCore.Mvc;
using rtmcuz.Data.Models;
using System.Diagnostics;
using rtmcuz.Data;
using rtmcuz.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using PagedList;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using rtmcuz.Extensions;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace rtmcuz.Controllers
{
    public class HomeController : Controller
    {
        private readonly RtmcUzContext _context;
        private readonly Locales _locale;
        private readonly IStringLocalizer<SlugResource> _localizer;
        private readonly ICompositeViewEngine _compositeViewEngine;
        const string DASHBOARD = "dashboard";
        const string SEARCH = "search";
        const string SHOW = "Show";
        const string NEWS = "news";

        public HomeController(RtmcUzContext context, IStringLocalizer<SlugResource> localizer, ICompositeViewEngine compositeViewEngine)
        {
            _context = context;
            _localizer = localizer;
            _compositeViewEngine = compositeViewEngine;
            _locale = (Locales)Enum.Parse(typeof(Locales), CultureInfo.CurrentCulture.Name.Replace('-', '_'));
        }


        public async Task<IActionResult> Index()
        {
            var interactiveServices = await QueryForSections(SectionTypes.Interactive).ToListAsync();
            var banners = await QueryForSections(SectionTypes.Banner).ToListAsync();
            var questions = await QueryForSections(SectionTypes.Question).ToListAsync();
            var services = await QueryForSections(SectionTypes.Service).ToListAsync();
            var news = await QueryForSections(SectionTypes.News)
                .OrderByDescending(n => n.CreatedDate).Take(4).ToListAsync();

            if (interactiveServices == null || banners == null || questions == null || services == null || news == null) return NotFound();

            ViewBag.InteractiveServices = interactiveServices;
            ViewBag.Banners = banners;
            ViewBag.Questions = questions;
            ViewBag.Services = services;
            ViewBag.News = news;

            return View();
        }

        [Route("/{slug}")]
        public IActionResult Section(string slug, int? page)
        {
            // if (slug.Contains(".css") || slug.Contains(".js")) return null;
            //var searchKey = GetLocaleKey(slug);

            SectionTypes? sectionType = GetSectionType(slug ?? "/", out string sectionName);
            if (sectionType == SectionTypes.Static)
            {
                var result = ExistsView(sectionName);
                if (!result.Success) return Redirect("/");

                return View(sectionName);
            }

            var query = QueryForSections((SectionTypes)sectionType).OrderByDescending(n => n.UpdatedDate);
            var items = query.ToList();
            if (slug == NEWS)
            {
                const int PAGE_SIZE = 5;
                var lastNews = query.Take(7).ToList();

                ViewBag.LastNews = lastNews;
                ViewBag.PageSize = PAGE_SIZE;
                ViewBag.TotalItems = items.Count;
                ViewBag.CurrentPage = page;
                return View(sectionName, items.ToPagedList(page ?? 1, PAGE_SIZE));
            }

            return View(sectionName, items);
        }

        [Route("/{slugGroup}/{slug}")]
        public IActionResult Show(string slugGroup, string slug)
        {
            var item = _context.Sections.Include(s => s.Image).Where(s => s.Slug.Equals(slug)).FirstOrDefault();
            if (item == null)
            {
                return Redirect("/");
            }

            if (item.Type == SectionTypes.News)
            {
                var lastNews = QueryForSections(SectionTypes.News).OrderByDescending(n => n.UpdatedDate).Take(7).ToList();
                ViewBag.LastNews = lastNews;
            }

            if (!item.Lang.Equals(_locale))
            {
                SetCookie(item.Lang.ToString().Replace("_", "-"));
                return Redirect($"/{slugGroup}/{item.Slug}");
            }

            string viewName = $"{item.Type}{SHOW}";

            var result = ExistsView(viewName);
            if (!result.Success) return Redirect("/");

            return View(viewName, item);
        }

        [Route("/search")]
        public IActionResult Search(string? searching, int? page)
        {
            if (searching == null)
            {
                return Redirect("/");
            }
            var dbQuery = _context.Sections.Where(s => s.Lang == _locale && (s.Type != SectionTypes.Banner && s.Type != SectionTypes.Question));
            var query = from s in dbQuery
                        where EF.Functions.Like(s.Title, $"%{searching}%")
                        select s;
            var items = query.ToList();

            const int PAGE_SIZE = 5;
            ViewBag.Searching = searching;
            ViewBag.PageSize = PAGE_SIZE;
            ViewBag.TotalItems = items.Count;
            ViewBag.CurrentPage = page;

            return View(items.ToPagedList(page ?? 1, PAGE_SIZE));
        }

        [Route("SetLanguage")]
        public IActionResult SetLanguage(string culture, string currentUrl)
        {
            string[] slugs = currentUrl.Split('?')[0].Split('/').Where((val, i) => i != 0).ToArray();
            SetCookie(culture);

            if (slugs[0] == DASHBOARD) return LocalRedirect(currentUrl);
            else if (slugs[0] == SEARCH) return LocalRedirect("/");

            //slugs[0] = GetLocaleKey(slugs[0]);

            string? queryParams = currentUrl.Split('?').Length > 1 ? $"?{currentUrl.Split('?')[1]}" : String.Empty;
            string returnUrl = $"~/{GetReturnUrl(slugs, culture)}{queryParams}";

            return LocalRedirect(returnUrl);
        }

        #region PRIVATE-METHODS
        private string GetReturnUrl(string[] urlArray, string culture)
        {
            if (urlArray.Length == 1)
            {
                return urlArray[0] ?? "";
            }
            else if (urlArray.Length > 1)
            {
                int groupId = (int)_context.Sections.Where(s => s.Slug == urlArray[urlArray.Length - 1]).FirstOrDefault().GroupId;
                Section? variant = _context.Sections
                    .Where(s => s.GroupId == groupId && s.Lang == (Locales)Enum.Parse(typeof(Locales), culture.Replace('-', '_')))
                    .FirstOrDefault();

                if (variant == null) return urlArray[0];

                urlArray[urlArray.Length - 1] = variant.Slug;
                return string.Join('/', urlArray);
            }
            return "/";
        }

        private SectionTypes GetSectionType(string slug, out string sectionName)
        {
            sectionName = string.Join(string.Empty, slug.Split("-").Select(s => s.FirstCharToUpper()).ToArray());
            string checkingSection = slug == NEWS ? sectionName : sectionName.Substring(0, sectionName.Length - 1);

            if (Enum.TryParse(checkingSection, out SectionTypes type))
            {
                return type;
            }

            return SectionTypes.Static;
        }

        public void SetCookie(string culture)
        {
            Response.Cookies.Append(
             CookieRequestCultureProvider.DefaultCookieName,
             CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
             new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
        }

        private ViewEngineResult ExistsView(string name)
        {
            var viewName = $"~/Views/Home/{name}.cshtml";
            var result = _compositeViewEngine.GetView("", viewName, false);
            return result;
        }

        private string? GetLocaleKey(string slug) => _localizer.GetAllStrings().FirstOrDefault(x => x.Value == slug)?.Name;
        private IQueryable<Section> QueryForSections(SectionTypes type) => _context.Sections.Include(b => b.Image).Where(s => s.Type == type && s.Lang == _locale);
        private IQueryable<Section> QueryForSection(int groupId) => _context.Sections.Include(b => b.Image).Where(s => s.GroupId == groupId && s.Lang == _locale);
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}