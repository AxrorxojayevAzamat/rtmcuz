﻿using Microsoft.AspNetCore.Mvc;
using rtmcuz.Data.Models;
using System.Diagnostics;
using rtmcuz.Data;
using rtmcuz.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace rtmcuz.Controllers
{
    public class HomeController : Controller
    {
        private readonly RtmcUzContext _context;
        private readonly Locales _locale;


        public HomeController(RtmcUzContext context)
        {
            _context = context;
            _locale = (Locales)Enum.Parse(typeof(Locales), CultureInfo.CurrentCulture.Name.Replace('-', '_'));
        }

        public async Task<IActionResult> Index()
        {
            var interactiveServices = await _context.Sections.Where(s => s.Type == SectionTypes.Interactive && s.Lang == _locale).ToListAsync();
            var banners = await _context.Sections.Include(b => b.Image).Where(s => s.Type == SectionTypes.Banner && s.Lang == _locale).ToListAsync();
            var questions = await _context.Sections.Where(s => s.Type == SectionTypes.Question && s.Lang == _locale).ToListAsync();
            var services = await _context.Sections.Include(b => b.Image).Where(s => s.Type == SectionTypes.Service && s.Lang == _locale).ToListAsync();
            var news = await _context.Sections.Include(b => b.Image).Where(s => s.Type == SectionTypes.News && s.Lang == _locale)
                .OrderByDescending(n => n.CreatedDate).Take(4).ToListAsync();

            if (interactiveServices == null || banners == null || questions == null || services == null || news == null) return NotFound();

            ViewBag.InteractiveServices = interactiveServices;
            ViewBag.Banners = banners;
            ViewBag.Questions = questions;
            ViewBag.Services = services;
            ViewBag.News = news;

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