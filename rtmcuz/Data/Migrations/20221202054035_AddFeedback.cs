using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace rtmcuz.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ae8a55ba-eeb3-4948-8d44-b68de226c8d3");

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "Departments_fk0",
                        column: x => x.DepartmentId,
                        principalTable: "Sections",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "91af7e97-e621-4093-a10c-47dde02ff02e", 0, "43c20db8-87ff-44e1-8f6e-e2f20289b164", "rtmcuz@admin1", true, true, null, "RTMCUZ@ADMIN1", "RTMCUZ@ADMIN1", "AQAAAAEAACcQAAAAEHSVqCymNGU9GWI9CucXRRoZuXThIh2wKKeFAon2rMaibTrSnfZJZDWPmf+dfTdCtg==", null, false, "85e2ebe6-bb15-46b8-a130-26e54f6ad180", false, "rtmcuz@admin1" });

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_DepartmentId",
                table: "Feedback",
                column: "DepartmentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "91af7e97-e621-4093-a10c-47dde02ff02e");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ae8a55ba-eeb3-4948-8d44-b68de226c8d3", 0, "29bd57de-4e5b-4070-9b71-c5dea98be83b", "rtmcuz@admin1", true, true, null, "RTMCUZ@ADMIN1", "RTMCUZ@ADMIN1", "AQAAAAEAACcQAAAAEPVmIDd5RrDpyWbyuAOA1OuHogDI1B+D/5sFyal29I6PlYXDrddM2kkyHgQtNWF3Jg==", null, false, "2af2c1ad-b9e9-401f-bb64-19ee89ff1ffb", false, "rtmcuz@admin1" });
        }
    }
}
