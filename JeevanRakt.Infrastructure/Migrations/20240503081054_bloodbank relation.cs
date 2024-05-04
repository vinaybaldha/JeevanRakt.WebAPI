using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeevanRakt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bloodbankrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BloodBankId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "BloodBankId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "58c49942-d5c6-4b15-9a56-878e4333e07b", "AQAAAAIAAYagAAAAEA8n1vuYe9lMtu9aJ2bcnLnf5hgfz2UwIDQy3prU/jTc8RWk8KY1557p0Cx2XKQs4Q==", "dddaa798-0037-46b8-a337-3b9d9e8cbcfb" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BloodBankId",
                table: "AspNetUsers",
                column: "BloodBankId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BloodBanks_BloodBankId",
                table: "AspNetUsers",
                column: "BloodBankId",
                principalTable: "BloodBanks",
                principalColumn: "BloodBankId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BloodBanks_BloodBankId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BloodBankId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BloodBankId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25d97973-bfaf-4a8d-8bd9-5a882e1507d6", "AQAAAAIAAYagAAAAEPB//wr/U0k3HkG/fJgaIIe5FeyE/wAMNvESY7IV5zojb+25+kRjUwKiRYC+BvzpLw==", "3969e9d7-9146-433b-9f52-caada1e357a8" });
        }
    }
}
