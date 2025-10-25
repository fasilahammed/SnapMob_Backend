using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnapMob_Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultProductss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://res.cloudinary.com/demo/image/upload/iphone15.jpg" });

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://res.cloudinary.com/demo/image/upload/galaxy_s23.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "CurrentStock", "Description", "Price" },
                values: new object[] { new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, "Latest Apple iPhone", 1099.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "CurrentStock", "Description", "Name", "Price" },
                values: new object[] { new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 40, "Flagship Samsung phone", "Galaxy S23", 999.99m });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 10, 25, 5, 35, 37, 397, DateTimeKind.Utc).AddTicks(2421));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 10, 25, 5, 35, 37, 397, DateTimeKind.Utc).AddTicks(3279));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 10, 25, 5, 35, 37, 397, DateTimeKind.Utc).AddTicks(3282));

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 25, 15, 0, 0, 0, DateTimeKind.Unspecified), "https://via.placeholder.com/400x400.png?text=iPhone+15" });

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 25, 15, 0, 0, 0, DateTimeKind.Unspecified), "https://via.placeholder.com/400x400.png?text=Galaxy+S24" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "CurrentStock", "Description", "Price" },
                values: new object[] { new DateTime(2025, 10, 25, 15, 0, 0, 0, DateTimeKind.Unspecified), 10, "Latest Apple flagship", 120000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "CurrentStock", "Description", "Name", "Price" },
                values: new object[] { new DateTime(2025, 10, 25, 15, 0, 0, 0, DateTimeKind.Unspecified), 15, "Latest Samsung flagship", "Samsung Galaxy S24", 90000m });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
