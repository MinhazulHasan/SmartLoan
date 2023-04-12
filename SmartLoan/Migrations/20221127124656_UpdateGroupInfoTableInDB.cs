using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Migrations
{
    public partial class UpdateGroupInfoTableInDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupInfos_MemberInfos_MemberInfoId",
                table: "GroupInfos");

            migrationBuilder.AlterColumn<int>(
                name: "MemberInfoId",
                table: "GroupInfos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GroupLeaderId",
                table: "GroupInfos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupInfos_MemberInfos_MemberInfoId",
                table: "GroupInfos",
                column: "MemberInfoId",
                principalTable: "MemberInfos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupInfos_MemberInfos_MemberInfoId",
                table: "GroupInfos");

            migrationBuilder.AlterColumn<int>(
                name: "MemberInfoId",
                table: "GroupInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupLeaderId",
                table: "GroupInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupInfos_MemberInfos_MemberInfoId",
                table: "GroupInfos",
                column: "MemberInfoId",
                principalTable: "MemberInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
