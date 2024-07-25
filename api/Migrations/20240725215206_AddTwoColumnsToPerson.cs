using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddTwoColumnsToPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31ce7de0-a4fd-407c-b131-0a0d33f07897");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "971ac333-3592-482a-8761-40e097947373");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeathDate",
                table: "People",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "KnownAs",
                table: "People",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "883b843e-b3de-4674-b99e-84e3f0858211", null, "User", "USER" },
                    { "ce4480fb-2ec6-4d09-b8e8-2ef4377ff697", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "883b843e-b3de-4674-b99e-84e3f0858211");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce4480fb-2ec6-4d09-b8e8-2ef4377ff697");

            migrationBuilder.DropColumn(
                name: "DeathDate",
                table: "People");

            migrationBuilder.DropColumn(
                name: "KnownAs",
                table: "People");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "31ce7de0-a4fd-407c-b131-0a0d33f07897", null, "User", "USER" },
                    { "971ac333-3592-482a-8761-40e097947373", null, "Admin", "ADMIN" }
                });
        }
    }
}
