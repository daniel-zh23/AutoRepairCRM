using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoRepairCRM.Database.Migrations
{
    public partial class AdminConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dea12856-c198-4129-b3f3-b893d8395082", 0, "d35d1f8b-b93d-4196-9a0f-6d0df2d29957", "ApplicationUser", "admin@abv.bg", true, "Admin", "Admin", false, null, "ADMIN@ABV.BG", "ADMIN@ABV.BG", "AQAAAAEAACcQAAAAECwrKNZ4/TajNDgp8+0jD35YKflRCnse3heUuwlVkcLGzXExnKejDRasQs9uWhwvmg==", null, false, "44bc75f3-55b2-4eaf-b5fd-061e58238c80", false, "admin@abv.bg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "Email", "EmailConfirmed", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "97655e48-276a-4495-8d8b-c18d2b4653e1", "admin@abv.com", false, "AQAAAAEAACcQAAAAEDwQ3xJg24gNPnqzkhoN6jineHVtKr17yBLSYxA98Lhx79S5Hy9aiaFFO8qDmM30wg==", "23e0a966-d32a-4263-a65b-ae5b522a96be", "admin@abv.com" });
        }
    }
}
