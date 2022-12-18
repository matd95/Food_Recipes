using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food_Recipes.Migrations
{
    public partial class Categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Recipe");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Recipe",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_CategoryID",
                table: "Recipe",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_Category_CategoryID",
                table: "Recipe",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_Category_CategoryID",
                table: "Recipe");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_CategoryID",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Recipe");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
