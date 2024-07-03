using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class ChangedBirthDateType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d0d8252-a0f6-404f-a1d8-811289090228");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "821f5a40-620b-4189-8951-b5aee97659c6");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "People",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d4d7f52-9e6a-46f7-963c-e51b9376aa2e", null, "Admin", "ADMIN" },
                    { "eeab98a4-836e-4bd4-8558-e67e70796a54", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d4d7f52-9e6a-46f7-963c-e51b9376aa2e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eeab98a4-836e-4bd4-8558-e67e70796a54");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "BirthDate",
                table: "People",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d0d8252-a0f6-404f-a1d8-811289090228", null, "User", "USER" },
                    { "821f5a40-620b-4189-8951-b5aee97659c6", null, "Admin", "ADMIN" }
                });
        }
    }
}
