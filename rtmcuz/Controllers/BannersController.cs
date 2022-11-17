using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rtmcuz.Data;
using rtmcuz.Data.Models;
using rtmcuz.Data.Enums;
using rtmcuz.ViewModels;
using rtmcuz.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace rtmcuz.Controllers
{
    public class BannersController : Controller
    {
        private readonly RtmcUzContext _context;
        private readonly IAttachmentService _attachmentService;

        public BannersController(RtmcUzContext context, IAttachmentService attachmentService)
        {
            _context = context;
            _attachmentService = attachmentService;
        }
    [Authorize]

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sections.Include(s => s.Image).Where(s => s.Type == SectionTypes.Banner).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.Banner) == null)
            {
                return NotFound();
            }

            var banner = await _context.Sections.FirstOrDefaultAsync(m => m.Id == id);
            if (banner == null)
            {
                return NotFound();
            }

            return View(banner);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Banner banner, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                int imageId = -1;

                if (image != null)
                {
                    imageId = _attachmentService.UploadFileToStorage(image);
                }

                if (imageId > -1)
                {
                    banner.ImageId = imageId;
                }

                _context.Add(Section.FromBanner(banner));
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(banner);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.Banner) == null)
            {
                return NotFound();
            }

            var banner = await _context.Sections.Include(s => s.Image)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if (banner == null)
            {
                return NotFound();
            }

            return View(Banner.FromSection(banner));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Banner banner, IFormFile image)
        {
            if (id != banner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    int imageId = -1;

                    if (image != null)
                    {
                        imageId = _attachmentService.UploadFileToStorage(image);
                    }

                    if (imageId > -1)
                    {
                        banner.ImageId = imageId;
                    }

                    _context.Update(Section.FromBanner(banner));
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannersExists(banner.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(banner);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.Banner) == null)
            {
                return NotFound();
            }

            var banner = await _context.Sections.FirstOrDefaultAsync(m => m.Id == id);
            if (banner == null)
            {
                return NotFound();
            }

            return View(banner);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sections.Where(s => s.Type == SectionTypes.Banner) == null)
            {
                return Problem("Entity set 'RtmcUzContext.Banners'  is null.");
            }

            var banner = await _context.Sections.FindAsync(id);
            if (banner != null)
            {
                _context.Sections.Remove(banner);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool BannersExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
    }
}