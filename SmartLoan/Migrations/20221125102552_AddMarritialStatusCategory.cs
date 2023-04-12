using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Migrations
{
    public partial class AddMarritialStatusCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberInfos_MarritialStatus_MarritialStatusId",
                table: "MemberInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarritialStatus",
                table: "MarritialStatus");

            migrationBuilder.RenameTable(
                name: "MarritialStatus",
                newName: "MarritialStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarritialStatuses",
                table: "MarritialStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberInfos_MarritialStatuses_MarritialStatusId",
                table: "MemberInfos",
                column: "MarritialStatusId",
                principalTable: "MarritialStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberInfos_MarritialStatuses_MarritialStatusId",
                table: "MemberInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarritialStatuses",
                table: "MarritialStatuses");

            migrationBuilder.RenameTable(
                name: "MarritialStatuses",
                newName: "MarritialStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarritialStatus",
                table: "MarritialStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberInfos_MarritialStatus_MarritialStatusId",
                table: "MemberInfos",
                column: "MarritialStatusId",
                principalTable: "MarritialStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
