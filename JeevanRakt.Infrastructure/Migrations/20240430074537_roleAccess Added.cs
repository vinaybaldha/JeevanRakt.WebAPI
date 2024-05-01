using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeevanRakt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class roleAccessAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleAccesses",
                columns: table => new
                {
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Menu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a2c3381b-12f9-4185-a8be-9f38e9694215", "AQAAAAIAAYagAAAAEM+ban8hDxO7EWSjZHzv2s+oP2+/YIaUm0F0doNNn98Je107s0s7+9WWS4KLM//r+w==", "c6733fb4-8014-48ea-9c6d-bbf92601a262" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleAccesses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bf4934b-109e-4d9b-9b1c-f834dcdfd825", "AQAAAAIAAYagAAAAECw5TzLYSGaoYwxh/U9IT9VFQy+tEphyq+N40Ruyg+faK6hdg81gVN3KbDPUEzWhkw==", "93ecd14f-1842-4ece-9a02-27720826a59a" });
        }
    }
}
