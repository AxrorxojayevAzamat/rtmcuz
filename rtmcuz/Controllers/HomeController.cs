using Microsoft.AspNetCore.Mvc;
using rtmcuz.Data.Models;
using System.Diagnostics;
using rtmcuz.Data;
using rtmcuz.Data.Enums;
using rtmcuz.Resources;
using Microsoft.EntityFrameworkCore;

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

        public HomeController(ILogger<HomeController> logger, RtmcUzContext context,
            LocalizationService localizationService)
        {
            _logger = logger;
            _context = context;
            _localizationService = localizationService;
        }

        public async Task<IActionResult> Index()
        {
            //var pages = _context.Pages.ToList();
            var center = _localizationService.GetLocalizedHtmlString("Center");
            var interactiveServices = await _context.Sections.Where(s => s.Type == SectionTypes.InterActive).ToListAsync();
            var banners = await _context.Sections.Include(b => b.Image).Where(s => s.Type == SectionTypes.Banner).ToListAsync();
            if (interactiveServices == null)
                return NotFound();
            ViewBag.InteractiveServices = interactiveServices;
            ViewBag.Banners = banners;
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
    }
}