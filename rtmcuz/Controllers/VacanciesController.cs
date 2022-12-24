using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rtmcuz.Data.Models;
using rtmcuz.Data.Enums;
using rtmcuz.ViewModels;
using rtmcuz.Interfaces;
using Microsoft.AspNetCore.Authorization;
using PagedList;

namespace rtmcuz.Controllers
{
    [Authorize]
    [Route("dashboard/{controller}/{action}")]
    public class VacanciesController : Controller
    {
        private readonly ISectionRepository _sectionRepository;

        public VacanciesController(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var items = await _sectionRepository.ListItemsAsync(SectionTypes.Vacancy);
            const int PAGE_SIZE = 10;
            ViewBag.PageSize = PAGE_SIZE;
            ViewBag.TotalItems = items.Count;
            ViewBag.CurrentPage = page;
            return View(items.ToPagedList(page ?? 1, PAGE_SIZE));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.Vacancy))
            {
                return NotFound();
            }

            var vacancy = await _sectionRepository.GetItemAsync(id);
            if (vacancy == null)
            {
                return NotFound();
            }

            return View(vacancy);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vacancy vacancy)
        {
            if (ModelState.IsValid)
            {
                _sectionRepository.Create(Section.FromVacancy(vacancy));
                return RedirectToAction(nameof(Index));
            }

            return View(vacancy);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.Vacancy))
            {
                return NotFound();
            }

            var vacancy = await _sectionRepository.GetItemAsync(id);

            ViewData["Variants"] = _sectionRepository.VariantsList((int)vacancy.GroupId);

            if (vacancy == null)
            {
                return NotFound();
            }

            return View(Vacancy.FromSection(vacancy));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vacancy vacancy)
        {
            if (id != vacancy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _sectionRepository.Save(Section.FromVacancy(vacancy));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentsExists(vacancy.Id))
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

            return View(vacancy);
        }
        public async Task<IActionResult> Variant(int groupId, Locales langValue)
        {
            ViewData["Variants"] = _sectionRepository.VariantsList(groupId);

            return View(new Vacancy() { GroupId = groupId, Lang = langValue });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Variant(int id, Vacancy vacancy)
        {
            if (id != vacancy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _sectionRepository.Save(Section.FromVacancy(vacancy));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentsExists(vacancy.Id))
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

            return View(vacancy);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.Vacancy))
            {
                return NotFound();
            }

            var vacancy = await _sectionRepository.GetItemAsync(id);
            if (vacancy == null)
            {
                return NotFound();
            }

            return View(vacancy);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_sectionRepository.IsNull(SectionTypes.Vacancy))
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