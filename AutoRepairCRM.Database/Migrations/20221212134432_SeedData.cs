using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoRepairCRM.Database.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole1",
                column: "ConcurrencyStamp",
                value: "cd0820b3-a399-4a8c-8b5c-49923f328635");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole3",
                column: "ConcurrencyStamp",
                value: "f8f92c0d-26ba-4210-8399-75ae62e76d99");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole4",
                column: "ConcurrencyStamp",
                value: "63ef8119-94ab-4e78-a48b-b0b989dfac1a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole5",
                column: "ConcurrencyStamp",
                value: "25a43118-a6b8-4b33-9e60-6eea19a48503");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "MockUser1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e6c3143-2940-4011-928b-704430f7a224", "AQAAAAEAACcQAAAAEFyb9pFh9fvizOQM90Z4e3377A/kXiLeDJIyGkEV/rN7qUH/2ZSPgWK2ElCAb8Hj3Q==", "646ca329-b322-4299-94a4-ccd8c0676a63" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "MockUser2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "87b27028-bd03-4756-a559-0b9afc8b6808", "AQAAAAEAACcQAAAAEDlxqm9kFSGVv+b3gtP2jEAI9KMyleiAMxttCh84dEw7Abq2X+NCL1wc8/GMgZ802Q==", "09823bed-d106-4ac8-9f71-b23a836d1e0b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "MockUser3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f302e6a-ddff-424a-83cd-6d165b03e862", "AQAAAAEAACcQAAAAEJk4R9TlzJ3ds+UDqnGDyJLDA+LvPcqUiG9e3SzocELJFblLx32BDN2AImElm7a7lA==", "a8b0f855-7dc8-4c28-9baa-66783d082319" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "MockUser4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee6cab10-b4b7-4f96-bb2b-bfd88a153e18", "AQAAAAEAACcQAAAAEBs4e32XcQA82sXeJ+O4OD0AjSI/1raPUptQ1Y6uA5rWfa/LUCN9/D8BKrfedsK+eQ==", "80fc0b39-663b-47e9-acd2-03155b40a8a8" });

            migrationBuilder.InsertData(
                table: "ServicesEmployees",
                columns: new[] { "EmployeeId", "ServiceId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ServicesEmployees",
                keyColumns: new[] { "EmployeeId", "ServiceId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ServicesEmployees",
                keyColumns: new[] { "EmployeeId", "ServiceId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ServicesEmployees",
                keyColumns: new[] { "EmployeeId", "ServiceId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ServicesEmployees",
                keyColumns: new[] { "EmployeeId", "ServiceId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole1",
                column: "ConcurrencyStamp",
                value: "9b770585-56f2-4ffe-947e-9ff97643d11d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole3",
                column: "ConcurrencyStamp",
                value: "096888a4-7bcd-4adb-91fe-3234b70e85cc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole4",
                column: "ConcurrencyStamp",
                value: "64f0176f-1b31-49e7-9cc5-bb804d8bbd00");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole5",
                column: "ConcurrencyStamp",
                value: "65e7821a-e325-464c-ba85-192a718c5ee5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "MockUser1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aeaac57f-e5de-4734-a424-21cb91e9bb83", "AQAAAAEAACcQAAAAEL6rAiLGiREkkGkJd5hE+jwo5axL0FPd1aTnkRJzk2dYeqAVxYnOeJ4C0ISjpYkfcQ==", "d2cfb129-6d5a-4097-b1d3-f359d5bf8e6b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "MockUser2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b5d8fda0-7bab-4c68-9ed3-d3d69906ee2f", "AQAAAAEAACcQAAAAEIE/597M8GXRaTOyWUQGNyD2ttRd9s3uhpMfYceagrrC4rJQbRl1USA7AthG3Xrw7w==", "75b8fa34-a737-46e9-b9b4-0a07ba2cb928" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "MockUser3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a24b9a7-bec3-4dea-926c-156549d3878a", "AQAAAAEAACcQAAAAEOzQhAVa2V+27aPNhenhE8/KQ/QsFH3Iz2Nd36DW0QDO4xP3v+2BbFQhzBkLtgwK8g==", "74e07305-892a-45bb-8335-2a1633a480d2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "MockUser4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb217bbc-a52b-4502-99fe-ef20db3ed39e", "AQAAAAEAACcQAAAAEO/pJkfRi55mS3CDOuDDrvrI2cxtS8Eppxg0WHA+I+BO/x9S8znMHGaeS6Kdct2xmw==", "524baf30-f065-4694-a8c9-66093a5dd1da" });
        }
    }
}
