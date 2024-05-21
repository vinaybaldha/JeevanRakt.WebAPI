using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeevanRakt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addpaymentstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "Recipients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b85c706-1716-4ccf-b795-2acaddadc4a9", "AQAAAAIAAYagAAAAEBU7rE+kHHAmsBOcMPcSsYfVllsZOfLbtGPyJ4Gu8EsT+deXsXYEdV/8Gje1lm3MZg==", "2f9e26d6-a9eb-4c85-9a8b-af70a2481564" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Recipients");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a6f13f99-5d7a-40b9-a340-5bd75d47f0bb", "AQAAAAIAAYagAAAAEKY8yKlk0rPuqR2i0x580RSu3Z7zDOswT/V6104BGWZHqV6OwJx7BG51PirPeSc0PA==", "e454cd42-9c6e-4ef7-8731-86d8d0473bc3" });
        }
    }
}
