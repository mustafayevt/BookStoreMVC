using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BookStoreMVC.Migrations
{
    public partial class ads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Genres_ParentId",
                table: "Genres");

            migrationBuilder.DropTable(
                name: "UserAds");

            migrationBuilder.DropIndex(
                name: "IX_Genres_ParentId",
                table: "Genres");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Ads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ads_UserId",
                table: "Ads",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_AspNetUsers_UserId",
                table: "Ads",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_AspNetUsers_UserId",
                table: "Ads");

            migrationBuilder.DropIndex(
                name: "IX_Ads_UserId",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Ads");

            migrationBuilder.CreateTable(
                name: "UserAds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AdId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAds_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAds_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_ParentId",
                table: "Genres",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAds_AdId",
                table: "UserAds",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAds_UserId",
                table: "UserAds",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Genres_ParentId",
                table: "Genres",
                column: "ParentId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
