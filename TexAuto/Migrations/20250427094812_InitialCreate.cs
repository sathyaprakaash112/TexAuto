using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TexAuto.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalShifts = table.Column<int>(type: "int", nullable: false),
                    StartTime1 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime1 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime2 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime2 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime3 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime3 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime4 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime4 = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shift");
        }
    }
}
