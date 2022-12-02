using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rtmcuz.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterRequiredFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "91af7e97-e621-4093-a10c-47dde02ff02e");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Feedback",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5eaec230-4611-4470-8343-b68ce79056e6", 0, "00133707-d5d2-45bc-9cd9-976e39a1ec61", "rtmcuz@admin1", true, true, null, "RTMCUZ@ADMIN1", "RTMCUZ@ADMIN1", "AQAAAAEAACcQAAAAEJULcteN7kDZmveRS8MNpzwwHXVpgQ2k648xsRS1aMW5zybCC2cDa+dFbp2M2XJs7g==", null, false, "95545b84-5eb6-46f0-ba9c-9b03e7595139", false, "rtmcuz@admin1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5eaec230-4611-4470-8343-b68ce79056e6");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Feedback",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "91af7e97-e621-4093-a10c-47dde02ff02e", 0, "43c20db8-87ff-44e1-8f6e-e2f20289b164", "rtmcuz@admin1", true, true, null, "RTMCUZ@ADMIN1", "RTMCUZ@ADMIN1", "AQAAAAEAACcQAAAAEHSVqCymNGU9GWI9CucXRRoZuXThIh2wKKeFAon2rMaibTrSnfZJZDWPmf+dfTdCtg==", null, false, "85e2ebe6-bb15-46b8-a130-26e54f6ad180", false, "rtmcuz@admin1" });
        }
    }
}
