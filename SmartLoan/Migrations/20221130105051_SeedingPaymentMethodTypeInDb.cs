using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Migrations
{
    public partial class SeedingPaymentMethodTypeInDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO PaymentMethods(Id,MethodName) VALUES (1,'Cash'),(2,'Digital')");
            migrationBuilder.Sql("UPDATE CollectionDetails SET PaymentMethodId=1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
