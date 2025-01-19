using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMART.Migrations
{
    /// <inheritdoc />
    public partial class Fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentContracts_ProcessEquipments_ProcessEquipmentId",
                table: "EquipmentContracts");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentContracts_ProcessEquipmentId",
                table: "EquipmentContracts");

            migrationBuilder.DropColumn(
                name: "ProcessEquipmentId",
                table: "EquipmentContracts");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "ProcessEquipmentId",
                table: "EquipmentContracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentContracts_ProcessEquipmentId",
                table: "EquipmentContracts",
                column: "ProcessEquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentContracts_ProcessEquipments_ProcessEquipmentId",
                table: "EquipmentContracts",
                column: "ProcessEquipmentId",
                principalTable: "ProcessEquipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
