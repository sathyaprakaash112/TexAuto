using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TexAuto.Migrations
{
    /// <inheritdoc />
    public partial class CleanBuildAfterAddingNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EffectiveDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalShifts = table.Column<int>(type: "int", nullable: false),
                    StartTime1 = table.Column<TimeOnly>(type: "time", nullable: true),
                    EndTime1 = table.Column<TimeOnly>(type: "time", nullable: true),
                    StartTime2 = table.Column<TimeOnly>(type: "time", nullable: true),
                    EndTime2 = table.Column<TimeOnly>(type: "time", nullable: true),
                    StartTime3 = table.Column<TimeOnly>(type: "time", nullable: true),
                    EndTime3 = table.Column<TimeOnly>(type: "time", nullable: true),
                    StartTime4 = table.Column<TimeOnly>(type: "time", nullable: true),
                    EndTime4 = table.Column<TimeOnly>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MachineTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineTypes_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Tradable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTypes_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineTypeId = table.Column<int>(type: "int", nullable: false),
                    CalculationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Machines_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Machines_MachineTypes_MachineTypeId",
                        column: x => x.MachineTypeId,
                        principalTable: "MachineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    SetHank = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NetWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Productions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    ProductInId = table.Column<int>(type: "int", nullable: false),
                    ProductOutId = table.Column<int>(type: "int", nullable: false),
                    ShiftDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpeningHank = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ClosingHank = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ShiftTime = table.Column<double>(type: "float", nullable: false),
                    RunTime = table.Column<double>(type: "float", nullable: false),
                    IdleTime = table.Column<double>(type: "float", nullable: false),
                    DelHank = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalProduction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductionEfficiency = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Lap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mixing = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NoOfDoffs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConeWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpeningKgs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Closing = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SliverBreaks = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpectedProduction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductionDrop = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productions_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productions_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productions_Products_ProductInId",
                        column: x => x.ProductInId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productions_Products_ProductOutId",
                        column: x => x.ProductOutId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productions_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WasteType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    IsSellable = table.Column<bool>(type: "bit", nullable: false),
                    ProductionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WasteType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WasteType_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WasteType_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Wastes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WasteTypeId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wastes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wastes_WasteType_WasteTypeId",
                        column: x => x.WasteTypeId,
                        principalTable: "WasteType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Mixing", "Mixing" },
                    { 2, "Blowroom", "Blowroom" },
                    { 3, "Carding", "Carding" },
                    { 4, "Drawing Breaker", "Drawing Breaker" },
                    { 5, "Drawing Finisher", "Drawing Finisher" },
                    { 6, "Simplex", "Simplex" },
                    { 7, "Spinning", "Spinning" },
                    { 8, "Cone Winding", "Cone Winding" },
                    { 9, "Autoconer", "Autoconer" },
                    { 10, "Packing", "Packing" },
                    { 11, "Sweeping", "Sweeping" },
                    { 12, "Extra Work", "Extra Work" },
                    { 13, "Others", "Others" }
                });

            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "Id", "EffectiveDate", "EndTime1", "EndTime2", "EndTime3", "EndTime4", "StartTime1", "StartTime2", "StartTime3", "StartTime4", "TotalShifts" },
                values: new object[] { 1, new DateOnly(2024, 1, 1), new TimeOnly(20, 0, 0), new TimeOnly(8, 0, 0), null, null, new TimeOnly(8, 0, 0), new TimeOnly(20, 0, 0), null, null, 2 });

            migrationBuilder.InsertData(
                table: "MachineTypes",
                columns: new[] { "Id", "DepartmentId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Bale Plucker" },
                    { 2, 2, "Blowroom" },
                    { 3, 3, "Carding" },
                    { 4, 4, "Drawing Breaker" },
                    { 5, 5, "Drawing Finisher" },
                    { 6, 6, "Simplex" },
                    { 7, 7, "Spinning" },
                    { 8, 8, "Cone Winding" },
                    { 9, 9, "Autoconer" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "DepartmentId", "Description", "Name", "Tradable" },
                values: new object[,]
                {
                    { 1, 13, null, "Cotton", false },
                    { 2, 3, null, "Carding Sliver", false },
                    { 3, 4, null, "Breaker Sliver", false },
                    { 4, 5, null, "Finisher Sliver", false },
                    { 5, 6, null, "Roving", false },
                    { 6, 7, null, "Spin Yarn", false },
                    { 7, 8, null, "Winded Yarn", false },
                    { 8, 9, null, "Autoconed Yarn", false },
                    { 9, 10, null, "Bag", false },
                    { 10, 1, null, "Mixed Bale", false }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "NetWeight", "ProductTypeId", "SetHank" },
                values: new object[,]
                {
                    { 1, null, "Cotton", 0.0m, 1, "" },
                    { 2, null, "Mixed Bale", 0.0m, 10, "" },
                    { 3, null, "Carding Sliver 0.092ne", 0.0m, 2, "0.092ne" },
                    { 4, null, "Breaker Sliver 0.095ne", 0.0m, 3, "0.095ne" },
                    { 5, null, "Finisher Sliver 0.097ne", 0.0m, 4, "0.097ne" },
                    { 6, null, "Roving 0.95ne", 0.0m, 5, "0.95ne" },
                    { 7, null, "Spin Yarn 60s", 0.0m, 6, "60s" },
                    { 8, null, "Autoconed Yarn 1.5Kgs 60s", 1.5m, 8, "60s" },
                    { 9, null, "Winded Yarn 1.5Kgs 60s", 1.5m, 7, "60s" },
                    { 10, null, "Autoconed Bag 60Kgs 60s", 60m, 9, "60s" },
                    { 11, null, "Winded Bag 60Kgs 60s", 60m, 9, "60s" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Machines_DepartmentId",
                table: "Machines",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_MachineTypeId",
                table: "Machines",
                column: "MachineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineTypes_DepartmentId",
                table: "MachineTypes",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_DepartmentId",
                table: "Productions",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_MachineId",
                table: "Productions",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_ProductInId",
                table: "Productions",
                column: "ProductInId");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_ProductionDate",
                table: "Productions",
                column: "ProductionDate");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_ProductOutId",
                table: "Productions",
                column: "ProductOutId");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_ShiftId",
                table: "Productions",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_DepartmentId",
                table: "ProductTypes",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Wastes_WasteTypeId",
                table: "Wastes",
                column: "WasteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WasteType_DepartmentId",
                table: "WasteType",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_WasteType_ProductionId",
                table: "WasteType",
                column: "ProductionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wastes");

            migrationBuilder.DropTable(
                name: "WasteType");

            migrationBuilder.DropTable(
                name: "Productions");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "MachineTypes");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
