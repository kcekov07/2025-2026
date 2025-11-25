using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoLoop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOpeningHoursToStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OpeningHours",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpeningHours",
                table: "Stores");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 18, 13, 14, 62, DateTimeKind.Utc).AddTicks(5277));

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 18, 13, 14, 62, DateTimeKind.Utc).AddTicks(5282));
        }
    }
}
