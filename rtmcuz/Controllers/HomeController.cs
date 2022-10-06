using Microsoft.AspNetCore.Mvc;
using rtmcuz.Models;
using System.Diagnostics;
using rtmcuz.Data;

namespace rtmcuz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public List<MenuItem> TodoItems = new()
        {
            new MenuItem() { Id = 0, Name = "Ab done", Slug = "ab-done" },
            new MenuItem() { Id = 1, Name = "Cd as", Slug = "cd-as" },
            new MenuItem() { Id = 2, Name = "Ef like", Slug = "ef-like" },
        };

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("{controller}/{action}/{slug}")]
        public IActionResult Privacy(string slug)
        {
            MenuItem item = new();
            foreach (var ti in TodoItems)
            {
                if (ti.Slug == slug) item = ti;

            }

            ViewBag.Slug = slug;
            return View(item);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}