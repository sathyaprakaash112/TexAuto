using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TexAuto.Migrations
{
    /// <inheritdoc />
    public partial class SeedShitsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "Id", "EffectiveDate", "EndTime1", "EndTime2", "EndTime3", "EndTime4", "StartTime1", "StartTime2", "StartTime3", "StartTime4", "TotalShifts" },
                values: new object[] { 1, new DateOnly(2024, 1, 1), new TimeOnly(20, 0, 0), new TimeOnly(8, 0, 0), null, null, new TimeOnly(8, 0, 0), new TimeOnly(20, 0, 0), null, null, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
