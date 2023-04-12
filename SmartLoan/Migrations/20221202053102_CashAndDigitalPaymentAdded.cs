using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Migrations
{
    public partial class CashAndDigitalPaymentAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PaymentDigital",
                table: "Collections",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PaymentInCash",
                table: "Collections",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDigital",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "PaymentInCash",
                table: "Collections");
        }
    }
}
