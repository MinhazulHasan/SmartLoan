using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Migrations
{
    public partial class ModifyGroupWithMembersTableIntoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupWithMembers_GroupInfos_GroupInfoId",
                table: "GroupWithMembers");

            migrationBuilder.AlterColumn<int>(
                name: "GroupInfoId",
                table: "GroupWithMembers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupWithMembers_GroupInfos_GroupInfoId",
                table: "GroupWithMembers",
                column: "GroupInfoId",
                principalTable: "GroupInfos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupWithMembers_GroupInfos_GroupInfoId",
                table: "GroupWithMembers");

            migrationBuilder.AlterColumn<int>(
                name: "GroupInfoId",
                table: "GroupWithMembers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupWithMembers_GroupInfos_GroupInfoId",
                table: "GroupWithMembers",
                column: "GroupInfoId",
                principalTable: "GroupInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
