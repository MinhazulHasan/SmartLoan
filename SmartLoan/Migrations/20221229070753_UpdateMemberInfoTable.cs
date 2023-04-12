using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Migrations
{
    public partial class UpdateMemberInfoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MotherName",
                table: "MemberInfos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PassportNum",
                table: "MemberInfos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpouseNid",
                table: "MemberInfos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpousePhone",
                table: "MemberInfos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotherName",
                table: "MemberInfos");

            migrationBuilder.DropColumn(
                name: "PassportNum",
                table: "MemberInfos");

            migrationBuilder.DropColumn(
                name: "SpouseNid",
                table: "MemberInfos");

            migrationBuilder.DropColumn(
                name: "SpousePhone",
                table: "MemberInfos");
        }
    }
}
