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

namespace rtmcuz.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly RtmcUzContext _context;

        public DepartmentsController(RtmcUzContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sections.Where(s => s.Type == SectionTypes.Department).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.Department) == null)
            {
                return NotFound();
            }

            var department = await _context.Sections.FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Section.FromDepartment(department));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.Department) == null)
            {
                return NotFound();
            }

            var department = await _context.Sections.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Section.FromDepartment(department));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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
            return View(department);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sections.Where(s => s.Type == SectionTypes.Department) == null)
            {
                return NotFound();
            }

            var department = await _context.Sections.FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sections.Where(s => s.Type == SectionTypes.Department) == null)
            {
                return Problem("Entity set 'RtmcUzContext.Department'  is null.");
            }
            var department = await _context.Sections.FindAsync(id);
            if (department != null)
            {
                _context.Sections.Remove(department);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
    }
}
