using EcoLoop.Data;
using EcoLoop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoLoop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var stores = await _context.Stores
            .Where(s => s.IsApproved)
            .OrderByDescending(s => s.Id)
            .Take(4)

                .Select(s => new StoreCardViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Category = s.Category,
                    ImageUrl = s.ImageUrl,
                    Rating = 0
                })
                .ToListAsync();

            var model = new HomeViewModel
            {
                NearbyStores = stores,
                StoresCount = await _context.Stores.CountAsync(),
                ReviewsCount = 0,
                EventsCount = 0
            };

            return View(model);
        }
    }
}
