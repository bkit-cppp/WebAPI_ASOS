using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_categories_tb_genders_GenderId",
                table: "tb_categories");

            migrationBuilder.DropIndex(
                name: "IX_tb_categories_GenderId",
                table: "tb_categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tb_categories_GenderId",
                table: "tb_categories",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_categories_tb_genders_GenderId",
                table: "tb_categories",
                column: "GenderId",
                principalTable: "tb_genders",
                principalColumn: "Id");
        }
    }
}
