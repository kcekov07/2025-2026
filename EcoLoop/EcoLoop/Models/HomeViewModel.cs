using System.Collections.Generic;


namespace EcoLoop.Models
{
    public class HomeViewModel
    {
        public IEnumerable<StoreCardViewModel> NearbyStores { get; set; }
        public int StoresCount { get; set; }
        public int ReviewsCount { get; set; }
        public int EventsCount { get; set; }
    }

    
}
