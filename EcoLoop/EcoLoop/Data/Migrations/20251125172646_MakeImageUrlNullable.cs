using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcoLoop.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeImageUrlNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "AcceptsOwnPackaging", "Address", "Category", "CreatedAt", "Delivery", "Description", "ImageUrl", "IsProducer", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 1, false, "София, Център", "Био храни", new DateTime(2025, 11, 25, 17, 26, 46, 521, DateTimeKind.Utc).AddTicks(4397), false, "Био магазин с екологични продукти.", "/images/sample-store.jpg", false, 42.697699999999998, 23.321899999999999, "Bio Market" },
                    { 2, false, "Пловдив, Център", "Еко стоки", new DateTime(2025, 11, 25, 17, 26, 46, 521, DateTimeKind.Utc).AddTicks(4405), false, "Магазин за устойчиви продукти.", "/images/sample-store2.jpg", false, 42.1479, 24.75, "Green Shop" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
