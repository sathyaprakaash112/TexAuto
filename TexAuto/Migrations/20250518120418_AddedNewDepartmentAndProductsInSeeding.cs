using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TexAuto.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewDepartmentAndProductsInSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Cone Winding", "Cone Winding" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 13, "Others", "Others" });

            migrationBuilder.UpdateData(
                table: "MachineTypes",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Cone Winding");

            migrationBuilder.UpdateData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DepartmentId",
                value: 13);

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "DepartmentId", "Description", "Name" },
                values: new object[] { 10, 1, null, "Mixed Bale" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Cotton");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "ProductTypeId" },
                values: new object[] { "Mixed Bale", 10 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "ProductTypeId" },
                values: new object[] { "Carding Sliver", 2 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "ProductTypeId" },
                values: new object[,]
                {
                    { 4, null, "Breaker Sliver", 3 },
                    { 5, null, "Finisher Sliver", 4 },
                    { 6, null, "Roving", 5 },
                    { 7, null, "Spin Yarn", 6 },
                    { 8, null, "Autoconed Yarn", 8 },
                    { 9, null, "Winded Yarn", 7 },
                    { 10, null, "Autoconed Bag", 9 },
                    { 11, null, "Winded Bag", 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Cone Winsing", "Cone Winsing" });

            migrationBuilder.UpdateData(
                table: "MachineTypes",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Cone Winsing");

            migrationBuilder.UpdateData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DepartmentId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Cotton Raw");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "ProductTypeId" },
                values: new object[] { "Sliver Type A", 2 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "ProductTypeId" },
                values: new object[] { "Yarn 30s", 6 });
        }
    }
}
