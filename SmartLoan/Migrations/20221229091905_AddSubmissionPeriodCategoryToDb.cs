using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Migrations
{
    public partial class AddSubmissionPeriodCategoryToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO SubmissionPeriods (Title, TimeInDays) VALUES ('Weekly', 7)");
            migrationBuilder.Sql("INSERT INTO SubmissionPeriods (Title, TimeInDays) VALUES ('Fortnightly', 15)");
            migrationBuilder.Sql("INSERT INTO SubmissionPeriods (Title, TimeInDays) VALUES ('Monthly', 30)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
