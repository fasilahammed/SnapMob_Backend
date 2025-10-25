using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SnapMob_Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "IsDeleted", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, "system", new DateTime(2025, 10, 25, 5, 31, 25, 128, DateTimeKind.Utc).AddTicks(8538), "system", null, false, "system", null, "Apple" },
                    { 2, "system", new DateTime(2025, 10, 25, 5, 31, 25, 128, DateTimeKind.Utc).AddTicks(9184), "system", null, false, "system", null, "Samsung" },
                    { 3, "system", new DateTime(2025, 10, 25, 5, 31, 25, 128, DateTimeKind.Utc).AddTicks(9186), "system", null, false, "system", null, "Xiaomi" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CreatedBy", "CreatedOn", "CurrentStock", "DeletedBy", "DeletedOn", "Description", "IsActive", "IsDeleted", "ModifiedBy", "ModifiedOn", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "system", new DateTime(2025, 10, 25, 5, 31, 25, 130, DateTimeKind.Utc).AddTicks(1714), 10, "system", null, "Latest Apple flagship", true, false, "system", null, "iPhone 15", 120000m },
                    { 2, 2, "system", new DateTime(2025, 10, 25, 5, 31, 25, 130, DateTimeKind.Utc).AddTicks(2222), 15, "system", null, "Latest Samsung flagship", true, false, "system", null, "Samsung Galaxy S24", 90000m }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "ImageUrl", "IsDeleted", "IsMain", "ModifiedBy", "ModifiedOn", "ProductId" },
                values: new object[,]
                {
                    { 1, "system", new DateTime(2025, 10, 25, 5, 31, 25, 130, DateTimeKind.Utc).AddTicks(4410), "system", null, "https://via.placeholder.com/400x400.png?text=iPhone+15", false, true, "system", null, 1 },
                    { 2, "system", new DateTime(2025, 10, 25, 5, 31, 25, 130, DateTimeKind.Utc).AddTicks(4415), "system", null, "https://via.placeholder.com/400x400.png?text=Galaxy+S24", false, true, "system", null, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
