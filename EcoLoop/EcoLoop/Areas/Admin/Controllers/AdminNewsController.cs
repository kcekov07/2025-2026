using EcoLoop.Data;
using EcoLoop.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoLoop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminNewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminNewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LIST
        public async Task<IActionResult> Index()
        {
            var news = await _context.News
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return View(news);
        }

        // GET: ADD
        [HttpGet]
        public IActionResult Add()
        {
            return View(new News());
        }

        // POST: ADD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(News model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.CreatedAt = DateTime.UtcNow;

            _context.News.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        // GET: EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var n = await _context.News.FindAsync(id);
            if (n == null) return NotFound();

            return View(n);
        }

        // POST: EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(News model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.News.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // POST: DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var n = await _context.News.FindAsync(id);
            if (n != null)
            {
                _context.News.Remove(n);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
