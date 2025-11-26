using EcoLoop.Data;
using EcoLoop.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLoop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Най-активни потребители (по StoresAdded + Reviews + EventsJoined)
            var topUsers = await _context.Users
                .OrderByDescending(u => u.StoresAdded + u.StoresVisited + u.EventsJoined)
                .Take(5)
                .ToListAsync();

            // Най-посещавани магазини (по Reviews count)
            var topStores = await _context.Stores
                .OrderByDescending(s => s.Reviews.Count)
                .Take(5)
                .ToListAsync();

            // Неподобрени магазини (чакащи одобрение)
            var pendingStores = await _context.Stores
                .Where(s => EF.Property<bool>(s, "IsApproved") == false)
                .ToListAsync();

            ViewBag.TopUsers = topUsers;
            ViewBag.TopStores = topStores;
            ViewBag.PendingStores = pendingStores;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApproveStore(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) return NotFound();

            _context.Entry(store).Property("IsApproved").CurrentValue = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
