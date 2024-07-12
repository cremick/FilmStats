using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cf4aaf2-7dbd-4024-81c2-f85a37e9c3b7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3df48e41-bdd6-4804-8d20-27cb8386c53b");

            migrationBuilder.AddColumn<int>(
                name: "ActingCredits",
                table: "People",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DirectingCredits",
                table: "People",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "673664c5-3f5b-409f-b10e-17e8767f860a", null, "User", "USER" },
                    { "8a6133c7-d983-454e-8212-0a3f72b9265a", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "673664c5-3f5b-409f-b10e-17e8767f860a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a6133c7-d983-454e-8212-0a3f72b9265a");

            migrationBuilder.DropColumn(
                name: "ActingCredits",
                table: "People");

            migrationBuilder.DropColumn(
                name: "DirectingCredits",
                table: "People");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3cf4aaf2-7dbd-4024-81c2-f85a37e9c3b7", null, "Admin", "ADMIN" },
                    { "3df48e41-bdd6-4804-8d20-27cb8386c53b", null, "User", "USER" }
                });
        }
    }
}
