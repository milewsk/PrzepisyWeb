using Microsoft.EntityFrameworkCore.Migrations;

namespace PrzepisyWeb.Migrations
{
    public partial class Mig_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LikeCounter",
                table: "Recipes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikeCounter",
                table: "Recipes");
        }
    }
}
