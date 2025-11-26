using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EcoLoop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class RegisterModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RegisterModel(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string UserRole { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = new ApplicationUser
        {
            UserName = Input.Email,
            Email = Input.Email,
            UserRole = Input.UserRole,
            EmailConfirmed = true
        };

        // Нива по подразбиране
        if (Input.UserRole == "User")
            user.EcoLevel = "Eco Explorer";

        if (Input.UserRole == "Producer")
            user.ProducerLevel = "Local Partner";

        var result = await _userManager.CreateAsync(user, Input.Password);

        if (result.Succeeded)
        {
            // ако ролята не съществува ? създаваме я
            if (!await _roleManager.RoleExistsAsync(Input.UserRole))
            {
                await _roleManager.CreateAsync(new IdentityRole(Input.UserRole));
            }

            // добавяне към роля
            await _userManager.AddToRoleAsync(user, Input.UserRole);

            // auto-login
            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToPage("/Index");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return Page();
    }
}
