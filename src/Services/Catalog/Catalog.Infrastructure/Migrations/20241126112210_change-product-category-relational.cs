using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeproductcategoryrelational : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_products_tb_categories_CategoryId",
                table: "tb_products");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "tb_products",
                newName: "CategoryGenderId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_products_CategoryId",
                table: "tb_products",
                newName: "IX_tb_products_CategoryGenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_products_tb_category_gender_CategoryGenderId",
                table: "tb_products",
                column: "CategoryGenderId",
                principalTable: "tb_category_gender",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_products_tb_category_gender_CategoryGenderId",
                table: "tb_products");

            migrationBuilder.RenameColumn(
                name: "CategoryGenderId",
                table: "tb_products",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_products_CategoryGenderId",
                table: "tb_products",
                newName: "IX_tb_products_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_products_tb_categories_CategoryId",
                table: "tb_products",
                column: "CategoryId",
                principalTable: "tb_categories",
                principalColumn: "Id");
        }
    }
}
