using Microsoft.EntityFrameworkCore.Migrations;

namespace Northwind.EF.Persistence.MSSQL.Migrations
{
    public partial class CustomerNameIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Customer_CompanyName",
                table: "Customer",
                column: "CompanyName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_CompanyName",
                table: "Customer");
        }
    }
}
