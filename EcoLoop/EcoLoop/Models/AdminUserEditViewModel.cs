using Microsoft.AspNetCore.Mvc;

namespace EcoLoop.Models
{
    public class AdminUserEditViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }

        public string CurrentRole { get; set; }
        [BindProperty]
        public string NewRole { get; set; }
    }
}
