using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicHub.Backend.Migrations
{
    /// <inheritdoc />
    public partial class SongsField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Songs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreviewUrl",
                table: "Songs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpotifyId",
                table: "Songs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "PreviewUrl",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "SpotifyId",
                table: "Songs");
        }
    }
}
