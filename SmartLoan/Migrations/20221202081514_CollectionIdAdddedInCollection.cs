using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Migrations
{
    public partial class CollectionIdAdddedInCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDigital",
                table: "Collections");

            migrationBuilder.AlterColumn<double>(
                name: "PaymentInCash",
                table: "Collections",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollectionId",
                table: "Collections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "PaymentOnline",
                table: "Collections",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PaymentInCash",
                table: "CollectionDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PaymentOnline",
                table: "CollectionDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "PaymentOnline",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "PaymentInCash",
                table: "CollectionDetails");

            migrationBuilder.DropColumn(
                name: "PaymentOnline",
                table: "CollectionDetails");

            migrationBuilder.AlterColumn<double>(
                name: "PaymentInCash",
                table: "Collections",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<double>(
                name: "PaymentDigital",
                table: "Collections",
                type: "float",
                nullable: true);
        }
    }
}
