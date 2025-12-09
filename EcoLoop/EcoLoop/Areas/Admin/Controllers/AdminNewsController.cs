using EcoLoop.Data;
using EcoLoop.Data.Models;
using EcoLoop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
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
            var news = await _context.MyNews
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return View(news);
        }

        // GET: ADD
        [HttpGet]
        public IActionResult Add()
        {
            return View(new NewsViewModel());
        }

        // POST: ADD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(NewsViewModel model)
        {
            var category = new string []{ "Еко бизнес", "Общество", "Съвети", "Законодателство", "Локални" };


            News news = new News
            {
                Title = model.Title,
                Summary = model.Summary,
                Content = model.Content,
                ImageUrl = model.ImageUrl,
                CreatedAt = DateTime.UtcNow,
                Category = category[model.CategoryId],

            };
            _context.MyNews.Add(news);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        // GET: EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var n = await _context.MyNews.FindAsync(id);
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

            _context.MyNews.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // POST: DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var n = await _context.MyNews.FindAsync(id);
            if (n != null)
            {
                _context.MyNews.Remove(n);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
