using System;
using System.Collections.Generic;
using System.Linq;

namespace EcoLoop.Data.Models
{
    public class Store
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Category { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string ImageUrl { get; set; }


        public bool AcceptsOwnPackaging { get; set; }
        public bool Delivery { get; set; }
        public bool IsProducer { get; set; }
        public string? OpeningHours { get; set; }  // пример: "09:00-18:00"

        public bool IsApproved { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<StoreImage> Images { get; set; } = new List<StoreImage>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public double Rating =>
            Reviews.Any() ? Math.Round(Reviews.Average(r => r.Stars), 1) : 0.0;
    }
}
