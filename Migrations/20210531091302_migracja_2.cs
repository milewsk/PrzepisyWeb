using Microsoft.EntityFrameworkCore.Migrations;

namespace PrzepisyWeb.Migrations
{
    public partial class migracja_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_IdentityUser_userId",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Recipes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_userId",
                table: "Recipes",
                newName: "IX_Recipes_UserId");

            migrationBuilder.CreateTable(
                name: "FavouriteRecipe",
                columns: table => new
                {
                    RecipeID = table.Column<int>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteRecipe", x => new { x.RecipeID, x.Id });
                    table.ForeignKey(
                        name: "FK_FavouriteRecipe_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteRecipe_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteRecipe_UserId",
                table: "FavouriteRecipe",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_IdentityUser_UserId",
                table: "Recipes",
                column: "UserId",
                principalTable: "IdentityUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_IdentityUser_UserId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "FavouriteRecipe");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Recipes",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes",
                newName: "IX_Recipes_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_IdentityUser_userId",
                table: "Recipes",
                column: "userId",
                principalTable: "IdentityUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
