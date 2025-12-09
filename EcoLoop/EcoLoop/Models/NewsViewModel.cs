using System.ComponentModel.DataAnnotations;

namespace EcoLoop.Models
{
    public class NewsViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Summary { get; set; }

        [Required]
        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CategoryId { get; set; }
    }
}
