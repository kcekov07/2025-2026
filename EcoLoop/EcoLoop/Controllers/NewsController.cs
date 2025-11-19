using EcoLoop.Data;
using EcoLoop.Data.Models;
using EcoLoop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoLoop.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext context;

        public NewsController(ApplicationDbContext _context)
        {
            context = _context;
        }

        // /News/All
        public async Task<IActionResult> All()
        {
            var news = await context.News
                .OrderByDescending(n => n.CreatedOn)
                .Select(n => new NewsViewModel
                {
                    Id = n.Id,
                    Title = n.Title,
                    ShortDescription = n.ShortDescription,
                    ImageUrl = n.ImageUrl,
                    CreatedOn = n.CreatedOn.ToString("dd.MM.yyyy")
                })
                .ToListAsync();

            return View(news);
        }

        // /News/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var n = await context.News.FindAsync(id);
            if (n == null) return NotFound();

            var model = new NewsDetailsViewModel
            {
                Id = n.Id,
                Title = n.Title,
                ShortDescription = n.ShortDescription,
                FullText = n.FullText,
                ImageUrl = n.ImageUrl,
                CreatedOn = n.CreatedOn.ToString("dd.MM.yyyy")
            };

            return View(model);
        }

        // /News/Add
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(NewsAddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var news = new News
            {
                Title = model.Title,
                ShortDescription = model.ShortDescription,
                FullText = model.FullText,
                ImageUrl = model.ImageUrl
            };

            context.News.Add(news);
            await context.SaveChangesAsync();

            return RedirectToAction("All");
        }
    }
}
