using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Migrations
{
    public partial class GroupInfoTableAddedInCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_GroupNames_GroupNameId",
                table: "Collections");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.RenameColumn(
                name: "GroupNameId",
                table: "Collections",
                newName: "GroupInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Collections_GroupNameId",
                table: "Collections",
                newName: "IX_Collections_GroupInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_GroupInfos_GroupInfoId",
                table: "Collections",
                column: "GroupInfoId",
                principalTable: "GroupInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_GroupInfos_GroupInfoId",
                table: "Collections");

            migrationBuilder.RenameColumn(
                name: "GroupInfoId",
                table: "Collections",
                newName: "GroupNameId");

            migrationBuilder.RenameIndex(
                name: "IX_Collections_GroupInfoId",
                table: "Collections",
                newName: "IX_Collections_GroupNameId");

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_GroupNames_GroupNameId",
                table: "Collections",
                column: "GroupNameId",
                principalTable: "GroupNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
