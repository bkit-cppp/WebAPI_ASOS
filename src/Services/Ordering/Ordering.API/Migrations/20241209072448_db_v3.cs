using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.API.Migrations
{
    /// <inheritdoc />
    public partial class db_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "tb_order_histories",
                newName: "ToStatus");

            migrationBuilder.AddColumn<string>(
                name: "FromStatus",
                table: "tb_order_histories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromStatus",
                table: "tb_order_histories");

            migrationBuilder.RenameColumn(
                name: "ToStatus",
                table: "tb_order_histories",
                newName: "Status");
        }
    }
}
