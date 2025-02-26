using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_colors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_tb_colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_genders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_tb_genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_sizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_tb_sizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_user_comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Fullname = table.Column<string>(type: "text", nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_user_comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ImageFile = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    GenderId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_categories_tb_genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "tb_genders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AverageRating = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    BrandId = table.Column<Guid>(type: "uuid", nullable: true),
                    SizeAndFit = table.Column<string>(type: "text", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_products_tb_brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "tb_brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_products_tb_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tb_categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_size_category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    SizeId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_size_category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_size_category_tb_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tb_categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_size_category_tb_sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "tb_sizes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_comments_tb_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tb_products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_comments_tb_user_comments_UserId",
                        column: x => x.UserId,
                        principalTable: "tb_user_comments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_product_items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    ColorId = table.Column<Guid>(type: "uuid", nullable: true),
                    OriginalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    SalePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    IsSale = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_product_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_product_items_tb_colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "tb_colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_product_items_tb_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tb_products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    Rate = table.Column<int>(type: "integer", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_ratings_tb_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tb_products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_wishlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_wishlists_tb_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tb_products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_images_tb_product_items_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "tb_product_items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_variations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    SizeId = table.Column<Guid>(type: "uuid", nullable: true),
                    QtyDisplay = table.Column<int>(type: "integer", nullable: false),
                    QtyInStock = table.Column<int>(type: "integer", nullable: false),
                    Stock = table.Column<decimal>(type: "numeric", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_variations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_variations_tb_product_items_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "tb_product_items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_variations_tb_sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "tb_sizes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_categories_GenderId",
                table: "tb_categories",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_comments_ProductId",
                table: "tb_comments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_comments_UserId",
                table: "tb_comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_images_ProductItemId",
                table: "tb_images",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_product_items_ColorId",
                table: "tb_product_items",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_product_items_ProductId",
                table: "tb_product_items",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_products_BrandId",
                table: "tb_products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_products_CategoryId",
                table: "tb_products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_ratings_ProductId",
                table: "tb_ratings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_size_category_CategoryId",
                table: "tb_size_category",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_size_category_SizeId",
                table: "tb_size_category",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_variations_ProductItemId",
                table: "tb_variations",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_variations_SizeId",
                table: "tb_variations",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_wishlists_ProductId",
                table: "tb_wishlists",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_comments");

            migrationBuilder.DropTable(
                name: "tb_images");

            migrationBuilder.DropTable(
                name: "tb_ratings");

            migrationBuilder.DropTable(
                name: "tb_size_category");

            migrationBuilder.DropTable(
                name: "tb_variations");

            migrationBuilder.DropTable(
                name: "tb_wishlists");

            migrationBuilder.DropTable(
                name: "tb_user_comments");

            migrationBuilder.DropTable(
                name: "tb_product_items");

            migrationBuilder.DropTable(
                name: "tb_sizes");

            migrationBuilder.DropTable(
                name: "tb_colors");

            migrationBuilder.DropTable(
                name: "tb_products");

            migrationBuilder.DropTable(
                name: "tb_brands");

            migrationBuilder.DropTable(
                name: "tb_categories");

            migrationBuilder.DropTable(
                name: "tb_genders");
        }
    }
}
