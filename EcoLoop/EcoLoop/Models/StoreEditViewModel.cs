using System.ComponentModel.DataAnnotations;

namespace EcoLoop.Models
{
    public class StoreEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [MaxLength(120)]
        public string Category { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        public bool AcceptsOwnPackaging { get; set; }

        public string ImageUrl { get; set; }
    }
}
