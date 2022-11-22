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
    public class NewsController : Controller
    {
        private readonly ISectionRepository _sectionRepository;

        public NewsController(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _sectionRepository.ListItemsAsync(SectionTypes.News));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.News))
            {
                return NotFound();
            }

            var news = await _sectionRepository.GetItemAsync(id);
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
        public async Task<IActionResult> Create(News news, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                _sectionRepository.Create(Section.FromNews(news), image);
                return RedirectToAction(nameof(Index));
            }

            return View(news);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.News))
            {
                return NotFound();
            }

            var news = await _sectionRepository.GetItemAsync(id);

            ViewData["Variants"] = _sectionRepository.VariantsList((int)news.GroupId);

            if (news == null)
            {
                return NotFound();
            }

            return View(News.FromSection(news));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, News news, IFormFile? image)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _sectionRepository.Save(Section.FromNews(news), image);
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
        public async Task<IActionResult> Variant(int groupId, Locales langValue)
        {
            ViewData["Variants"] = _sectionRepository.VariantsList(groupId);

            return View(new News() { GroupId = groupId, Lang = langValue });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Variant(int id, News news, IFormFile image)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _sectionRepository.Save(Section.FromNews(news), image);
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
            if (_sectionRepository.Exists(id, SectionTypes.News))
            {
                return NotFound();
            }

            var news = await _sectionRepository.GetItemAsync(id);
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
            if (_sectionRepository.IsNull(SectionTypes.News))
            {
                return Problem("Entity set 'RtmcUzContext.News'  is null.");
            }

            await _sectionRepository.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _sectionRepository.Any(id);
        }
    }
}