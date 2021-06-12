using Microsoft.EntityFrameworkCore.Migrations;

namespace PrzepisyWeb.Migrations
{
    public partial class Migracja_test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerUserName",
                table: "Recipes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerUserName",
                table: "Recipes");
        }
    }
}
