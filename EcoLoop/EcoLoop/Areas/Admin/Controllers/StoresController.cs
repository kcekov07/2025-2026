using EcoLoop.Data;
using EcoLoop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoLoop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pending = await _context.Stores
                .Where(s => !s.IsApproved)
                .OrderByDescending(s => s.CreatedAt)
                .Select(s => new AdminStoreListItemViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Category = s.Category,
                    Address = s.Address,
                    IsApproved = s.IsApproved,
                    Rating = s.Rating,
                    ReviewsCount = s.Reviews.Count,
                    CreatedAtShort = s.CreatedAt.ToString("dd.MM.yyyy")
                })
                .ToListAsync();

            var all = await _context.Stores
                .OrderByDescending(s => s.CreatedAt)
                .Select(s => new AdminStoreListItemViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Category = s.Category,
                    Address = s.Address,
                    IsApproved = s.IsApproved,
                    Rating = s.Rating,
                    ReviewsCount = s.Reviews.Count,
                    CreatedAtShort = s.CreatedAt.ToString("dd.MM.yyyy")
                })
                .ToListAsync();

            ViewBag.PendingStores = pending;
            ViewBag.AllStores = all;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) return NotFound();

            store.IsApproved = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) return NotFound();

            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
