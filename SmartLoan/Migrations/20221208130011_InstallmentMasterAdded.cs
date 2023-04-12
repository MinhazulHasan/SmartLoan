using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Migrations
{
    public partial class InstallmentMasterAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstallmentMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstallmentNumber = table.Column<int>(type: "int", nullable: false),
                    GroupInfoId = table.Column<int>(type: "int", nullable: false),
                    LoanMasterId = table.Column<int>(type: "int", nullable: false),
                    ExpectedAmount = table.Column<double>(type: "float", nullable: false),
                    CollectedAmount = table.Column<double>(type: "float", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CollectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CollectorId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstallmentMaster_LoanMasters_LoanMasterId",
                        column: x => x.LoanMasterId,
                        principalTable: "LoanMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstallmentMaster_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentMaster_LoanMasterId",
                table: "InstallmentMaster",
                column: "LoanMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentMaster_StatusId",
                table: "InstallmentMaster",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstallmentMaster");
        }
    }
}
