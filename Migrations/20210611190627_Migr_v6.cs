using Microsoft.EntityFrameworkCore.Migrations;

namespace PrzepisyWeb.Migrations
{
    public partial class Migr_v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CounterLike",
                table: "LikeDislikeList");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CounterLike",
                table: "LikeDislikeList",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
