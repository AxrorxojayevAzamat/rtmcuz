using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rtmcuz.Data;
using rtmcuz.Data.Enums;
using rtmcuz.FormModels;
using rtmcuz.Models;
using SlugGenerator;
namespace rtmcuz.Controllers
{
    public class InteractivesController : Controller
    {
        private readonly RtmcUzContext _context;

        public InteractivesController(RtmcUzContext context)
        {
            _context = context;
        }

        // GET: Interactives
        //_context.Sections.Where(s => s.Type == SectionTypes.InterActive)
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sections.Where(s => s.Type == SectionTypes.InterActive).ToListAsync());
        }

        // GET: Interactives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.InterActive) == null)
            {
                return NotFound();
            }

            var interactive = await _context.Sections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interactive == null)
            {
                return NotFound();
            }

            return View(interactive);
        }

        // GET: Interactives/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Interactives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Subtitle,Icon")] Interactive interactive)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Section.FromInteractive(interactive));
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(interactive);
        }

        // GET: Interactives/Edit/5
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
            return View(interactive);
        }

        // POST: Interactives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Subtitle,Icon")] Interactive interactive)
        {
            if (id != interactive.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interactive);
                    await _context.SaveChangesAsync();
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

        // GET: Interactives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sections == null)
            {
                return NotFound();
            }

            var interactive = await _context.Sections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interactive == null)
            {
                return NotFound();
            }

            return View(interactive);
        }

        // POST: Interactives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sections == null)
            {
                return Problem("Entity set 'RtmcUzContext.Interactive'  is null.");
            }
            var interactive = await _context.Sections.FindAsync(id);
            if (interactive != null)
            {
                _context.Sections.Remove(interactive);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InteractiveExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
    }
}
