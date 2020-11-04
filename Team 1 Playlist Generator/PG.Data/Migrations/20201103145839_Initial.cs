using Microsoft.EntityFrameworkCore.Migrations;

namespace PG.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 30, nullable: false),
                    Cover = table.Column<string>(nullable: true),
                    Cover_small = table.Column<string>(nullable: true),
                    Cover_medium = table.Column<string>(nullable: true),
                    Cover_big = table.Column<string>(nullable: true),
                    Cover_xl = table.Column<string>(nullable: true),
                    Md5_image = table.Column<string>(nullable: true),
                    Tracklist = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Creators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Tracklist = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Duration = table.Column<int>(nullable: false),
                    Is_loved_track = table.Column<bool>(nullable: false),
                    Collaborative = table.Column<bool>(nullable: false),
                    Nb_tracks = table.Column<int>(nullable: false),
                    Fans = table.Column<int>(nullable: false),
                    Link = table.Column<string>(nullable: true),
                    Share = table.Column<string>(nullable: true),
                    picture = table.Column<string>(nullable: true),
                    Picture_small = table.Column<string>(nullable: true),
                    Picture_medium = table.Column<string>(nullable: true),
                    Picture_big = table.Column<string>(nullable: true),
                    Picture_xl = table.Column<string>(nullable: true),
                    Checksum = table.Column<string>(nullable: true),
                    Tracklist = table.Column<string>(nullable: true),
                    Creation_date = table.Column<string>(nullable: true),
                    Md5_image = table.Column<string>(nullable: true),
                    CreatorId = table.Column<int>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Playlists_Creators_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Creators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Playlists_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Readable = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Title_short = table.Column<string>(nullable: true),
                    Title_version = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Duration = table.Column<int>(nullable: false),
                    Rank = table.Column<int>(nullable: false),
                    Explicit_lyrics = table.Column<bool>(nullable: false),
                    Explicit_content_lyrics = table.Column<int>(nullable: false),
                    Explicit_content_cover = table.Column<int>(nullable: false),
                    Preview = table.Column<string>(nullable: true),
                    Md5_image = table.Column<string>(nullable: true),
                    Time_add = table.Column<int>(nullable: false),
                    ArtistId = table.Column<int>(nullable: true),
                    AlbumId = table.Column<int>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    PlaylistId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Songs_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Songs_Creators_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Creators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Songs_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_CreatorId",
                table: "Playlists",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_UserId",
                table: "Playlists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_AlbumId",
                table: "Songs",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ArtistId",
                table: "Songs",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_PlaylistId",
                table: "Songs",
                column: "PlaylistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Creators");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
