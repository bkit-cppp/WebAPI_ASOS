using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promotion.API.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_discount_types",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_discount_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_discounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    Minimum = table.Column<decimal>(type: "numeric", nullable: false),
                    DiscountTypeId = table.Column<string>(type: "text", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_discounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_discounts_tb_discount_types_DiscountTypeId",
                        column: x => x.DiscountTypeId,
                        principalTable: "tb_discount_types",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_discount_histories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiscountApplied = table.Column<decimal>(type: "numeric", nullable: false),
                    DiscountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_discount_histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_discount_histories_tb_discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "tb_discounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_discount_products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiscountId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_discount_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_discount_products_tb_discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "tb_discounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_discount_histories_DiscountId",
                table: "tb_discount_histories",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_discount_products_DiscountId",
                table: "tb_discount_products",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_discounts_DiscountTypeId",
                table: "tb_discounts",
                column: "DiscountTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_discount_histories");

            migrationBuilder.DropTable(
                name: "tb_discount_products");

            migrationBuilder.DropTable(
                name: "tb_discounts");

            migrationBuilder.DropTable(
                name: "tb_discount_types");
        }
    }
}
