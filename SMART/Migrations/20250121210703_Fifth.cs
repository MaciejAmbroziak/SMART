using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMART.Migrations
{
    /// <inheritdoc />
    public partial class Fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessEquipments_EquipmentContracts_EquipmentContractId",
                table: "ProcessEquipments");

            migrationBuilder.DropIndex(
                name: "IX_ProcessEquipments_EquipmentContractId",
                table: "ProcessEquipments");

            migrationBuilder.DropColumn(
                name: "EquipmentContractId",
                table: "ProcessEquipments");

            migrationBuilder.CreateTable(
                name: "ProcessEquipmentEquipmentContracts",
                columns: table => new
                {
                    EquipmentContractsId = table.Column<int>(type: "int", nullable: false),
                    ProcessEquipmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessEquipmentEquipmentContracts", x => new { x.EquipmentContractsId, x.ProcessEquipmentId });
                    table.ForeignKey(
                        name: "FK_ProcessEquipmentEquipmentContracts_EquipmentContracts_EquipmentContractsId",
                        column: x => x.EquipmentContractsId,
                        principalTable: "EquipmentContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessEquipmentEquipmentContracts_ProcessEquipments_ProcessEquipmentId",
                        column: x => x.ProcessEquipmentId,
                        principalTable: "ProcessEquipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessEquipmentEquipmentContracts_ProcessEquipmentId",
                table: "ProcessEquipmentEquipmentContracts",
                column: "ProcessEquipmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessEquipmentEquipmentContracts");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentContractId",
                table: "ProcessEquipments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessEquipments_EquipmentContractId",
                table: "ProcessEquipments",
                column: "EquipmentContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessEquipments_EquipmentContracts_EquipmentContractId",
                table: "ProcessEquipments",
                column: "EquipmentContractId",
                principalTable: "EquipmentContracts",
                principalColumn: "Id");
        }
    }
}
