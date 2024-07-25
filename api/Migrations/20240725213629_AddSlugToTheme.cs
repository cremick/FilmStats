using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugToTheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "673664c5-3f5b-409f-b10e-17e8767f860a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a6133c7-d983-454e-8212-0a3f72b9265a");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Themes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "31ce7de0-a4fd-407c-b131-0a0d33f07897", null, "User", "USER" },
                    { "971ac333-3592-482a-8761-40e097947373", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31ce7de0-a4fd-407c-b131-0a0d33f07897");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "971ac333-3592-482a-8761-40e097947373");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Themes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "673664c5-3f5b-409f-b10e-17e8767f860a", null, "User", "USER" },
                    { "8a6133c7-d983-454e-8212-0a3f72b9265a", null, "Admin", "ADMIN" }
                });
        }
    }
}
