using System.ComponentModel.DataAnnotations;

namespace EcoLoop.Data.Models
{
    public class News
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        [MaxLength(300)]
        public string ShortDescription { get; set; }

        [Required]
        public string FullText { get; set; }

        [MaxLength(300)]
        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
