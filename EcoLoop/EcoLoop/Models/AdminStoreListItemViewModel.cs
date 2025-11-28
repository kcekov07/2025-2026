namespace EcoLoop.Models
{
    public class AdminStoreListItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Category { get; set; }
        public string Address { get; set; }

        public bool IsApproved { get; set; }
        public double Rating { get; set; }
        public int ReviewsCount { get; set; }

        public string CreatedAtShort { get; set; }
    }
}
