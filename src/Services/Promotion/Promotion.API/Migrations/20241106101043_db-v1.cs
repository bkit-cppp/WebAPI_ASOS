using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promotion.API.Migrations
{
    /// <inheritdoc />
    public partial class dbv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "tb_discounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "tb_discounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "tb_discounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "tb_discounts");

            migrationBuilder.DropColumn(
                name: "Condition",
                table: "tb_discounts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "tb_discounts");
        }
    }
}
