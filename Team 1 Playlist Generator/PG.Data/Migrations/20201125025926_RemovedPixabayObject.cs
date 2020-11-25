using Microsoft.EntityFrameworkCore.Migrations;

namespace PG.Data.Migrations
{
    public partial class RemovedPixabayObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PixabayImage");

            migrationBuilder.DropColumn(
                name: "PixabayId",
                table: "Playlists");

            migrationBuilder.AddColumn<string>(
                name: "PixabayImage",
                table: "Playlists",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b32cc6d-2fc9-4808-a0a6-b3877bf9a381",
                column: "ConcurrencyStamp",
                value: "9d304237-c43d-404c-8347-fae049709634");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93ad4deb-b9f7-4a98-9585-8b79963aee55",
                column: "ConcurrencyStamp",
                value: "6e02ea2b-d182-4cab-960e-731c9e209664");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5dfedde4-7ad5-45b0-adf2-e9da0ce2f3c3", "AQAAAAEAACcQAAAAEAuNfiwlzu398uK9/KpHXS3hkiJiSh4im4nuhb8Bv4GHoIhKtOmuGcjPoA2fqjT1lw==", "596653e8-9e6d-4338-945d-10d55a9609d0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PixabayImage",
                table: "Playlists");

            migrationBuilder.AddColumn<int>(
                name: "PixabayId",
                table: "Playlists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PixabayImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LargeImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaylistId = table.Column<int>(type: "int", nullable: false),
                    PreviewURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebformatURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PixabayImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PixabayImage_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b32cc6d-2fc9-4808-a0a6-b3877bf9a381",
                column: "ConcurrencyStamp",
                value: "2766b1ab-b6f0-4132-a2c9-0fbdd208036f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93ad4deb-b9f7-4a98-9585-8b79963aee55",
                column: "ConcurrencyStamp",
                value: "c0e5655e-3d60-456a-9429-82e3a8537207");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e39830ae-e806-4aad-adde-d6f954dc5a5a", "AQAAAAEAACcQAAAAEHeK8Roqx572SKiqVUezW0RngjcR8tnRHWGzVAVayMtr+p8BgL6NEiKJrhMk3qoHxw==", "809020b9-5a89-4045-b595-44a3adcdcb37" });

            migrationBuilder.CreateIndex(
                name: "IX_PixabayImage_PlaylistId",
                table: "PixabayImage",
                column: "PlaylistId",
                unique: true);
        }
    }
}
