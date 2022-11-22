using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rtmcuz.Data.Models;
using rtmcuz.Data.Enums;
using rtmcuz.ViewModels;
using rtmcuz.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace rtmcuz.Controllers
{
    [Authorize]
    [Route("dashboard/{controller}/{action}")]
    public class DepartmentsController : Controller
    {
        private readonly ISectionRepository _sectionRepository;

        public DepartmentsController(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _sectionRepository.ListItemsAsync(SectionTypes.Department));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.Department))
            {
                return NotFound();
            }

            var deparment = await _sectionRepository.GetItemAsync(id);
            if (deparment == null)
            {
                return NotFound();
            }

            return View(deparment);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department deparment)
        {
            if (ModelState.IsValid)
            {
                _sectionRepository.Create(Section.FromDepartment(deparment));
                return RedirectToAction(nameof(Index));
            }

            return View(deparment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.Department))
            {
                return NotFound();
            }

            var deparment = await _sectionRepository.GetItemAsync(id);

            ViewData["Variants"] = _sectionRepository.VariantsList((int)deparment.GroupId);

            if (deparment == null)
            {
                return NotFound();
            }

            return View(Department.FromSection(deparment));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department deparment)
        {
            if (id != deparment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _sectionRepository.Save(Section.FromDepartment(deparment));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentsExists(deparment.Id))
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

            return View(deparment);
        }
        public async Task<IActionResult> Variant(int groupId, Locales langValue)
        {
            ViewData["Variants"] = _sectionRepository.VariantsList(groupId);

            return View(new Department() { GroupId = groupId, Lang = langValue });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Variant(int id, Department deparment)
        {
            if (id != deparment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _sectionRepository.Save(Section.FromDepartment(deparment));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentsExists(deparment.Id))
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

            return View(deparment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.Department))
            {
                return NotFound();
            }

            var deparment = await _sectionRepository.GetItemAsync(id);
            if (deparment == null)
            {
                return NotFound();
            }

            return View(deparment);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_sectionRepository.IsNull(SectionTypes.Department))
            {
                return Problem("Entity set 'RtmcUzContext.Departments'  is null.");
            }

            await _sectionRepository.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentsExists(int id)
        {
            return _sectionRepository.Any(id);
        }
    }
}