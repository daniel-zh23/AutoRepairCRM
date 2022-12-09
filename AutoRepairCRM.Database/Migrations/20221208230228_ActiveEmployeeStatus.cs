using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoRepairCRM.Database.Migrations
{
    public partial class ActiveEmployeeStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole2");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Employees");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole1",
                column: "ConcurrencyStamp",
                value: "0c072d40-c7b9-4984-845b-e641d04ebead");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole3",
                column: "ConcurrencyStamp",
                value: "7f403fd2-9353-4826-9f79-def794d3571a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole4",
                column: "ConcurrencyStamp",
                value: "6cb44d82-4c82-4526-8590-022292614b1f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "MockRole5",
                column: "ConcurrencyStamp",
                value: "368723bd-3020-44eb-88f7-b7b88b1cd075");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "MockRole2", "0f855d5d-bbce-4960-9e49-c1957af8e588", "Manager", "MANAGER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "MockUser1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff6667d9-9af8-4c0e-bf3b-a7e9900c85bd", "AQAAAAEAACcQAAAAEOHzmV+IhxU8hYsuTdBiFtpi5pl2f7nSHvlIYXS2f9G37WfZXAO5Ju3CZFJ4TCoirw==", "0149e2c4-9895-421d-aaa2-8caa21a197c5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "MockUser2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d60eb499-0f6c-4e24-af02-1c1e7aef40fd", "AQAAAAEAACcQAAAAEBFxBxPJGjxHSZZ7aBc7gakkQhgFHb+4kaBwOeiQBfZ1UOLdA2kCnYRQgu1cVoqWyA==", "7555aa3c-8b61-4827-8aad-9632a4f1701e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "MockUser3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f51d982c-2b89-4565-994c-c236316a583b", "AQAAAAEAACcQAAAAEK8jEv2/U2S9EOkL+3JayvTlZtfNQQjEs31dJBSPzoLPTby6acaDeAVMnrEaWf4SYQ==", "6fb646bf-34a1-48bf-af82-79f07bcdc52e" });
        }
    }
}
