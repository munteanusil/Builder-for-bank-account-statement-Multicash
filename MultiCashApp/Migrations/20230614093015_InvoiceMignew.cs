using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiCashApp.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceMignew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Suppliers_CompanyName",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CompanyName",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Invoices");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Invoices");

            migrationBuilder.AddColumn<int>(
                name: "CompanyName",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CompanyName",
                table: "Invoices",
                column: "CompanyName");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Suppliers_CompanyName",
                table: "Invoices",
                column: "CompanyName",
                principalTable: "Suppliers",
                principalColumn: "SupplierId");
        }
    }
}
