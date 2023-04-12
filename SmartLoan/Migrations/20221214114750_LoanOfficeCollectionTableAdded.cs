using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Migrations
{
    public partial class LoanOfficeCollectionTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SubmittedAmountToOffice",
                table: "InstallmentMaster",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "LoanOfficeCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstallmentMasterId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    CollectorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmitterId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CollectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountReceived = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanOfficeCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanOfficeCollections_InstallmentMaster_InstallmentMasterId",
                        column: x => x.InstallmentMasterId,
                        principalTable: "InstallmentMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanOfficeCollections_InstallmentMasterId",
                table: "LoanOfficeCollections",
                column: "InstallmentMasterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanOfficeCollections");

            migrationBuilder.DropColumn(
                name: "SubmittedAmountToOffice",
                table: "InstallmentMaster");
        }
    }
}
