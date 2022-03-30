using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable CS1591

namespace AS91892.Data.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Genres",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Genres", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Images",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ImageTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Images", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "RecordLabels",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RecordLabels", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Albums",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AlbumCoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Year = table.Column<int>(type: "int", nullable: false),
                ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Albums", x => x.Id);
                table.ForeignKey(
                    name: "FK_Albums_Images_AlbumCoverId",
                    column: x => x.AlbumCoverId,
                    principalTable: "Images",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Songs",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                CoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Songs", x => x.Id);
                table.ForeignKey(
                    name: "FK_Songs_Albums_AlbumId",
                    column: x => x.AlbumId,
                    principalTable: "Albums",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Songs_Genres_GenreId",
                    column: x => x.GenreId,
                    principalTable: "Genres",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Songs_Images_CoverId",
                    column: x => x.CoverId,
                    principalTable: "Images",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Artists",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ArtistName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                LabelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                SongId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Artists", x => x.Id);
                table.ForeignKey(
                    name: "FK_Artists_RecordLabels_LabelId",
                    column: x => x.LabelId,
                    principalTable: "RecordLabels",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Artists_Songs_SongId",
                    column: x => x.SongId,
                    principalTable: "Songs",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Albums_AlbumCoverId",
            table: "Albums",
            column: "AlbumCoverId");

        migrationBuilder.CreateIndex(
            name: "IX_Albums_ArtistId",
            table: "Albums",
            column: "ArtistId");

        migrationBuilder.CreateIndex(
            name: "IX_Artists_LabelId",
            table: "Artists",
            column: "LabelId");

        migrationBuilder.CreateIndex(
            name: "IX_Artists_SongId",
            table: "Artists",
            column: "SongId");

        migrationBuilder.CreateIndex(
            name: "IX_Songs_AlbumId",
            table: "Songs",
            column: "AlbumId");

        migrationBuilder.CreateIndex(
            name: "IX_Songs_CoverId",
            table: "Songs",
            column: "CoverId");

        migrationBuilder.CreateIndex(
            name: "IX_Songs_GenreId",
            table: "Songs",
            column: "GenreId");

        migrationBuilder.AddForeignKey(
            name: "FK_Albums_Artists_ArtistId",
            table: "Albums",
            column: "ArtistId",
            principalTable: "Artists",
            principalColumn: "Id");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Albums_Artists_ArtistId",
            table: "Albums");

        migrationBuilder.DropTable(
            name: "Artists");

        migrationBuilder.DropTable(
            name: "RecordLabels");

        migrationBuilder.DropTable(
            name: "Songs");

        migrationBuilder.DropTable(
            name: "Albums");

        migrationBuilder.DropTable(
            name: "Genres");

        migrationBuilder.DropTable(
            name: "Images");
    }
}
