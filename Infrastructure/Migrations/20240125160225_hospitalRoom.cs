using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class hospitalRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentRooms_TreatmentMachines_TreatmentMachineId",
                table: "TreatmentRooms");

            migrationBuilder.AlterColumn<int>(
                name: "TreatmentMachineId",
                table: "TreatmentRooms",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentRooms_TreatmentMachines_TreatmentMachineId",
                table: "TreatmentRooms",
                column: "TreatmentMachineId",
                principalTable: "TreatmentMachines",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentRooms_TreatmentMachines_TreatmentMachineId",
                table: "TreatmentRooms");

            migrationBuilder.AlterColumn<int>(
                name: "TreatmentMachineId",
                table: "TreatmentRooms",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentRooms_TreatmentMachines_TreatmentMachineId",
                table: "TreatmentRooms",
                column: "TreatmentMachineId",
                principalTable: "TreatmentMachines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
