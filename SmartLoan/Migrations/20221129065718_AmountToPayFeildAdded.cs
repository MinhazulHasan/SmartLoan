using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Migrations
{
    public partial class AmountToPayFeildAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AmountToPay",
                table: "CollectionDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountToPay",
                table: "CollectionDetails");
        }
    }
}
