using Microsoft.AspNetCore.Identity;

namespace EcoLoop.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Профилна снимка
        public string ProfileImageUrl { get; set; } = "/images/default-avatar.png";

        // Видимата роля в профила: "User", "Producer", "Admin"
        public string UserRole { get; set; } = "User";

        // Нива за нормален потребител
        // Eco Explorer → Green Hero → Earth Guardian
        public string EcoLevel { get; set; } = "Eco Explorer";

        // Нива за продавач
        // Примери: Local Partner → Green Vendor → Eco Champion
        public string ProducerLevel { get; set; } = "Local Partner";

        // Статистики
        public int StoresVisited { get; set; }      // 🌍 Посетени магазини
        public int StoresAdded { get; set; }        // ♻️ Добавени обекти
        public int PackagingSaved { get; set; }     // 💚 Спестени опаковки
        public int EventsJoined { get; set; }       // 📅 Участия в събития
    }
}
