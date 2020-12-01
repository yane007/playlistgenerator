using Microsoft.EntityFrameworkCore.Migrations;

namespace PG.Data.Migrations
{
    public partial class UserNotBanned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b32cc6d-2fc9-4808-a0a6-b3877bf9a381",
                column: "ConcurrencyStamp",
                value: "d74e3c1c-8fc2-487d-ad95-b842a5c820aa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93ad4deb-b9f7-4a98-9585-8b79963aee55",
                column: "ConcurrencyStamp",
                value: "5e1a15f9-2a98-4c38-923b-9d0c5ef1b0b8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ed79f14-fece-48f1-806e-f04fdbdf3baf", "AQAAAAEAACcQAAAAEMnJ7uLjkIJBp6UUP4/P9qWcu0989cvxaG1CKfW8zkAHPOJPO/zzFr43iBEYwIq5XQ==", "123486ca-df51-4bd8-a3ca-51af330196ff" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b32cc6d-2fc9-4808-a0a6-b3877bf9a381",
                column: "ConcurrencyStamp",
                value: "3e8b422c-841f-4d8a-983a-eb77c603c0ef");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93ad4deb-b9f7-4a98-9585-8b79963aee55",
                column: "ConcurrencyStamp",
                value: "49dbdee5-1374-4e80-8c6f-8b72a862e86c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a719c6f9-6d5b-43e3-9aee-d3a22c44a1e2", "AQAAAAEAACcQAAAAEK4GL6WS5JG67Wdabogm96/9ivqW/HvT5Ms9tTZyFxktYDjETH0YEEXHFLyaoWuizQ==", "d54a7982-bd2d-42d6-ba99-aefdc7dabb09" });
        }
    }
}
