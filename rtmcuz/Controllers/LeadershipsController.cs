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
    public class LeadershipsController : Controller
    {
        private readonly ISectionRepository _sectionRepository;

        public LeadershipsController(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _sectionRepository.ListItemsAsync(SectionTypes.Leadership));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.Leadership))
            {
                return NotFound();
            }

            var leadership = await _sectionRepository.GetItemAsync(id);
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
        public async Task<IActionResult> Create(Leadership leadership, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                _sectionRepository.Create(Section.FromLeadership(leadership), image);
                return RedirectToAction(nameof(Index));
            }

            return View(leadership);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.Leadership))
            {
                return NotFound();
            }

            var leadership = await _sectionRepository.GetItemAsync(id);

            ViewData["Variants"] = _sectionRepository.VariantsList((int)leadership.GroupId);

            if (leadership == null)
            {
                return NotFound();
            }

            return View(Leadership.FromSection(leadership));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Leadership leadership, IFormFile image)
        {
            if (id != leadership.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _sectionRepository.Save(Section.FromLeadership(leadership), image);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeadershipsExists(leadership.Id))
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
        public async Task<IActionResult> Variant(int groupId, Locales langValue)
        {
            ViewData["Variants"] = _sectionRepository.VariantsList(groupId);

            return View(new Leadership() { GroupId = groupId, Lang = langValue });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Variant(int id, Leadership leadership, IFormFile image)
        {
            if (id != leadership.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _sectionRepository.Save(Section.FromLeadership(leadership), image);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeadershipsExists(leadership.Id))
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
            if (_sectionRepository.Exists(id, SectionTypes.Leadership))
            {
                return NotFound();
            }

            var leadership = await _sectionRepository.GetItemAsync(id);
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
            if (_sectionRepository.IsNull(SectionTypes.Leadership))
            {
                return Problem("Entity set 'RtmcUzContext.Leaderships'  is null.");
            }

            await _sectionRepository.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }

        private bool LeadershipsExists(int id)
        {
            return _sectionRepository.Any(id);
        }
    }
}