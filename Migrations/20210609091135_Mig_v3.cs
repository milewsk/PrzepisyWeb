using Microsoft.EntityFrameworkCore.Migrations;

namespace PrzepisyWeb.Migrations
{
    public partial class Mig_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikeCounter",
                table: "Recipes");

            migrationBuilder.CreateTable(
                name: "LikeDislikeList",
                columns: table => new
                {
                    RecipeID = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: false),
                    LikeID = table.Column<string>(nullable: true),
                    CounterLike = table.Column<int>(nullable: false),
                    Like = table.Column<bool>(nullable: false),
                    Dislike = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeDislikeList", x => new { x.RecipeID, x.UserID });
                    table.ForeignKey(
                        name: "FK_LikeDislikeList_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeDislikeList_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikeDislikeList_UserID",
                table: "LikeDislikeList",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeDislikeList");

            migrationBuilder.AddColumn<int>(
                name: "LikeCounter",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
