using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPay.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLogIdToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogId",
                table: "PaymentRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogId",
                table: "PaymentRecords");
        }
    }
}
