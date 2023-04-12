using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Migrations
{
    public partial class SubmissionPeriodAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmissionPeriodInMonth",
                table: "GroupInfos");

            migrationBuilder.AddColumn<int>(
                name: "SubmissionPeriodId",
                table: "GroupInfos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubmissionPeriods",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeInDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionPeriods", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupInfos_SubmissionPeriodId",
                table: "GroupInfos",
                column: "SubmissionPeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupInfos_SubmissionPeriods_SubmissionPeriodId",
                table: "GroupInfos",
                column: "SubmissionPeriodId",
                principalTable: "SubmissionPeriods",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupInfos_SubmissionPeriods_SubmissionPeriodId",
                table: "GroupInfos");

            migrationBuilder.DropTable(
                name: "SubmissionPeriods");

            migrationBuilder.DropIndex(
                name: "IX_GroupInfos_SubmissionPeriodId",
                table: "GroupInfos");

            migrationBuilder.DropColumn(
                name: "SubmissionPeriodId",
                table: "GroupInfos");

            migrationBuilder.AddColumn<byte>(
                name: "SubmissionPeriodInMonth",
                table: "GroupInfos",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
