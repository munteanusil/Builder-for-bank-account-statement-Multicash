using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiCashApp.Migrations
{
    /// <inheritdoc />
    public partial class NewChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplierNameNew",
                table: "SupplierChanges");

            migrationBuilder.RenameColumn(
                name: "SupplierNameOld",
                table: "SupplierChanges",
                newName: "SupplierName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplierName",
                table: "SupplierChanges",
                newName: "SupplierNameOld");

            migrationBuilder.AddColumn<string>(
                name: "SupplierNameNew",
                table: "SupplierChanges",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
