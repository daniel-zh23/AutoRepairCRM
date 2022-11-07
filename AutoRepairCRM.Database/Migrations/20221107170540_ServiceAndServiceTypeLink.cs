using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoRepairCRM.Database.Migrations
{
    public partial class ServiceAndServiceTypeLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LicensePlate",
                table: "CustomersCars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceTypeId",
                table: "Services",
                column: "ServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceTypes_ServiceTypeId",
                table: "Services",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceTypes_ServiceTypeId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ServiceTypeId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "LicensePlate",
                table: "CustomersCars");
        }
    }
}
