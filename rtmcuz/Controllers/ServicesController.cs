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
    public class ServicesController : Controller
    {
        private readonly ISectionRepository _sectionRepository;

        public ServicesController(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _sectionRepository.ListItemsAsync(SectionTypes.Service));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.Service))
            {
                return NotFound();
            }

            var service = await _sectionRepository.GetItemAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                _sectionRepository.Create(Section.FromService(service), image);
                return RedirectToAction(nameof(Index));
            }

            return View(service);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.Service))
            {
                return NotFound();
            }

            var service = await _sectionRepository.GetItemAsync(id);

            ViewData["Variants"] = _sectionRepository.VariantsList((int)service.GroupId);

            if (service == null)
            {
                return NotFound();
            }

            return View(Service.FromSection(service));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Service service, IFormFile? image)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _sectionRepository.Save(Section.FromService(service), image);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicesExists(service.Id))
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

            return View(service);
        }
        public async Task<IActionResult> Variant(int groupId, Locales langValue)
        {
            ViewData["Variants"] = _sectionRepository.VariantsList(groupId);

            return View(new Service() { GroupId = groupId, Lang = langValue });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Variant(int id, Service service, IFormFile image)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _sectionRepository.Save(Section.FromService(service), image);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicesExists(service.Id))
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

            return View(service);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.Service))
            {
                return NotFound();
            }

            var service = await _sectionRepository.GetItemAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_sectionRepository.IsNull(SectionTypes.Service))
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