using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoLoop.Data.Migrations
{
    /// <inheritdoc />
    public partial class edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AcceptsOwnPackaging", "Address", "Category", "CreatedAt", "Description", "ImageUrl" },
                values: new object[] { true, "София", "Еко храни", new DateTime(2025, 11, 25, 18, 13, 14, 62, DateTimeKind.Utc).AddTicks(5277), "Био магазин", "/images/sample/bio.jpg" });

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "Category", "CreatedAt", "Description", "ImageUrl", "Name" },
                values: new object[] { "Пловдив", "Натурална козметика", new DateTime(2025, 11, 25, 18, 13, 14, 62, DateTimeKind.Utc).AddTicks(5282), "Естествена козметика", "/images/sample/cosmetics.jpg", "Green Cosmetics" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AcceptsOwnPackaging", "Address", "Category", "CreatedAt", "Description", "ImageUrl" },
                values: new object[] { false, "София, Център", "Био храни", new DateTime(2025, 11, 25, 17, 26, 46, 521, DateTimeKind.Utc).AddTicks(4397), "Био магазин с екологични продукти.", "/images/sample-store.jpg" });

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "Category", "CreatedAt", "Description", "ImageUrl", "Name" },
                values: new object[] { "Пловдив, Център", "Еко стоки", new DateTime(2025, 11, 25, 17, 26, 46, 521, DateTimeKind.Utc).AddTicks(4405), "Магазин за устойчиви продукти.", "/images/sample-store2.jpg", "Green Shop" });
        }
    }
}
