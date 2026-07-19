using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagamentAPI.Migrations
{
    /// <inheritdoc />
    public partial class r4334r : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InventoryProcesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalStockInPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalStockOutPrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryProcesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryProcessDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ProcessType = table.Column<string>(type: "TEXT", nullable: false),
                    InventoryProcessId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProcessedQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryProcessDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryProcessDetails_InventoryProcesses_InventoryProcessId",
                        column: x => x.InventoryProcessId,
                        principalTable: "InventoryProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryProcessDetails_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryProcessDetails_InventoryProcessId",
                table: "InventoryProcessDetails",
                column: "InventoryProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryProcessDetails_ItemId",
                table: "InventoryProcessDetails",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryProcessDetails");

            migrationBuilder.DropTable(
                name: "InventoryProcesses");
        }
    }
}
