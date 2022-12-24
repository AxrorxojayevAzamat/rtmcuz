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
    public class ReportsController : Controller
    {
        private readonly ISectionRepository _sectionRepository;

        public ReportsController(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var items = await _sectionRepository.ListItemsAsync(SectionTypes.Report);
            const int PAGE_SIZE = 10;
            ViewBag.PageSize = PAGE_SIZE;
            ViewBag.TotalItems = items.Count;
            ViewBag.CurrentPage = page;
            return View(items.ToPagedList(page ?? 1, PAGE_SIZE));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.Report))
            {
                return NotFound();
            }

            var report = await _sectionRepository.GetItemAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Report report)
        {
            if (ModelState.IsValid)
            {
                _sectionRepository.Create(Section.FromReport(report));
                return RedirectToAction(nameof(Index));
            }

            return View(report);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.Report))
            {
                return NotFound();
            }

            var report = await _sectionRepository.GetItemAsync(id);

            ViewData["Variants"] = _sectionRepository.VariantsList((int)report.GroupId);

            if (report == null)
            {
                return NotFound();
            }

            return View(Report.FromSection(report));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Report report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _sectionRepository.Save(Section.FromReport(report));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicesExists(report.Id))
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

            return View(report);
        }
        public async Task<IActionResult> Variant(int groupId, Locales langValue)
        {
            ViewData["Variants"] = _sectionRepository.VariantsList(groupId);

            return View(new Report() { GroupId = groupId, Lang = langValue });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Variant(int id, Report report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _sectionRepository.Save(Section.FromReport(report));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicesExists(report.Id))
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

            return View(report);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.Report))
            {
                return NotFound();
            }

            var report = await _sectionRepository.GetItemAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_sectionRepository.IsNull(SectionTypes.Report))
            {
                return Problem("Entity set 'RtmcUzContext.Services'  is null.");
            }

            await _sectionRepository.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ServicesExists(int id)
        {
            return _sectionRepository.Any(id);
        }
    }
}