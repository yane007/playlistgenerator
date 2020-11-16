using Microsoft.EntityFrameworkCore.Migrations;

namespace PG.Data.Migrations
{
    public partial class SongUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Playlists_PlaylistId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_PlaylistId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "PlaylistId",
                table: "Genres");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Songs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Songs");

            migrationBuilder.AddColumn<int>(
                name: "PlaylistId",
                table: "Genres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_PlaylistId",
                table: "Genres",
                column: "PlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Playlists_PlaylistId",
                table: "Genres",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
