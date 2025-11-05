using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnapMob_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddBillingNameAndPhoneToOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RazorpayOrderId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "BillingName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingPhone",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BillingPhone",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "PaymentId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazorpayOrderId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
