using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TexAuto.Migrations
{
    /// <inheritdoc />
    public partial class AddedLedger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnderGroupId = table.Column<int>(type: "int", nullable: true),
                    NatureOfGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffectsInventory = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupMaster_GroupMaster_UnderGroupId",
                        column: x => x.UnderGroupId,
                        principalTable: "GroupMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LedgerMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LedgerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupMasterId = table.Column<int>(type: "int", nullable: false),
                    LedgerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PinCode = table.Column<int>(type: "int", nullable: true),
                    GstNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    PanNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsTdsApplicable = table.Column<bool>(type: "bit", nullable: false),
                    OpeningBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BalanceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfscCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerMaster_GroupMaster_GroupMasterId",
                        column: x => x.GroupMasterId,
                        principalTable: "GroupMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupMaster_UnderGroupId",
                table: "GroupMaster",
                column: "UnderGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerMaster_GroupMasterId",
                table: "LedgerMaster",
                column: "GroupMasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LedgerMaster");

            migrationBuilder.DropTable(
                name: "GroupMaster");
        }
    }
}
