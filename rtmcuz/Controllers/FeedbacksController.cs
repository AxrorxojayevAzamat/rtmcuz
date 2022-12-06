
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rtmcuz.Data;
using PagedList;

namespace rtmcuz.Controllers
{
    [Authorize]
    [Route("dashboard/{controller}/{action}")]
    public class FeedbacksController : Controller
    {
        private readonly RtmcUzContext _context;

        public FeedbacksController(RtmcUzContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var feedbacks = await _context.Feedback.Include(f => f.Department).ToListAsync();
            const int PAGE_SIZE = 30;
            ViewBag.PageSize = PAGE_SIZE;
            ViewBag.TotalItems = feedbacks.Count;
            ViewBag.CurrentPage = page;
            return View(feedbacks.ToPagedList(page ?? 1, PAGE_SIZE));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Feedback == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedback
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }
    }
}
