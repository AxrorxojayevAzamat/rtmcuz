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
using rtmcuz.ViewModels;
using rtmcuz.Infrastructure.Exceptions;
using SlugGenerator;
using Microsoft.AspNetCore.Authorization;

namespace rtmcuz.Controllers
{
    [Authorize]
    [Route("dashboard/{controller}/{action}")]
    public class InteractivesController : Controller
    {
        private readonly RtmcUzContext _context;

        public InteractivesController(RtmcUzContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sections.Where(s => s.Type == SectionTypes.InterActive).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.InterActive) == null)
            {
                return NotFound();
            }

            var interactive = await _context.Sections.FirstOrDefaultAsync(m => m.Id == id);
            if (interactive == null)
            {
                return NotFound();
            }

            return View(interactive);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Interactive interactive)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Section.FromInteractive(interactive));

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(interactive);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sections == null)
            {
                return NotFound();
            }

            var interactive = await _context.Sections.FindAsync(id);
            if (interactive == null)
            {
                return NotFound();
            }

            return View(Interactive.FromSection(interactive));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Interactive interactive)
        {
            if (id != interactive.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Section.FromInteractive(interactive));
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InteractiveExists(interactive.Id))
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

            return View(interactive);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.InterActive) == null)
            {
                return NotFound();
            }

            var interactive = await _context.Sections.FirstOrDefaultAsync(m => m.Id == id);
            if (interactive == null)
            {
                return NotFound();
            }

            return View(interactive);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sections.Where(s => s.Type == SectionTypes.InterActive) == null)
            {
                return Problem("Entity set 'RtmcUzContext.Interactive'  is null.");
            }

            var interactive = await _context.Sections.FindAsync(id);
            if (interactive != null)
            {
                _context.Sections.Remove(interactive);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool InteractiveExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
    }
}