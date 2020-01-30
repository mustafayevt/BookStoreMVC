using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreMVC.Migrations
{
    public partial class ads3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "Ads",
                newName: "GenresIds");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GenresIds",
                table: "Ads",
                newName: "GenresId");
        }
    }
}
