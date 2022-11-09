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

namespace rtmcuz.Controllers
{
    public class BannersController : Controller
    {
        private readonly RtmcUzContext _context;

        public BannersController(RtmcUzContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sections.Where(s => s.Type == SectionTypes.Banner).ToListAsync());
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
        public async Task<IActionResult> Create(Banner banner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Section.FromBanner(banner));
                await _context.SaveChangesAsync();
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

            var banner = await _context.Sections.FindAsync(id);
            if (banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Banner banner)
        {
            if (id != banner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Section.FromBanner(banner));
                    await _context.SaveChangesAsync();
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

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BannersExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
    }
}
