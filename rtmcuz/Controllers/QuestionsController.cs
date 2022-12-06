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
    public class QuestionsController : Controller
    {
        private readonly ISectionRepository _sectionRepository;

        public QuestionsController(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var items = await _sectionRepository.ListItemsAsync(SectionTypes.Question);
            const int PAGE_SIZE = 10;
            ViewBag.PageSize = PAGE_SIZE;
            ViewBag.TotalItems = items.Count;
            ViewBag.CurrentPage = page;
            return View(items.ToPagedList(page ?? 1, PAGE_SIZE));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.Question))
            {
                return NotFound();
            }

            var question = await _sectionRepository.GetItemAsync(id);
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
                _sectionRepository.Create(Section.FromQuestion(question));
                return RedirectToAction(nameof(Index));
            }

            return View(question);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (_sectionRepository.Exists(id, SectionTypes.Question))
            {
                return NotFound();
            }

            var question = await _sectionRepository.GetItemAsync(id);

            ViewData["Variants"] = _sectionRepository.VariantsList((int)question.GroupId);

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
                    _sectionRepository.Save(Section.FromQuestion(question));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionsExists(question.Id))
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
        public async Task<IActionResult> Variant(int groupId, Locales langValue)
        {
            ViewData["Variants"] = _sectionRepository.VariantsList(groupId);

            return View(new Question() { GroupId = groupId, Lang = langValue });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Variant(int id, Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _sectionRepository.Save(Section.FromQuestion(question));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionsExists(question.Id))
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
            if (_sectionRepository.Exists(id, SectionTypes.Question))
            {
                return NotFound();
            }

            var question = await _sectionRepository.GetItemAsync(id);
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
            if (_sectionRepository.IsNull(SectionTypes.Question))
            {
                return Problem("Entity set 'RtmcUzContext.Questions'  is null.");
            }

            await _sectionRepository.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionsExists(int id)
        {
            return _sectionRepository.Any(id);
        }
    }
}