using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class productprice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSale",
                table: "tb_product_items");

            migrationBuilder.DropColumn(
                name: "OriginalPrice",
                table: "tb_product_items");

            migrationBuilder.RenameColumn(
                name: "SalePrice",
                table: "tb_product_items",
                newName: "AdditionalPrice");

            migrationBuilder.AddColumn<bool>(
                name: "IsSale",
                table: "tb_products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "OriginalPrice",
                table: "tb_products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "tb_products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSale",
                table: "tb_products");

            migrationBuilder.DropColumn(
                name: "OriginalPrice",
                table: "tb_products");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "tb_products");

            migrationBuilder.RenameColumn(
                name: "AdditionalPrice",
                table: "tb_product_items",
                newName: "SalePrice");

            migrationBuilder.AddColumn<bool>(
                name: "IsSale",
                table: "tb_product_items",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "OriginalPrice",
                table: "tb_product_items",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
