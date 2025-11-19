using System.ComponentModel.DataAnnotations;

namespace EcoLoop.Models
{
    public class NewsAddViewModel
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        [MaxLength(300)]
        public string ShortDescription { get; set; }

        [Required]
        public string FullText { get; set; }

        public string ImageUrl { get; set; }
    }
}
