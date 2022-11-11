using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rtmcuz.Data;
using rtmcuz.Data.Enums;
using rtmcuz.Data.Models;
using rtmcuz.Interfaces;
using rtmcuz.ViewModels;

namespace rtmcuz.Controllers
{
    public class LeadershipsController : Controller
    {
        private readonly RtmcUzContext _context;
        private readonly IAttachmentService _attachmentService;

        public LeadershipsController(RtmcUzContext context, IAttachmentService attachmentService)
        {
            _context = context;
            _attachmentService = attachmentService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sections.Include(s => s.Image).Where(s => s.Type == SectionTypes.Leadership).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.Leadership) == null)
            {
                return NotFound();
            }

            var leadership = await _context.Sections.FirstOrDefaultAsync(m => m.Id == id);
            if (leadership == null)
            {
                return NotFound();
            }

            return View(leadership);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Leadership leadership, IFormFile image)
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
                    leadership.ImageId = imageId;
                }

                _context.Add(Section.FromLeadership(leadership));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(leadership);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.Leadership) == null)
            {
                return NotFound();
            }

            var leadership = await _context.Sections.Include(s => s.Image)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
            if (leadership == null)
            {
                return NotFound();
            }

            return View(Leadership.FromSection(leadership));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Leadership leadership, IFormFile image)
        {
            if (id != leadership.Id)
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
                        leadership.ImageId = imageId;
                    }

                    _context.Update(Section.FromLeadership(leadership));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeadershipExists(leadership.Id))
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

            return View(leadership);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.Leadership) == null)
            {
                return NotFound();
            }

            var leadership = await _context.Sections.FirstOrDefaultAsync(m => m.Id == id);
            if (leadership == null)
            {
                return NotFound();
            }

            return View(leadership);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sections.Where(s => s.Type == SectionTypes.Leadership) == null)
            {
                return Problem("Entity set 'RtmcUzContext.Leadership'  is null.");
            }

            var leadership = await _context.Sections.FindAsync(id);
            if (leadership != null)
            {
                _context.Sections.Remove(leadership);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeadershipExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
    }
}