using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreMVC.Migrations
{
    public partial class ads2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Ads_AdId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_AdId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "AdId",
                table: "Genres");

            migrationBuilder.AddColumn<List<int>>(
                name: "GenresId",
                table: "Ads",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenresId",
                table: "Ads");

            migrationBuilder.AddColumn<int>(
                name: "AdId",
                table: "Genres",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_AdId",
                table: "Genres",
                column: "AdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Ads_AdId",
                table: "Genres",
                column: "AdId",
                principalTable: "Ads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
