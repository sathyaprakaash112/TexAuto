using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TexAuto.Migrations
{
    /// <inheritdoc />
    public partial class AddedBalePurchaseDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoisturePercent",
                table: "BalePurchases");

            migrationBuilder.DropColumn(
                name: "NetWeightKg",
                table: "BalePurchases");

            migrationBuilder.DropColumn(
                name: "NumberOfBales",
                table: "BalePurchases");

            migrationBuilder.DropColumn(
                name: "RatePerKg",
                table: "BalePurchases");

            migrationBuilder.CreateTable(
                name: "BaleDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BalePurchaseId = table.Column<int>(type: "int", nullable: false),
                    NumberOfBales = table.Column<int>(type: "int", nullable: false),
                    NetWeightKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RatePerKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MoisturePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaleDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaleDetails_BalePurchases_BalePurchaseId",
                        column: x => x.BalePurchaseId,
                        principalTable: "BalePurchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaleDetails_BalePurchaseId",
                table: "BaleDetails",
                column: "BalePurchaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaleDetails");

            migrationBuilder.AddColumn<decimal>(
                name: "MoisturePercent",
                table: "BalePurchases",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NetWeightKg",
                table: "BalePurchases",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBales",
                table: "BalePurchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "RatePerKg",
                table: "BalePurchases",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
