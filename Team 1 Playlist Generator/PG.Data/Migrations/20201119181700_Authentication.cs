using Microsoft.EntityFrameworkCore.Migrations;

namespace PG.Data.Migrations
{
    public partial class Authentication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b32cc6d-2fc9-4808-a0a6-b3877bf9a381",
                column: "ConcurrencyStamp",
                value: "b043f399-6b58-42a5-ab0d-587f89b8a287");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93ad4deb-b9f7-4a98-9585-8b79963aee55",
                column: "ConcurrencyStamp",
                value: "520dd893-610f-4e83-aad2-d4e723227e87");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "802c8789-1657-4eca-81e7-64c1af759f42", "AQAAAAEAACcQAAAAEOI0QzN62Prk1Esh/y3yfY6nmgh8IefaP4q0K9w9eOdu1IYfHo/2hQ5hxGjFGOakaA==", "e797d076-1d10-480d-9c91-ea9fa0f299f9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b32cc6d-2fc9-4808-a0a6-b3877bf9a381",
                column: "ConcurrencyStamp",
                value: "6fb76f06-44be-4c96-a52d-6e3952091d6c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93ad4deb-b9f7-4a98-9585-8b79963aee55",
                column: "ConcurrencyStamp",
                value: "58d9b593-1acb-4f69-8ac8-331b69579245");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f1f061d-3563-4882-ae8f-4c7840cdb50f", "AQAAAAEAACcQAAAAEIEX++CtPEHSVdOvaTeFM4VY/41N79ZETBoDUUtJ22oAiQ6CO2wuNC5bT6cvMUY3yA==", "4d9a96a7-30c7-4b8a-bfef-4cbbbee2498f" });
        }
    }
}
