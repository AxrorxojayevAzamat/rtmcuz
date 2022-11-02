using Microsoft.AspNetCore.Mvc;
using rtmcuz.Data.Models;
using System.Diagnostics;
using rtmcuz.Data;
using rtmcuz.Data.Enums;
using rtmcuz.Resources;

namespace rtmcuz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RtmcUzContext _context;
        private readonly LocalizationService _localizationService;

        //public List<MenuItem> TodoItems = new()
        //{
        //    new MenuItem() { Id = 0, Name = "Ab done", Slug = "ab-done" },
        //    new MenuItem() { Id = 1, Name = "Cd as", Slug = "cd-as" },
        //    new MenuItem() { Id = 2, Name = "Ef like", Slug = "ef-like" },
        //};

        public HomeController(ILogger<HomeController> logger, RtmcUzContext context, LocalizationService localizationService)
        {
            _logger = logger;
            _context = context;
            _localizationService = localizationService;
        }

        public IActionResult Index()
        {
            //var pages = _context.Pages.ToList();
            var center = _localizationService.GetLocalizedHtmlString("Center");
            var interactiveServices = _context.Sections.Where(s => s.Type == SectionTypes.InterActive).ToList();
            ViewBag.InteractiveServices = interactiveServices;
            return View();
        }

        [Route("{controller}/{action}/{slug}")]
        public IActionResult Privacy(string slug)
        {
            //MenuItem item = new();
            //foreach (var ti in TodoItems)
            //{
            //    if (ti.Slug == slug) item = ti;

            //}

            //ViewBag.Slug = slug;
            return View();
        }

        public IActionResult AboutCenter()
        {
            return View();
        }
        public IActionResult News()
        {
            return View();
        }
        public IActionResult Documents()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}