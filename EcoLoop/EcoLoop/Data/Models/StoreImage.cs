namespace EcoLoop.Data.Models
{
    public class StoreImage
    {
        public int Id { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public string Url { get; set; }
    }
}
