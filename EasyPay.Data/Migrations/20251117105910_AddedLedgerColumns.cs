using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPay.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLedgerColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ClosingBalance",
                table: "PaymentRecords",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "PaymentRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingBalance",
                table: "PaymentRecords");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "PaymentRecords");
        }
    }
}
