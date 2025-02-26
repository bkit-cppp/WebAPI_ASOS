using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.API.Migrations
{
    /// <inheritdoc />
    public partial class dbidentityv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "tb_users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "BanReason",
                table: "tb_users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HubId",
                table: "tb_users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailConfirmed",
                table: "tb_users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "StatusId",
                table: "tb_users",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tb_roles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "tb_notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Navigate = table.Column<string>(type: "text", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_notifications_tb_users_UserId",
                        column: x => x.UserId,
                        principalTable: "tb_users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_otps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsExpired = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_otps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_otps_tb_users_UserId",
                        column: x => x.UserId,
                        principalTable: "tb_users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_point_histories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReferenceId = table.Column<string>(type: "text", nullable: false),
                    ReferenceType = table.Column<string>(type: "text", nullable: false),
                    PointBefore = table.Column<int>(type: "integer", nullable: false),
                    PointChange = table.Column<int>(type: "integer", nullable: false),
                    PointAfter = table.Column<int>(type: "integer", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_point_histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_point_histories_tb_users_UserId",
                        column: x => x.UserId,
                        principalTable: "tb_users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_statuses",
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
                    table.PrimaryKey("PK_tb_statuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_users_StatusId",
                table: "tb_users",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_notifications_UserId",
                table: "tb_notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_otps_UserId",
                table: "tb_otps",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_point_histories_UserId",
                table: "tb_point_histories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_users_tb_statuses_StatusId",
                table: "tb_users",
                column: "StatusId",
                principalTable: "tb_statuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_users_tb_statuses_StatusId",
                table: "tb_users");

            migrationBuilder.DropTable(
                name: "tb_notifications");

            migrationBuilder.DropTable(
                name: "tb_otps");

            migrationBuilder.DropTable(
                name: "tb_point_histories");

            migrationBuilder.DropTable(
                name: "tb_statuses");

            migrationBuilder.DropIndex(
                name: "IX_tb_users_StatusId",
                table: "tb_users");

            migrationBuilder.DropColumn(
                name: "BanReason",
                table: "tb_users");

            migrationBuilder.DropColumn(
                name: "HubId",
                table: "tb_users");

            migrationBuilder.DropColumn(
                name: "IsEmailConfirmed",
                table: "tb_users");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "tb_users");

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "tb_users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tb_roles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
