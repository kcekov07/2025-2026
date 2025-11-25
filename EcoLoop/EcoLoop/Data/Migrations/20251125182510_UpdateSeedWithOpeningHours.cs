using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoLoop.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedWithOpeningHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "OpeningHours" },
                values: new object[] { new DateTime(2025, 11, 25, 18, 25, 9, 471, DateTimeKind.Utc).AddTicks(9459), "09:00-18:00" });

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "OpeningHours" },
                values: new object[] { new DateTime(2025, 11, 25, 18, 25, 9, 471, DateTimeKind.Utc).AddTicks(9469), "09:00-19:00" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "OpeningHours" },
                values: new object[] { new DateTime(2025, 11, 25, 18, 24, 26, 896, DateTimeKind.Utc).AddTicks(8416), null });

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "OpeningHours" },
                values: new object[] { new DateTime(2025, 11, 25, 18, 24, 26, 896, DateTimeKind.Utc).AddTicks(8422), null });
        }
    }
}
