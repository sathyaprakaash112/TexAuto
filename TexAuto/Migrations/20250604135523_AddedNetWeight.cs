using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TexAuto.Migrations
{
    /// <inheritdoc />
    public partial class AddedNetWeight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "NetWeightKg",
                table: "BalePurchases",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetWeightKg",
                table: "BalePurchases");
        }
    }
}
