using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeevanRakt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class approvebloodbank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateStatus",
                table: "BloodBanks",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "997aa0a8-6735-4df3-901f-2a37d4438ef7", "AQAAAAIAAYagAAAAEE+vOgOYG3fqO8w30sMDbOQLmgvdCjxm99s++emm5sM7OBSvVkcT67q/pPOPL19FjA==", "b3528592-0a07-49b2-8c8d-374126254e09" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateStatus",
                table: "BloodBanks");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "662843e5-9c9d-4195-802b-345242d7d8f8", "AQAAAAIAAYagAAAAEOMol5aDRoG+bFt3lLhR3M+X5SgQU4n05Z2hBGKrDcYZ7aDlLtz5wdkYn08HjGfFEA==", "102865c0-e07b-4185-98eb-6b3f542f4274" });
        }
    }
}
