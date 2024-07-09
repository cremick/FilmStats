using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d4d7f52-9e6a-46f7-963c-e51b9376aa2e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eeab98a4-836e-4bd4-8558-e67e70796a54");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "People",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3cf4aaf2-7dbd-4024-81c2-f85a37e9c3b7", null, "Admin", "ADMIN" },
                    { "3df48e41-bdd6-4804-8d20-27cb8386c53b", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cf4aaf2-7dbd-4024-81c2-f85a37e9c3b7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3df48e41-bdd6-4804-8d20-27cb8386c53b");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Films");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d4d7f52-9e6a-46f7-963c-e51b9376aa2e", null, "Admin", "ADMIN" },
                    { "eeab98a4-836e-4bd4-8558-e67e70796a54", null, "User", "USER" }
                });
        }
    }
}
