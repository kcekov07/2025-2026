using Microsoft.AspNetCore.Identity;

namespace EcoLoop.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? ProfileImageUrl { get; set; } = "/images/profile/default.svg";


        public string UserRole { get; set; } = "User";

        // За потребители
        public string EcoLevel { get; set; } = "Eco Explorer";

        // За продавачи
        public string ProducerLevel { get; set; } = "Local Partner";

        // Кратко описание / биография
        public string? Bio { get; set; }

        // Статистики
        public int StoresVisited { get; set; }
        public int StoresAdded { get; set; }
        public int PackagingSaved { get; set; }
        public int EventsJoined { get; set; }
    }
}
