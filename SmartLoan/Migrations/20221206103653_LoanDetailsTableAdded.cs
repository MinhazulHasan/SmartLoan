using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Migrations
{
    public partial class LoanDetailsTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanMasterId = table.Column<int>(type: "int", nullable: false),
                    GroupInfoId = table.Column<int>(type: "int", nullable: false),
                    MemberInfoId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false),
                    Installment = table.Column<int>(type: "int", nullable: false),
                    InterestRate = table.Column<double>(type: "float", nullable: false),
                    ProcessingFee = table.Column<double>(type: "float", nullable: false),
                    OtherCharge = table.Column<double>(type: "float", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    TotalFee = table.Column<double>(type: "float", nullable: false),
                    AmountPerInstallment = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanDetails_LoanMasters_LoanMasterId",
                        column: x => x.LoanMasterId,
                        principalTable: "LoanMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanDetails_MemberInfos_MemberInfoId",
                        column: x => x.MemberInfoId,
                        principalTable: "MemberInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanDetails_LoanMasterId",
                table: "LoanDetails",
                column: "LoanMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDetails_MemberInfoId",
                table: "LoanDetails",
                column: "MemberInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanDetails");
        }
    }
}
