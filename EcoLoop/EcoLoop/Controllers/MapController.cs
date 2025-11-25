using EcoLoop.Data;
using EcoLoop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLoop.Controllers
{
    public class MapController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MapController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var stores = await _context.Stores
                .Where(s => s.Latitude != 0 && s.Longitude != 0)
                .Select(s => new MapStoreViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Category = s.Category,
                    Latitude = s.Latitude,
                    Longitude = s.Longitude,
                    AcceptsOwnPackaging = s.AcceptsOwnPackaging,
                    Rating = s.Rating,
                    OpeningHours = s.OpeningHours

                })
                .ToListAsync();

            var model = new MapPageViewModel
            {
                Stores = stores
            };

            return View(model);
        }
    }
}
