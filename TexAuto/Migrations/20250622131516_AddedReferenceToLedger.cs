using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TexAuto.Migrations
{
    /// <inheritdoc />
    public partial class AddedReferenceToLedger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Agent",
                table: "BalePurchases");

            migrationBuilder.DropColumn(
                name: "BaleType",
                table: "BalePurchases");

            migrationBuilder.DropColumn(
                name: "PurchaseAccount",
                table: "BalePurchases");

            migrationBuilder.DropColumn(
                name: "Supplier",
                table: "BalePurchases");

            migrationBuilder.DropColumn(
                name: "TransportVendor",
                table: "BalePurchases");

            migrationBuilder.RenameColumn(
                name: "LedgerName",
                table: "LedgerMaster",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "ProductionNumber",
                table: "Productions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AgentId",
                table: "BalePurchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaleTypeId",
                table: "BalePurchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PurchaseAccountId",
                table: "BalePurchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "BalePurchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransportVendorId",
                table: "BalePurchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnloadingManId",
                table: "BalePurchases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BalePurchases_AgentId",
                table: "BalePurchases",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_BalePurchases_BaleTypeId",
                table: "BalePurchases",
                column: "BaleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BalePurchases_PurchaseAccountId",
                table: "BalePurchases",
                column: "PurchaseAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BalePurchases_SupplierId",
                table: "BalePurchases",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_BalePurchases_TransportVendorId",
                table: "BalePurchases",
                column: "TransportVendorId");

            migrationBuilder.CreateIndex(
                name: "IX_BalePurchases_UnloadingManId",
                table: "BalePurchases",
                column: "UnloadingManId");

            migrationBuilder.AddForeignKey(
                name: "FK_BalePurchases_LedgerMaster_AgentId",
                table: "BalePurchases",
                column: "AgentId",
                principalTable: "LedgerMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BalePurchases_LedgerMaster_PurchaseAccountId",
                table: "BalePurchases",
                column: "PurchaseAccountId",
                principalTable: "LedgerMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BalePurchases_LedgerMaster_SupplierId",
                table: "BalePurchases",
                column: "SupplierId",
                principalTable: "LedgerMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BalePurchases_LedgerMaster_TransportVendorId",
                table: "BalePurchases",
                column: "TransportVendorId",
                principalTable: "LedgerMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BalePurchases_LedgerMaster_UnloadingManId",
                table: "BalePurchases",
                column: "UnloadingManId",
                principalTable: "LedgerMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BalePurchases_Products_BaleTypeId",
                table: "BalePurchases",
                column: "BaleTypeId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BalePurchases_LedgerMaster_AgentId",
                table: "BalePurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_BalePurchases_LedgerMaster_PurchaseAccountId",
                table: "BalePurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_BalePurchases_LedgerMaster_SupplierId",
                table: "BalePurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_BalePurchases_LedgerMaster_TransportVendorId",
                table: "BalePurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_BalePurchases_LedgerMaster_UnloadingManId",
                table: "BalePurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_BalePurchases_Products_BaleTypeId",
                table: "BalePurchases");

            migrationBuilder.DropIndex(
                name: "IX_BalePurchases_AgentId",
                table: "BalePurchases");

            migrationBuilder.DropIndex(
                name: "IX_BalePurchases_BaleTypeId",
                table: "BalePurchases");

            migrationBuilder.DropIndex(
                name: "IX_BalePurchases_PurchaseAccountId",
                table: "BalePurchases");

            migrationBuilder.DropIndex(
                name: "IX_BalePurchases_SupplierId",
                table: "BalePurchases");

            migrationBuilder.DropIndex(
                name: "IX_BalePurchases_TransportVendorId",
                table: "BalePurchases");

            migrationBuilder.DropIndex(
                name: "IX_BalePurchases_UnloadingManId",
                table: "BalePurchases");

            migrationBuilder.DropColumn(
                name: "ProductionNumber",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "BalePurchases");

            migrationBuilder.DropColumn(
                name: "BaleTypeId",
                table: "BalePurchases");

            migrationBuilder.DropColumn(
                name: "PurchaseAccountId",
                table: "BalePurchases");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "BalePurchases");

            migrationBuilder.DropColumn(
                name: "TransportVendorId",
                table: "BalePurchases");

            migrationBuilder.DropColumn(
                name: "UnloadingManId",
                table: "BalePurchases");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "LedgerMaster",
                newName: "LedgerName");

            migrationBuilder.AddColumn<string>(
                name: "Agent",
                table: "BalePurchases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BaleType",
                table: "BalePurchases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchaseAccount",
                table: "BalePurchases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Supplier",
                table: "BalePurchases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransportVendor",
                table: "BalePurchases",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
