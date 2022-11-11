﻿using System;
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

namespace rtmcuz.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly RtmcUzContext _context;

        public QuestionsController(RtmcUzContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sections.Where(s => s.Type == SectionTypes.Question).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.Question) == null)
            {
                return NotFound();
            }

            var question = await _context.Sections.FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Question question)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Section.FromQuestion(question));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(question);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.Question) == null)
            {
                return NotFound();
            }

            var question = await _context.Sections.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            return View(Question.FromSection(question));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Section.FromQuestion(question));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
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

            return View(question);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.Question) == null)
            {
                return NotFound();
            }

            var question = await _context.Sections.FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sections.Where(s => s.Type == SectionTypes.Question) == null)
            {
                return Problem("Entity set 'RtmcUzContext.Question'  is null.");
            }

            var question = await _context.Sections.FindAsync(id);
            if (question != null)
            {
                _context.Sections.Remove(question);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
    }
}