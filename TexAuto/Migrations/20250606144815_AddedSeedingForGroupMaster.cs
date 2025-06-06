using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TexAuto.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeedingForGroupMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GroupMaster",
                columns: new[] { "Id", "AffectsInventory", "GroupName", "IsActive", "IsDefault", "NatureOfGroup", "Remarks", "UnderGroupId" },
                values: new object[,]
                {
                    { 1, false, "Capital Account", true, true, "Capital", null, null },
                    { 4, false, "Fixed Assets", true, true, "Asset", null, null },
                    { 10, false, "Investments", true, true, "Asset", null, null },
                    { 11, false, "Loans (Liability)", true, true, "Liability", null, null },
                    { 16, false, "Current Liabilities", true, true, "Liability", null, null },
                    { 24, false, "Provisions", true, true, "Liability", null, null },
                    { 27, false, "Bank Accounts", true, true, "Asset", null, null },
                    { 28, false, "Cash-in-Hand", true, true, "Asset", null, null },
                    { 31, false, "Deposits (Assets)", true, true, "Asset", null, null },
                    { 34, false, "Current Assets", true, true, "Asset", null, null },
                    { 40, true, "Stock-in-Hand", true, true, "Asset", null, null },
                    { 44, false, "Purchase Accounts", true, true, "Expense", null, null },
                    { 48, false, "Sales Accounts", true, true, "Income", null, null },
                    { 52, false, "Direct Expenses", true, true, "Expense", null, null },
                    { 57, false, "Indirect Expenses", true, true, "Expense", null, null },
                    { 62, false, "Indirect Incomes", true, true, "Income", null, null },
                    { 65, false, "Suspense Account", true, true, "Asset", null, null },
                    { 66, false, "Branch / Division", true, true, "Capital", null, null },
                    { 2, false, "Partner’s Capital", true, true, "Capital", null, 1 },
                    { 3, false, "Reserves & Surplus", true, true, "Capital", null, 1 },
                    { 5, false, "Building", true, true, "Asset", null, 4 },
                    { 6, false, "Machinery", true, true, "Asset", null, 4 },
                    { 7, false, "Vehicles", true, true, "Asset", null, 4 },
                    { 8, false, "Furniture & Fixtures", true, true, "Asset", null, 4 },
                    { 9, false, "Computer Equipment", true, true, "Asset", null, 4 },
                    { 12, false, "Secured Loans", true, true, "Liability", null, 11 },
                    { 15, false, "Unsecured Loans", true, true, "Liability", null, 11 },
                    { 17, false, "Sundry Creditors", true, true, "Liability", null, 16 },
                    { 18, false, "Duties & Taxes", true, true, "Liability", null, 16 },
                    { 22, false, "Outstanding Expenses", true, true, "Liability", null, 16 },
                    { 23, false, "Advance from Customers", true, true, "Liability", null, 16 },
                    { 25, false, "Salary Payable", true, true, "Liability", null, 24 },
                    { 26, false, "Bonus Payable", true, true, "Liability", null, 24 },
                    { 29, false, "Cash Office", true, true, "Asset", null, 28 },
                    { 30, false, "Petty Cash", true, true, "Asset", null, 28 },
                    { 32, false, "Rent Deposit", true, true, "Asset", null, 31 },
                    { 33, false, "Security Deposit", true, true, "Asset", null, 31 },
                    { 35, false, "Sundry Debtors", true, true, "Asset", null, 34 },
                    { 36, false, "Advance to Suppliers", true, true, "Asset", null, 34 },
                    { 37, false, "Prepaid Expenses", true, true, "Asset", null, 34 },
                    { 38, false, "Input GST Credit", true, true, "Asset", null, 34 },
                    { 39, false, "TDS Receivable", true, true, "Asset", null, 34 },
                    { 41, true, "Yarn", true, true, "Asset", null, 40 },
                    { 42, true, "Cotton Bale", true, true, "Asset", null, 40 },
                    { 43, true, "Wastage", true, true, "Asset", null, 40 },
                    { 45, false, "Yarn Purchase A/C", true, true, "Expense", null, 44 },
                    { 46, false, "Bale Purchase A/C", true, true, "Expense", null, 44 },
                    { 47, false, "Packing Material Purchase", true, true, "Expense", null, 44 },
                    { 49, false, "Yarn Sales", true, true, "Income", null, 48 },
                    { 50, false, "Bale Sales", true, true, "Income", null, 48 },
                    { 51, false, "Waste Sales", true, true, "Income", null, 48 },
                    { 53, false, "Freight Inward", true, true, "Expense", null, 52 },
                    { 54, false, "Loading Charges", true, true, "Expense", null, 52 },
                    { 55, false, "Commission Paid", true, true, "Expense", null, 52 },
                    { 56, false, "Transport Charges", true, true, "Expense", null, 52 },
                    { 58, false, "Rent", true, true, "Expense", null, 57 },
                    { 59, false, "Salary", true, true, "Expense", null, 57 },
                    { 60, false, "Office Expenses", true, true, "Expense", null, 57 },
                    { 61, false, "Misc. Expenses", true, true, "Expense", null, 57 },
                    { 63, false, "Discount Received", true, true, "Income", null, 62 },
                    { 64, false, "Interest Received", true, true, "Income", null, 62 },
                    { 67, false, "Unit 1", true, true, "Capital", null, 66 },
                    { 68, false, "Unit 2", true, true, "Capital", null, 66 },
                    { 13, false, "Bank Loans", true, true, "Liability", null, 12 },
                    { 14, false, "Vehicle Loans", true, true, "Liability", null, 12 },
                    { 19, false, "GST Payable", true, true, "Liability", null, 18 },
                    { 20, false, "TDS Payable", true, true, "Liability", null, 18 },
                    { 21, false, "TCS Payable", true, true, "Liability", null, 18 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "GroupMaster",
                keyColumn: "Id",
                keyValue: 16);
        }
    }
}
