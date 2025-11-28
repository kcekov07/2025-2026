using EcoLoop.Data;
using EcoLoop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoLoop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminHomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminHomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new AdminDashboardViewModel
            {
                Users = _context.Users.Count(),
                Stores = _context.Stores.Count(),
                Reviews = _context.Reviews.Count(),
                PendingStores = _context.Stores.Where(s => !s.IsApproved).Count()
            };

            return View(model);
        }
    }
}
