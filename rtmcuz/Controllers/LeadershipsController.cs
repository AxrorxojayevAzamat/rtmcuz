
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
using rtmcuz.FormModels;

namespace rtmcuz.Controllers
{
    public class LeadershipsController : Controller
    {
        private readonly RtmcUzContext _context;

        public LeadershipsController(RtmcUzContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sections.Where(s => s.Type == SectionTypes.Leadership).ToListAsync());
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
        public async Task<IActionResult> Create(Leadership leadership)
        {
            if (ModelState.IsValid)
            {
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

            var leadership = await _context.Sections.FindAsync(id);
            if (leadership == null)
            {
                return NotFound();
            }
            return View(leadership);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Leadership leadership)
        {
            if (id != leadership.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
