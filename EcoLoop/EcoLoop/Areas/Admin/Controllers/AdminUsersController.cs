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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminUsersController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // =====================================
        // LIST USERS
        // =====================================
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users
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

        // =====================================
        // EDIT ROLE - GET
        // =====================================
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var model = new AdminUserEditViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CurrentRole = roles.FirstOrDefault() ?? "User",
                NewRole = user.UserRole
            };

            return View(model);
        }

        // =====================================
        // EDIT ROLE - POST (ТОВА Е КЛЮЧОВО)
        // =====================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminUserEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound();

            // 1️⃣ Взимаме ВСИЧКИ текущи роли
            var oldRoles = await _userManager.GetRolesAsync(user);

            // 2️⃣ Премахваме всички роли
            await _userManager.RemoveFromRolesAsync(user, oldRoles);

            // 3️⃣ Добавяме новата роля
            await _userManager.AddToRoleAsync(user, model.NewRole);

            // 4️⃣ Записваме в custom колоната UserRole
            user.UserRole = model.NewRole;

            // 5️⃣ Ъпдейтваме user-а през Identity
            await _userManager.UpdateAsync(user);

            return Redirect("/Admin/AdminUsers");
        }

        // =====================================
        // BLOCK USER
        // =====================================
        [HttpPost]
        public async Task<IActionResult> Block(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            user.LockoutEnd = DateTime.UtcNow.AddYears(10);
            await _userManager.UpdateAsync(user);

            return Redirect("/Admin/AdminUsers");
        }

        // =====================================
        // UNBLOCK USER
        // =====================================
        [HttpPost]
        public async Task<IActionResult> Unblock(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            user.LockoutEnd = null;
            await _userManager.UpdateAsync(user);

            return Redirect("/Admin/AdminUsers");
        }

        // =====================================
        // DELETE USER
        // =====================================
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            await _userManager.DeleteAsync(user);

            return Redirect("/Admin/AdminUsers");
        }
    }
}
