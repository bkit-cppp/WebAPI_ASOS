using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.API.Migrations
{
    /// <inheritdoc />
    public partial class dbv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_orders_tb_order_status_StatusId",
                table: "tb_orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "tb_refunds",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "StatusId",
                table: "tb_orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "tb_orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "tb_orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "tb_orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                table: "tb_orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductItemId",
                table: "tb_order_items",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VariationId",
                table: "tb_order_items",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_tb_orders_tb_order_status_StatusId",
                table: "tb_orders",
                column: "StatusId",
                principalTable: "tb_order_status",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_orders_tb_order_status_StatusId",
                table: "tb_orders");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "tb_orders");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "tb_orders");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "tb_orders");

            migrationBuilder.DropColumn(
                name: "ReceiverName",
                table: "tb_orders");

            migrationBuilder.DropColumn(
                name: "ProductItemId",
                table: "tb_order_items");

            migrationBuilder.DropColumn(
                name: "VariationId",
                table: "tb_order_items");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "tb_refunds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StatusId",
                table: "tb_orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_orders_tb_order_status_StatusId",
                table: "tb_orders",
                column: "StatusId",
                principalTable: "tb_order_status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
