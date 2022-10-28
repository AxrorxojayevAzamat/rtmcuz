using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace rtmcuz.Migrations
{
    /// <inheritdoc />
    public partial class ChangePage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Pages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Locale",
                table: "Pages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OriginId",
                table: "Pages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PageType",
                table: "Pages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Pages",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Permalink",
                table: "Pages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Template",
                table: "Pages",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MenuItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PageId = table.Column<int>(type: "integer", nullable: false),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    MenuItemId = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItem_MenuItem_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MenuItem_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_MenuItemId",
                table: "MenuItem",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_PageId",
                table: "MenuItem",
                column: "PageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItem");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "Locale",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "OriginId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "PageType",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "Permalink",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "Template",
                table: "Pages");
        }
    }
}
