using EcoLoop.Data.Models;
using System.Collections.Generic;

namespace EcoLoop.Models
{
    public class HomePageViewModel
    {
        public List<News> TopNews { get; set; } = new();
    }
}
