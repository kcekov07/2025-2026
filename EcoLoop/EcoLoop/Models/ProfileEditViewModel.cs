namespace EcoLoop.Models
{
    public class ProfileEditViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string? Bio { get; set; }

        // За Upload
        public IFormFile? ProfileImage { get; set; }

        public string? CurrentImageUrl { get; set; }
    }
}
