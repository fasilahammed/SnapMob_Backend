using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SnapMob_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddProductSpecsFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Battery",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Camera",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Display",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Storage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "ProductImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Battery",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Camera",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Display",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Storage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "ProductImages");

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "IsDeleted", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, "system", new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", null, false, "system", null, "Apple" },
                    { 2, "system", new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", null, false, "system", null, "Samsung" },
                    { 3, "system", new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", null, false, "system", null, "Xiaomi" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CreatedBy", "CreatedOn", "CurrentStock", "DeletedBy", "DeletedOn", "Description", "IsActive", "IsDeleted", "ModifiedBy", "ModifiedOn", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "system", new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, "system", null, "Latest Apple iPhone", true, false, "system", null, "iPhone 15", 1099.99m },
                    { 2, 2, "system", new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 40, "system", null, "Flagship Samsung phone", true, false, "system", null, "Galaxy S23", 999.99m }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "ImageUrl", "IsDeleted", "IsMain", "ModifiedBy", "ModifiedOn", "ProductId" },
                values: new object[,]
                {
                    { 1, "system", new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", null, "https://res.cloudinary.com/demo/image/upload/iphone15.jpg", false, true, "system", null, 1 },
                    { 2, "system", new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", null, "https://res.cloudinary.com/demo/image/upload/galaxy_s23.jpg", false, true, "system", null, 2 }
                });
        }
    }
}
