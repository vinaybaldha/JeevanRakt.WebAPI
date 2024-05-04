using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeevanRakt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class relationadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BloodBankId",
                table: "Recipients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BloodBankId",
                table: "Donors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BloodInventory",
                columns: table => new
                {
                    BloodInventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    A1 = table.Column<int>(type: "int", nullable: false),
                    A2 = table.Column<int>(type: "int", nullable: false),
                    B1 = table.Column<int>(type: "int", nullable: false),
                    B2 = table.Column<int>(type: "int", nullable: false),
                    AB1 = table.Column<int>(type: "int", nullable: false),
                    AB2 = table.Column<int>(type: "int", nullable: false),
                    O1 = table.Column<int>(type: "int", nullable: false),
                    O2 = table.Column<int>(type: "int", nullable: false),
                    BloodBankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodInventory", x => x.BloodInventoryId);
                    table.ForeignKey(
                        name: "FK_BloodInventory_BloodBanks_BloodBankId",
                        column: x => x.BloodBankId,
                        principalTable: "BloodBanks",
                        principalColumn: "BloodBankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9339cb5c-8464-4a93-bd79-f5565dba012a", "AQAAAAIAAYagAAAAEOIblJ0L1iCmUChtdd3b+XwJZ+CacmmKenySyrkYlt43nZz2sZW0Wx+WgizsU+C7Mw==", "b54a2bed-2af0-46d1-8400-b525f8c73cbc" });

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_BloodBankId",
                table: "Recipients",
                column: "BloodBankId");

            migrationBuilder.CreateIndex(
                name: "IX_Donors_BloodBankId",
                table: "Donors",
                column: "BloodBankId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodInventory_BloodBankId",
                table: "BloodInventory",
                column: "BloodBankId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donors_BloodBanks_BloodBankId",
                table: "Donors",
                column: "BloodBankId",
                principalTable: "BloodBanks",
                principalColumn: "BloodBankId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipients_BloodBanks_BloodBankId",
                table: "Recipients",
                column: "BloodBankId",
                principalTable: "BloodBanks",
                principalColumn: "BloodBankId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donors_BloodBanks_BloodBankId",
                table: "Donors");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipients_BloodBanks_BloodBankId",
                table: "Recipients");

            migrationBuilder.DropTable(
                name: "BloodInventory");

            migrationBuilder.DropIndex(
                name: "IX_Recipients_BloodBankId",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Donors_BloodBankId",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "BloodBankId",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "BloodBankId",
                table: "Donors");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a2c3381b-12f9-4185-a8be-9f38e9694215", "AQAAAAIAAYagAAAAEM+ban8hDxO7EWSjZHzv2s+oP2+/YIaUm0F0doNNn98Je107s0s7+9WWS4KLM//r+w==", "c6733fb4-8014-48ea-9c6d-bbf92601a262" });
        }
    }
}
