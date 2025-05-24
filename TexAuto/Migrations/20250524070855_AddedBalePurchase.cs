using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TexAuto.Migrations
{
    /// <inheritdoc />
    public partial class AddedBalePurchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BalePurchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InwardNo = table.Column<int>(type: "int", nullable: false),
                    InwardDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LotNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaleType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfBales = table.Column<int>(type: "int", nullable: false),
                    NetWeightKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RatePerKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MoisturePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Agent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionPerBale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransportVendor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreightCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnloadingCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDays = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CgstPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SgstPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IgstPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TcsAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RoundOff = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TdsPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaleOut = table.Column<int>(type: "int", nullable: false),
                    BaleUsed = table.Column<int>(type: "int", nullable: false),
                    WeighbridgeSlipNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrossWeightKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TareWeightKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalePurchases", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalePurchases");
        }
    }
}
