using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rtmcuz.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnGroupId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6c7872f6-7e6e-4dfa-81aa-9a829c8b79f8");

            migrationBuilder.AlterColumn<int>(
                name: "Lang",
                table: "Sections",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Sections",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ae8a55ba-eeb3-4948-8d44-b68de226c8d3", 0, "29bd57de-4e5b-4070-9b71-c5dea98be83b", "rtmcuz@admin1", true, true, null, "RTMCUZ@ADMIN1", "RTMCUZ@ADMIN1", "AQAAAAEAACcQAAAAEPVmIDd5RrDpyWbyuAOA1OuHogDI1B+D/5sFyal29I6PlYXDrddM2kkyHgQtNWF3Jg==", null, false, "2af2c1ad-b9e9-401f-bb64-19ee89ff1ffb", false, "rtmcuz@admin1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ae8a55ba-eeb3-4948-8d44-b68de226c8d3");

            migrationBuilder.AlterColumn<int>(
                name: "Lang",
                table: "Sections",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Sections",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6c7872f6-7e6e-4dfa-81aa-9a829c8b79f8", 0, "a619706a-91de-47a6-a4ca-786373b0d2e8", "rtmcuz@admin1", true, true, null, "RTMCUZ@ADMIN1", "RTMCUZ@ADMIN1", "AQAAAAEAACcQAAAAEF+kQYlS5kda3izBpmw1uv8F6xujP1CQ3FC474jTapdWJF1V4I4gjJl09i+eiM1JqA==", null, false, "915f0fc0-e621-4c3d-bff7-1dda691c0de4", false, "rtmcuz@admin1" });
        }
    }
}
