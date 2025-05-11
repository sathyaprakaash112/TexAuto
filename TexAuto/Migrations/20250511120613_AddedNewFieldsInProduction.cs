using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TexAuto.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewFieldsInProduction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ClosingHank",
                table: "Productions",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OpeningHank",
                table: "Productions",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingHank",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "OpeningHank",
                table: "Productions");
        }
    }
}
