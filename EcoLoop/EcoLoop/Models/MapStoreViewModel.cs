namespace EcoLoop.Models
{
    public class MapStoreViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Category { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string OpeningHours { get; set; }

        public bool AcceptsOwnPackaging { get; set; }
        public double Rating { get; set; }
    }
}
