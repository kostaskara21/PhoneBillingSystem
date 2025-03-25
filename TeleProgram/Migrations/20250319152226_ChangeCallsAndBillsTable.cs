using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeleProgram.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCallsAndBillsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Coast",
                table: "Bills",
                newName: "Cost");

            migrationBuilder.AddColumn<string>(
                name: "CalledPhoneNumber",
                table: "Calls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalledPhoneNumber",
                table: "Calls");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Bills",
                newName: "Coast");
        }
    }
}
