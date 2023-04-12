using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Migrations
{
    public partial class AddMarritialStatusValueInItsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO MarritialStatuses (Id, StatusTitle) VALUES (1, 'Single')");
            migrationBuilder.Sql("INSERT INTO MarritialStatuses (Id, StatusTitle) VALUES (2, 'Married')");
            migrationBuilder.Sql("INSERT INTO MarritialStatuses (Id, StatusTitle) VALUES (3, 'Devorced')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
