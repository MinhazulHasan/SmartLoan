using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Migrations
{
    public partial class SeedingStatusValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Statuses(Title) VALUES('Due'),('Partially Collected'),('Collected')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
