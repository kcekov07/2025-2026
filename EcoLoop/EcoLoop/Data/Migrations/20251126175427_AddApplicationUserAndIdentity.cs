using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcoLoop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUserAndIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Stores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProducerLevel",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "ProducerLevel",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "AcceptsOwnPackaging", "Address", "Category", "CreatedAt", "Delivery", "Description", "ImageUrl", "IsProducer", "Latitude", "Longitude", "Name", "OpeningHours" },
                values: new object[,]
                {
                    { 1, true, "София", "Еко храни", new DateTime(2025, 11, 26, 17, 35, 40, 234, DateTimeKind.Utc).AddTicks(4672), false, "Био магазин", "/images/sample/bio.jpg", false, 42.697699999999998, 23.321899999999999, "Bio Market", "09:00-18:00" },
                    { 2, false, "Пловдив", "Натурална козметика", new DateTime(2025, 11, 26, 17, 35, 40, 234, DateTimeKind.Utc).AddTicks(4686), false, "Естествена козметика", "/images/sample/cosmetics.jpg", false, 42.1479, 24.75, "Green Cosmetics", "09:00-19:00" }
                });
        }
    }
}
