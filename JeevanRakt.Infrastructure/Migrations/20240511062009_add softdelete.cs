using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeevanRakt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addsoftdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecStatus",
                table: "Recipients",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecStatus",
                table: "Donors",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecStatus",
                table: "BloodBanks",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a6f13f99-5d7a-40b9-a340-5bd75d47f0bb", "AQAAAAIAAYagAAAAEKY8yKlk0rPuqR2i0x580RSu3Z7zDOswT/V6104BGWZHqV6OwJx7BG51PirPeSc0PA==", "e454cd42-9c6e-4ef7-8731-86d8d0473bc3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecStatus",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "RecStatus",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "RecStatus",
                table: "BloodBanks");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "462cbf09-5d58-4180-acde-4bca625c22b1", "AQAAAAIAAYagAAAAEC7k8lDQ9oRGCXSAP+TcfnwhWp/qt7C5NTsCjvOJ8ExZuHfyzvGnRyjtQ5MrSINlkA==", "f146b7db-ea05-44ae-a341-d811132089e9" });
        }
    }
}
