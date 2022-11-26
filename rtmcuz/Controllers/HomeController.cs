using Microsoft.AspNetCore.Mvc;
using rtmcuz.Data.Models;
using System.Diagnostics;
using rtmcuz.Data;
using rtmcuz.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using PagedList;
using Microsoft.Extensions.Localization;

namespace rtmcuz.Controllers
{
    // [Route("{culture}")]
    public class HomeController : Controller
    {
        private readonly RtmcUzContext _context;
        private readonly Locales _locale;
        private readonly IStringLocalizer<rtmcuz.Resources.Controllers.HomeController> _localizer;

        public HomeController(RtmcUzContext context, IStringLocalizer<rtmcuz.Resources.Controllers.HomeController> localizer)
        {
            _context = context;
            _localizer = localizer;
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

        // [Route("o-nas")]
        // [Route("biz-haqimizda")]
        public IActionResult AboutCenter()
        {
            return View();
        }

        //[Route("{slug}")]
        public IActionResult News(int? page)
        {
            const int PAGE_SIZE = 5;

            var query = QueryForSections(SectionTypes.News).OrderByDescending(n => n.UpdatedDate);
            var news = query.ToList();
            var lastNews = query.Take(7).ToList();

            ViewBag.PageSize = PAGE_SIZE;
            ViewBag.LastNews = lastNews;
            ViewBag.TotalItems = news.Count;
            ViewBag.CurrentPage = page;

            return View(news.ToPagedList(page ?? 1, PAGE_SIZE));
        }

        public IActionResult NewsShow(int? id)
        {
            var news = _context.Sections.Include(s => s.Image).Where(s => s.Id == id).First();
            var query = QueryForSections(SectionTypes.News).OrderByDescending(n => n.UpdatedDate);
            var lastNews = query.Take(7).ToList();
            ViewBag.LastNews = lastNews;

            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }
        public IActionResult Documents()
        {
            return View();
        }

        public IActionResult Feedback()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public JsonResult UploadCKEditorImage()
        {
            var files = Request.Form.Files;
            if (files.Count == 0)
            {
                var rError = new
                {
                    uploaded = false,
                    url = string.Empty
                };
                return Json(rError);
            }

            var formFile = files[0];
            var upFileName = formFile.FileName;
            // size, format check....
            var fileName = Guid.NewGuid() + Path.GetExtension(upFileName);
            var saveDir = @".\wwwroot\upload\";
            var savePath = saveDir + fileName;
            var previewPath = "/upload/" + fileName;

            bool result = true;
            try
            {
                if (!Directory.Exists(saveDir))
                {
                    Directory.CreateDirectory(saveDir);
                }

                using (FileStream fs = System.IO.File.Create(savePath))
                {
                    formFile.CopyTo(fs);
                    fs.Flush();
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            var rUpload = new
            {
                uploaded = result,
                url = result ? previewPath : string.Empty
            };
            return Json(rUpload);
        }

        private IQueryable<Section> QueryForSections(SectionTypes type) => _context.Sections.Include(b => b.Image).Where(s => s.Type == type && s.Lang == _locale);
    }
}