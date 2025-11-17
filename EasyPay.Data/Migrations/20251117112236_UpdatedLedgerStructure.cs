using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPay.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedLedgerStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "PaymentRecords",
                newName: "TransferredAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "OpeningBalance",
                table: "PaymentRecords",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpeningBalance",
                table: "PaymentRecords");

            migrationBuilder.RenameColumn(
                name: "TransferredAmount",
                table: "PaymentRecords",
                newName: "Amount");
        }
    }
}
