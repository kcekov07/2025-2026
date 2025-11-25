namespace EcoLoop.Models
{
    public class StoreCardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public string ImageUrl { get; set; }

        // Засега нямаме Reviews → оставяме Rating = 0
        public double Rating { get; set; }
    }
}
