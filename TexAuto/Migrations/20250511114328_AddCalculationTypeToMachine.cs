using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TexAuto.Migrations
{
    /// <inheritdoc />
    public partial class AddCalculationTypeToMachine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CalculationType",
                table: "Machines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalculationType",
                table: "Machines");
        }
    }
}
