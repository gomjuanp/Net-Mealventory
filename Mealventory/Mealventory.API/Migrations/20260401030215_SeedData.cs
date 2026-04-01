using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mealventory.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FoodItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, "juan@gmail.com", "password123", "juan" },
                    { 2, "daniel@hotmail.com", "password123", "daniel" }
                });

            migrationBuilder.InsertData(
                table: "FoodItems",
                columns: new[] { "Id", "ExpirationDate", "Name", "Quantity", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Milk", 3, 1 },
                    { 2, new DateTime(2026, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Banana", 2, 1 },
                    { 3, new DateTime(2026, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bacon", 1, 1 },
                    { 4, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Milk", 24, 2 },
                    { 5, new DateTime(2026, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Banana", 16, 2 },
                    { 6, new DateTime(2026, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple", 12, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FoodItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
