using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Migrations
{
    public partial class LoansIdAddedInLoansTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LoanName",
                table: "Loans",
                newName: "LoanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LoanId",
                table: "Loans",
                newName: "LoanName");
        }
    }
}
