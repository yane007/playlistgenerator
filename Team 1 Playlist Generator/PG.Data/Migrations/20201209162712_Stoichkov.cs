using Microsoft.EntityFrameworkCore.Migrations;

namespace PG.Data.Migrations
{
    public partial class Stoichkov : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoichkovHotel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoichkovHotel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoichkovRoom",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RoomNumber = table.Column<int>(nullable: false),
                    type = table.Column<string>(nullable: true),
                    HotelId = table.Column<int>(nullable: false),
                    StoichkovHotelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoichkovRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoichkovRoom_StoichkovHotel_StoichkovHotelId",
                        column: x => x.StoichkovHotelId,
                        principalTable: "StoichkovHotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b32cc6d-2fc9-4808-a0a6-b3877bf9a381",
                column: "ConcurrencyStamp",
                value: "7fcecb5e-34ca-4735-baf7-07a7a22cc0bb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93ad4deb-b9f7-4a98-9585-8b79963aee55",
                column: "ConcurrencyStamp",
                value: "db2f24e2-9de7-43e1-8ac7-1c5dc8b33abe");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "36a0a48f-3d9c-4746-a563-ea27c1af93c0", "AQAAAAEAACcQAAAAEI09JPiIjVO8sRqdGnRhn6SiqDuubk1ODlYB82dbsm8vn8548V+fscqShkIYQiGoug==", "3be63ff8-7ab5-401a-9c0c-e10748632e4f" });

            migrationBuilder.CreateIndex(
                name: "IX_StoichkovRoom_StoichkovHotelId",
                table: "StoichkovRoom",
                column: "StoichkovHotelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoichkovRoom");

            migrationBuilder.DropTable(
                name: "StoichkovHotel");

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
    }
}
