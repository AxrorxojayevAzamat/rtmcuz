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
    [Route("dashboard/{controller}/{action}")]

    public class NewsController : Controller
    {
        private readonly RtmcUzContext _context;

        public NewsController(RtmcUzContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sections.Where(s => s.Type == SectionTypes.News).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.News) == null)
            {
                return NotFound();
            }

            var news = await _context.Sections.FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(News news)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Section.FromNews(news));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.News) == null)
            {
                return NotFound();
            }

            var news = await _context.Sections.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(News.FromSection(news));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, News news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Section.FromNews(news));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
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
            return View(news);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.News) == null)
            {
                return NotFound();
            }

            var news = await _context.Sections.FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sections.Where(s => s.Type == SectionTypes.News) == null)
            {
                return Problem("Entity set 'RtmcUzContext.News'  is null.");
            }
            var news = await _context.Sections.FindAsync(id);
            if (news != null)
            {
                _context.Sections.Remove(news);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
    }
}
