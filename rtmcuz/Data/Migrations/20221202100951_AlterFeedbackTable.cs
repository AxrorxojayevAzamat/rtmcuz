using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rtmcuz.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterFeedbackTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Feedback_DepartmentId",
                table: "Feedback");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5eaec230-4611-4470-8343-b68ce79056e6");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ad66f928-ea1e-4184-9dd3-24fbe135184c", 0, "be3d2033-f8ca-459c-b3df-d7e08d3db1b4", "rtmcuz@admin1", true, true, null, "RTMCUZ@ADMIN1", "RTMCUZ@ADMIN1", "AQAAAAEAACcQAAAAEH2YzraIrEfBsNfIG61gg3VMKqrp6wnGwHAmbCbB7PMIKLm22cvEVb/GFqrKvKT5Xw==", null, false, "82fe461b-1843-4c55-880b-3ccb890d22b4", false, "rtmcuz@admin1" });

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_DepartmentId",
                table: "Feedback",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Feedback_DepartmentId",
                table: "Feedback");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad66f928-ea1e-4184-9dd3-24fbe135184c");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5eaec230-4611-4470-8343-b68ce79056e6", 0, "00133707-d5d2-45bc-9cd9-976e39a1ec61", "rtmcuz@admin1", true, true, null, "RTMCUZ@ADMIN1", "RTMCUZ@ADMIN1", "AQAAAAEAACcQAAAAEJULcteN7kDZmveRS8MNpzwwHXVpgQ2k648xsRS1aMW5zybCC2cDa+dFbp2M2XJs7g==", null, false, "95545b84-5eb6-46f0-ba9c-9b03e7595139", false, "rtmcuz@admin1" });

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_DepartmentId",
                table: "Feedback",
                column: "DepartmentId",
                unique: true);
        }
    }
}
