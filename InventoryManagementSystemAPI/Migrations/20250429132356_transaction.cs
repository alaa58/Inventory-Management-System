using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class transaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransactions_Warehouses_WarehouseId",
                table: "InventoryTransactions");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransactions_WarehouseId",
                table: "InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "InventoryTransactions");

            migrationBuilder.AddColumn<int>(
                name: "FromWarehouseId",
                table: "InventoryTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToWarehouseId",
                table: "InventoryTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransactions_FromWarehouseId",
                table: "InventoryTransactions",
                column: "FromWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransactions_ToWarehouseId",
                table: "InventoryTransactions",
                column: "ToWarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransactions_Warehouses_FromWarehouseId",
                table: "InventoryTransactions",
                column: "FromWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransactions_Warehouses_ToWarehouseId",
                table: "InventoryTransactions",
                column: "ToWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransactions_Warehouses_FromWarehouseId",
                table: "InventoryTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransactions_Warehouses_ToWarehouseId",
                table: "InventoryTransactions");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransactions_FromWarehouseId",
                table: "InventoryTransactions");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransactions_ToWarehouseId",
                table: "InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "FromWarehouseId",
                table: "InventoryTransactions");

            migrationBuilder.DropColumn(
                name: "ToWarehouseId",
                table: "InventoryTransactions");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "InventoryTransactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransactions_WarehouseId",
                table: "InventoryTransactions",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransactions_Warehouses_WarehouseId",
                table: "InventoryTransactions",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }
    }
}
