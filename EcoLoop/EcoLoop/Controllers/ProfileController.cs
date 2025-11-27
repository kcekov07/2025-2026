using EcoLoop.Data;
using EcoLoop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcoLoop.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public ProfileController(UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _env = env;
        }

        // 👉 ЕТО ГО ЛИПСВАЩИЯТ МЕТОД!
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Redirect("/Identity/Account/Login");

            return View(user);
        }

        // Показване на Edit профил
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new ProfileEditViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                Bio = user.Bio,
                CurrentImageUrl = user.ProfileImageUrl
            };

            return View(model);
        }

        // Обработка след редакция
        [HttpPost]
        public async Task<IActionResult> Edit(ProfileEditViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
                return View(model);

            // Update basic info
            user.UserName = model.Username;
            user.Email = model.Email;
            user.Bio = model.Bio;

            // Upload profile image
            if (model.ProfileImage != null)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ProfileImage.FileName)}";
                var path = Path.Combine(_env.WebRootPath, "images/profile", fileName);

                using (var stream = System.IO.File.Create(path))
                {
                    await model.ProfileImage.CopyToAsync(stream);
                }

                user.ProfileImageUrl = "/images/profile/" + fileName;
            }

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index", "Profile");
        }
    }
}
