using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreMVC.Migrations
{
    public partial class adUploadTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UploadTime",
                table: "Ads",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadTime",
                table: "Ads");
        }
    }
}
