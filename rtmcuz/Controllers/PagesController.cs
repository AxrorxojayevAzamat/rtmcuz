//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using rtmcuz.Data;
//using rtmcuz.Models;

//namespace rtmcuz.Controllers
//{
//    //[Route("dashboard/{controller}")]
//    public class PagesController : Controller
//    {
//        private readonly RtmcUzContext _context;

//        public PagesController(RtmcUzContext context)
//        {
//            _context = context;
//        }

//        [Route("{permalink}")]
//        public async Task<IActionResult> Show(string permalink)
//        {
//            //var page = await _context.Pages.FirstAsync(p => p.Permalink == permalink); 
//            string something = permalink;
//            ViewBag.Layout = "~/Views/Shared/_LayoutDashboard.cshtml";
//            return View("Index", await _context.Pages.Where(p => p.Permalink == something).ToListAsync());
//        }

//        public IActionResult Page()
//        {
//            return View();
//        }

//        // GET: Pages
//        //[Route("{permalink}")]
//        public async Task<IActionResult> Index(string permalink)
//        {
//            //var page = await _context.Pages.FirstAsync(p => p.Permalink == permalink); 
//            string something = permalink;
//            return View(await _context.Pages.Where(p => p.Permalink == something).ToListAsync());
//        }

//        // GET: Pages/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null || _context.Pages == null)
//            {
//                return NotFound();
//            }

//            var page = await _context.Pages
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (page == null)
//            {
//                return NotFound();
//            }

//            return View(page);
//        }

//        // GET: Pages/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Pages/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,Name,Slug,CreatedDate,UpdatedDate")] Page page)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(page);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(page);
//        }

//        // GET: Pages/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null || _context.Pages == null)
//            {
//                return NotFound();
//            }

//            var page = await _context.Pages.FindAsync(id);
//            if (page == null)
//            {
//                return NotFound();
//            }
//            return View(page);
//        }

//        // POST: Pages/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Slug,CreatedDate,UpdatedDate")] Page page)
//        {
//            if (id != page.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(page);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!PageExists(page.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(page);
//        }

//        // GET: Pages/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null || _context.Pages == null)
//            {
//                return NotFound();
//            }

//            var page = await _context.Pages
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (page == null)
//            {
//                return NotFound();
//            }

//            return View(page);
//        }

//        // POST: Pages/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            if (_context.Pages == null)
//            {
//                return Problem("Entity set 'RtmcUzContext.Pages'  is null.");
//            }
//            var page = await _context.Pages.FindAsync(id);
//            if (page != null)
//            {
//                _context.Pages.Remove(page);
//            }
            
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool PageExists(int id)
//        {
//          return _context.Pages.Any(e => e.Id == id);
//        }
//    }
//}
