using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiCashApp.Migrations
{
    /// <inheritdoc />
    public partial class Supplierchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_CompanyAccounts_CompanyCodeCompanyId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_CompanyCodeCompanyId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CompanyCodeCompanyId",
                table: "Suppliers");

            migrationBuilder.AddColumn<string>(
                name: "CompanyCode",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyCode",
                table: "Suppliers");

            migrationBuilder.AddColumn<int>(
                name: "CompanyCodeCompanyId",
                table: "Suppliers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CompanyCodeCompanyId",
                table: "Suppliers",
                column: "CompanyCodeCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_CompanyAccounts_CompanyCodeCompanyId",
                table: "Suppliers",
                column: "CompanyCodeCompanyId",
                principalTable: "CompanyAccounts",
                principalColumn: "CompanyId");
        }
    }
}
