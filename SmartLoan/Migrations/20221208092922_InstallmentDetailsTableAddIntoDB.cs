using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Migrations
{
    public partial class InstallmentDetailsTableAddIntoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstallmentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstalllmentNumber = table.Column<int>(type: "int", nullable: false),
                    CollectorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoanMasterId = table.Column<int>(type: "int", nullable: false),
                    LoanId = table.Column<int>(type: "int", nullable: false),
                    MemberInfoId = table.Column<int>(type: "int", nullable: false),
                    InstallmentAmount = table.Column<double>(type: "float", nullable: false),
                    ReceivedAmount = table.Column<double>(type: "float", nullable: false),
                    PenaltyCharge = table.Column<double>(type: "float", nullable: false),
                    DueAmount = table.Column<double>(type: "float", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    InstallmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstallmentDetails_LoanMasters_LoanMasterId",
                        column: x => x.LoanMasterId,
                        principalTable: "LoanMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstallmentDetails_MemberInfos_MemberInfoId",
                        column: x => x.MemberInfoId,
                        principalTable: "MemberInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstallmentDetails_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentDetails_LoanMasterId",
                table: "InstallmentDetails",
                column: "LoanMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentDetails_MemberInfoId",
                table: "InstallmentDetails",
                column: "MemberInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentDetails_StatusId",
                table: "InstallmentDetails",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstallmentDetails");
        }
    }
}
