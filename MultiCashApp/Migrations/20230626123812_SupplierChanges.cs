using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiCashApp.Migrations
{
    /// <inheritdoc />
    public partial class SupplierChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupplierChanges",
                columns: table => new
                {
                    SupplierChangesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyCodeOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyCodeNew = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierNameOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierNameNew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FiscalCodeOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FiscalCodeNew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalityOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalityNew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberNew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailNew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankNew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TradeRegisterOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TradeRegisterNew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressNew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Swift_BIC_old = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Swift_BIC_new = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhoMadeChange = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierChanges", x => x.SupplierChangesId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierChanges");
        }
    }
}
