using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoRepairCRM.Database.Migrations
{
    public partial class ActiveAccountStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole1",
                column: "ConcurrencyStamp",
                value: "2d36ea20-8171-4792-ae04-802506b48edd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole3",
                column: "ConcurrencyStamp",
                value: "cee05f54-306b-4eaf-9cba-8b849c6c65b8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole4",
                column: "ConcurrencyStamp",
                value: "cc956113-d6fe-4aa4-a724-b5ffb7b76250");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole5",
                column: "ConcurrencyStamp",
                value: "71707bdf-c231-4771-b0eb-ae6566c0b41d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "MockUser1",
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5b0a93b2-6e67-4d7e-a356-d034d7f185ef", true, "AQAAAAEAACcQAAAAENYTXn1YxipuTIb33TvBPKeSHLiq5BV1n3qXj+KX1xmGW0e0etqMwEPAblHxHG59kQ==", "e44c92bf-1b13-4e53-98a5-89ff9a873508" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "MockUser2",
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f591377f-adb8-4776-ae9a-9165b7e90f0d", true, "AQAAAAEAACcQAAAAELYFW1b6tSGT/Qhpiju9r+chQYAvg/znHF0s8GsFo0/S/tt6CLpCsoWIEQRpfKbLNQ==", "052d90f3-faf5-4e0e-b156-203bf971d7cd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "MockUser3",
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a0667d6-5c7c-4c72-9b32-bce689076d3a", true, "AQAAAAEAACcQAAAAED3aN9Utskz+iRUVPayM0FgYJv+D5d/Ahk/phyV5uJeeHYOjvsuG0Raw77Kpo4QVLg==", "d9c1c299-28e3-4adc-b117-586855963488" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole1",
                column: "ConcurrencyStamp",
                value: "edb9dcd8-8ff8-4faa-bf85-170e7bee3953");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole3",
                column: "ConcurrencyStamp",
                value: "187b3b21-332c-4889-a089-c7b3993db8a5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole4",
                column: "ConcurrencyStamp",
                value: "17b8fd03-417b-4e05-ae57-ce36e74bf66a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole5",
                column: "ConcurrencyStamp",
                value: "25dd6be0-73c2-4201-820d-f2892133e954");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "MockUser1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1f06371-35b4-455b-9b46-5f8a07194e6a", "AQAAAAEAACcQAAAAEDhDX4hV4mHckyITxyCQujeRv9k4wleY18V1c1k/q5sybKQ3LKEQ6+jFOvcZWKaZpg==", "cd38a389-8f37-4ee2-93d3-96167670166b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "MockUser2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3c9d3dc-e10f-4ffe-819a-25934f523350", "AQAAAAEAACcQAAAAEEJDSdDVDuIjG07WMLjTeS+Qr5FePL1yjK1VTWwc+VNKaej856dc595EM71CjXXVCA==", "1084549e-76e9-4420-9ab1-52052ac26f00" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "MockUser3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8efb89f6-ba52-43fe-8808-65475afb267a", "AQAAAAEAACcQAAAAEHIEFGc3DXjokDFURE0q8jt1xqXP9l5e5njonTLZ4n8S1QUdn+GMPXJQ8VaMy3WU8g==", "ca82df4b-6fbf-4664-a407-2f99d3f886c4" });
        }
    }
}
