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
    public class InteractivesController : Controller
    {
        private readonly ISectionRepository _sectionRepository;

        public InteractivesController(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var items = await _sectionRepository.ListItemsAsync(SectionTypes.Interactive);
            const int PAGE_SIZE = 10;
            ViewBag.PageSize = PAGE_SIZE;
            ViewBag.TotalItems = items.Count;
            ViewBag.CurrentPage = page;
            return View(items.ToPagedList(page ?? 1, PAGE_SIZE));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.Interactive))
            {
                return NotFound();
            }

            var interactive = await _sectionRepository.GetItemAsync(id);
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
                _sectionRepository.Create(Section.FromInteractive(interactive));
                return RedirectToAction(nameof(Index));
            }

            return View(interactive);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.Interactive))
            {
                return NotFound();
            }

            var interactive = await _sectionRepository.GetItemAsync(id);

            ViewData["Variants"] = _sectionRepository.VariantsList((int)interactive.GroupId);

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
                    _sectionRepository.Save(Section.FromInteractive(interactive));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InteractivesExists(interactive.Id))
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
        public async Task<IActionResult> Variant(int groupId, Locales langValue)
        {
            ViewData["Variants"] = _sectionRepository.VariantsList(groupId);

            return View(new Interactive() { GroupId = groupId, Lang = langValue });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Variant(int id, Interactive interactive)
        {
            if (id != interactive.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _sectionRepository.Save(Section.FromInteractive(interactive));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InteractivesExists(interactive.Id))
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
            if (_sectionRepository.Exists(id, SectionTypes.Interactive))
            {
                return NotFound();
            }

            var interactive = await _sectionRepository.GetItemAsync(id);
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
            if (_sectionRepository.IsNull(SectionTypes.Interactive))
            {
                return Problem("Entity set 'RtmcUzContext.Interactives'  is null.");
            }

            await _sectionRepository.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }

        private bool InteractivesExists(int id)
        {
            return _sectionRepository.Any(id);
        }
    }
}