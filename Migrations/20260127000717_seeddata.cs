using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FMS.API.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Barcode", "Code", "CreateDate", "Name" },
                values: new object[,]
                {
                    { 1, "papaya-001", "001", new DateTime(2026, 1, 27, 6, 7, 16, 821, DateTimeKind.Local).AddTicks(5009), "Papaya Green Queen" },
                    { 2, "papaya-shani-002", "002", new DateTime(2026, 1, 27, 6, 7, 16, 821, DateTimeKind.Local).AddTicks(5028), "Papaya Shahi" },
                    { 3, "papaya-babu-003", "003", new DateTime(2026, 1, 27, 6, 7, 16, 821, DateTimeKind.Local).AddTicks(5031), "Papaya Babu" },
                    { 4, "papaya-004", "004", new DateTime(2026, 1, 27, 6, 7, 16, 821, DateTimeKind.Local).AddTicks(5032), "Papaya Top Lady" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
