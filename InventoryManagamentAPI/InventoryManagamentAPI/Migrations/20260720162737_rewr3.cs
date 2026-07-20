using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagamentAPI.Migrations
{
    /// <inheritdoc />
    public partial class rewr3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinalQuantity",
                table: "InventoryProcesses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalQuantity",
                table: "InventoryProcesses");
        }
    }
}
