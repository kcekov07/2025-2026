using EcoLoop.Data;
using EcoLoop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoLoop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminUsersController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        // ================================
        // LIST USERS
        // ================================
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
                .Select(u => new AdminUserListViewModel
                {
                    UserId = u.Id,
                    Email = u.Email,
                    UserName = u.UserName,
                    Role = u.UserRole,
                    IsBlocked = u.LockoutEnd != null && u.LockoutEnd > DateTime.UtcNow
                })
                .ToListAsync();

            return View(users);
        }


        // ================================
        // EDIT ROLE - GET
        // ================================
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            var model = new AdminUserEditViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CurrentRole = user.UserRole,
                NewRole = user.UserRole
            };

            return View(model);
        }


        // ================================
        // EDIT ROLE - POST
        // ================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminUserEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.Users.FindAsync(model.UserId);

            if (user == null)
                return NotFound();

            // ---------------------------
            // Remove old role
            // ---------------------------
            if (!string.IsNullOrEmpty(user.UserRole))
                await _userManager.RemoveFromRoleAsync(user, user.UserRole);

            // ---------------------------
            // Add new role
            // ---------------------------
            await _userManager.AddToRoleAsync(user, model.NewRole);

            // ---------------------------
            // Update UserRole field in DB
            // ---------------------------
            user.UserRole = model.NewRole;

            await _context.SaveChangesAsync();

            return Redirect("/Admin/AdminUsers");
        }



        // ================================
        // BLOCK USER
        // ================================
        [HttpPost]
        public async Task<IActionResult> Block(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            user.LockoutEnd = DateTime.UtcNow.AddYears(10);

            await _context.SaveChangesAsync();

            return Redirect("/Admin/AdminUsers");
        }


        // ================================
        // UNBLOCK USER
        // ================================
        [HttpPost]
        public async Task<IActionResult> Unblock(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            user.LockoutEnd = null;

            await _context.SaveChangesAsync();

            return Redirect("/Admin/AdminUsers");
        }


        // ================================
        // DELETE USER
        // ================================
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Redirect("/Admin/AdminUsers");
        }
    }
}
