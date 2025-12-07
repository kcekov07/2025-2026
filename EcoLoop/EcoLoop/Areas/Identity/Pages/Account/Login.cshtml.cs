using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EcoLoop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcoLoop.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required, EmailAddress]
            public string Email { get; set; }

            [Required, DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // 1) Намираме user по имейл
            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Невалиден имейл или парола.");
                return Page();
            }

            // 2) Проверка дали е блокиран (LockoutEnd в бъдещето)
            if (user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.UtcNow)
            {
                // пренасочваме към специална страница за блокиран акаунт
                return RedirectToPage("/Account/Lockout");
            }

            // 3) Проверяваме паролата
            var result = await _signInManager.PasswordSignInAsync(
                user.UserName, Input.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Невалиден имейл или парола.");
                return Page();
            }

            // 4) Обновяваме claim "UserRole" да съвпада с текущото UserRole поле в базата
            var claims = await _userManager.GetClaimsAsync(user);
            var oldRoleClaim = claims.FirstOrDefault(c => c.Type == "UserRole");
            if (oldRoleClaim != null)
            {
                await _userManager.RemoveClaimAsync(user, oldRoleClaim);
            }

            await _userManager.AddClaimAsync(user, new Claim("UserRole", user.UserRole));

            // 5) Вече сме логнати (PasswordSignIn го прави), просто към home:
            return Redirect("/");
        }
    }
}
