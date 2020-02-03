using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreMVC.Migrations
{
    public partial class ads1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Genres_GenreId",
                table: "Ads");

            migrationBuilder.DropIndex(
                name: "IX_Ads_GenreId",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Ads");

            migrationBuilder.AddColumn<int>(
                name: "AdId",
                table: "Genreses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_AdId",
                table: "Genreses",
                column: "AdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Ads_AdId",
                table: "Genreses",
                column: "AdId",
                principalTable: "Ads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Ads_AdId",
                table: "Genreses");

            migrationBuilder.DropIndex(
                name: "IX_Genres_AdId",
                table: "Genreses");

            migrationBuilder.DropColumn(
                name: "AdId",
                table: "Genreses");

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Ads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ads_GenreId",
                table: "Ads",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Genres_GenreId",
                table: "Ads",
                column: "GenreId",
                principalTable: "Genreses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
