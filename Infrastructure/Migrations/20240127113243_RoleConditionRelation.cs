using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RoleConditionRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConditionId",
                table: "Roles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ConditionId",
                table: "Roles",
                column: "ConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Conditions_ConditionId",
                table: "Roles",
                column: "ConditionId",
                principalTable: "Conditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Conditions_ConditionId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_ConditionId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ConditionId",
                table: "Roles");
        }
    }
}
