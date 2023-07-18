using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiCashApp.Migrations
{
    /// <inheritdoc />
    public partial class AllSuppl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierUpload",
                table: "SupplierUpload");

            migrationBuilder.RenameTable(
                name: "SupplierUpload",
                newName: "AllSupplierList");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllSupplierList",
                table: "AllSupplierList",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AllSupplierList",
                table: "AllSupplierList");

            migrationBuilder.RenameTable(
                name: "AllSupplierList",
                newName: "SupplierUpload");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierUpload",
                table: "SupplierUpload",
                column: "SupplierId");
        }
    }
}
