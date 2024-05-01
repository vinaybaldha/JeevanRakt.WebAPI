using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeevanRakt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class menuadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuId);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bf4934b-109e-4d9b-9b1c-f834dcdfd825", "AQAAAAIAAYagAAAAECw5TzLYSGaoYwxh/U9IT9VFQy+tEphyq+N40Ruyg+faK6hdg81gVN3KbDPUEzWhkw==", "93ecd14f-1842-4ece-9a02-27720826a59a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2cfe254f-3766-4782-957a-3aa92ec52a22", "AQAAAAIAAYagAAAAEICVerqBmbIuJ34k48fNrFA+pvs8ZdqLRtoOW33zpRHWFqq0M1IhwGGxyWiwyXceWw==", "90ba485a-84dd-4a50-8fb2-414af2934637" });
        }
    }
}
