using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carservice.Migrations
{
    /// <inheritdoc />
    public partial class relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "RepairRequests");

            migrationBuilder.AddColumn<int>(
                name: "RequestStatusId",
                table: "RepairRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequests_RequestStatusId",
                table: "RepairRequests",
                column: "RequestStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequests_RequestStatuses_RequestStatusId",
                table: "RepairRequests",
                column: "RequestStatusId",
                principalTable: "RequestStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequests_RequestStatuses_RequestStatusId",
                table: "RepairRequests");

            migrationBuilder.DropIndex(
                name: "IX_RepairRequests_RequestStatusId",
                table: "RepairRequests");

            migrationBuilder.DropColumn(
                name: "RequestStatusId",
                table: "RepairRequests");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "RepairRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
