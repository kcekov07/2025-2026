using System;

namespace EcoLoop.Data.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public string Author { get; set; }

        public int Stars { get; set; }
        public string Comment { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
