using Microsoft.EntityFrameworkCore.Migrations;

namespace PrzepisyWeb.Migrations
{
    public partial class Migracja_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeCategories_Recipes_recipeID",
                table: "RecipeCategories");

            migrationBuilder.RenameColumn(
                name: "recipeID",
                table: "RecipeCategories",
                newName: "RecipeID");

            migrationBuilder.RenameColumn(
                name: "categoryName",
                table: "Categories",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "categoryID",
                table: "Categories",
                newName: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeCategories_Recipes_RecipeID",
                table: "RecipeCategories",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "RecipeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeCategories_Recipes_RecipeID",
                table: "RecipeCategories");

            migrationBuilder.RenameColumn(
                name: "RecipeID",
                table: "RecipeCategories",
                newName: "recipeID");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "categoryName");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Categories",
                newName: "categoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeCategories_Recipes_recipeID",
                table: "RecipeCategories",
                column: "recipeID",
                principalTable: "Recipes",
                principalColumn: "RecipeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
