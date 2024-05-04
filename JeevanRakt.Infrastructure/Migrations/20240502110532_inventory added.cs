using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeevanRakt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class inventoryadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodInventory_BloodBanks_BloodBankId",
                table: "BloodInventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BloodInventory",
                table: "BloodInventory");

            migrationBuilder.RenameTable(
                name: "BloodInventory",
                newName: "BloodInventories");

            migrationBuilder.RenameIndex(
                name: "IX_BloodInventory_BloodBankId",
                table: "BloodInventories",
                newName: "IX_BloodInventories_BloodBankId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BloodInventories",
                table: "BloodInventories",
                column: "BloodInventoryId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25d97973-bfaf-4a8d-8bd9-5a882e1507d6", "AQAAAAIAAYagAAAAEPB//wr/U0k3HkG/fJgaIIe5FeyE/wAMNvESY7IV5zojb+25+kRjUwKiRYC+BvzpLw==", "3969e9d7-9146-433b-9f52-caada1e357a8" });

            migrationBuilder.AddForeignKey(
                name: "FK_BloodInventories_BloodBanks_BloodBankId",
                table: "BloodInventories",
                column: "BloodBankId",
                principalTable: "BloodBanks",
                principalColumn: "BloodBankId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodInventories_BloodBanks_BloodBankId",
                table: "BloodInventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BloodInventories",
                table: "BloodInventories");

            migrationBuilder.RenameTable(
                name: "BloodInventories",
                newName: "BloodInventory");

            migrationBuilder.RenameIndex(
                name: "IX_BloodInventories_BloodBankId",
                table: "BloodInventory",
                newName: "IX_BloodInventory_BloodBankId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BloodInventory",
                table: "BloodInventory",
                column: "BloodInventoryId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9339cb5c-8464-4a93-bd79-f5565dba012a", "AQAAAAIAAYagAAAAEOIblJ0L1iCmUChtdd3b+XwJZ+CacmmKenySyrkYlt43nZz2sZW0Wx+WgizsU+C7Mw==", "b54a2bed-2af0-46d1-8400-b525f8c73cbc" });

            migrationBuilder.AddForeignKey(
                name: "FK_BloodInventory_BloodBanks_BloodBankId",
                table: "BloodInventory",
                column: "BloodBankId",
                principalTable: "BloodBanks",
                principalColumn: "BloodBankId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
