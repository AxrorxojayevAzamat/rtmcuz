using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [Route("dashboard/{controller}/{action}")]
    public class ServicesController : Controller
    {
        private readonly RtmcUzContext _context;
        private readonly IAttachmentService _attachmentService;

        public ServicesController(RtmcUzContext context, IAttachmentService attachmentService)
        {
            _context = context;
            _attachmentService = attachmentService;
        }

        public async Task<IActionResult> Index()
        {
            var rtmcUzContext = await _context.Sections.Include(s => s.Image).Where(s => s.Type == SectionTypes.Service).ToListAsync();
            return View(rtmcUzContext);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.Service) == null)
            {
                return NotFound();
            }

            var service = await _context.Sections.Include(s => s.Image).FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service, IFormFile image)
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
                    service.ImageId = imageId;
                }

                _context.Add(Section.FromService(service));
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.Service) == null)
            {
                return NotFound();
            }

            var service = await _context.Sections.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(Service.FromSection(service));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Service service, IFormFile image)
        {
            if (id != service.Id)
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
                        service.ImageId = imageId;
                    }

                    _context.Update(Section.FromService(service));
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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
            return View(service);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.Service) == null)
            {
                return NotFound();
            }

            var service = await _context.Sections.Include(s => s.Image).FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sections.Where(s => s.Type == SectionTypes.Service) == null)
            {
                return Problem("Entity set 'RtmcUzContext.Service'  is null.");
            }
            var service = await _context.Sections.FindAsync(id);
            if (service != null)
            {
                _context.Sections.Remove(service);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
    }
}
