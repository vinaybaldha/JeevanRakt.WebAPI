using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeevanRakt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BloodBanks_BloodBankId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BloodInventories_BloodBanks_BloodBankId",
                table: "BloodInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Donors_BloodBanks_BloodBankId",
                table: "Donors");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipients_BloodBanks_BloodBankId",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BloodBankId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "678cae65-bae4-4201-b130-19aa03496bb6", "AQAAAAIAAYagAAAAEJokdQvjZfcxV4b9GfCGVKDMnHhNLi2GI5isckSQru1sTbRKBDtNTNoS07j5K3se8g==", "bfd14af4-f1d8-4239-872a-1f1dfcaccaec" });

            migrationBuilder.AddForeignKey(
                name: "FK_BloodInventories_BloodBanks_BloodBankId",
                table: "BloodInventories",
                column: "BloodBankId",
                principalTable: "BloodBanks",
                principalColumn: "BloodBankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donors_BloodBanks_BloodBankId",
                table: "Donors",
                column: "BloodBankId",
                principalTable: "BloodBanks",
                principalColumn: "BloodBankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipients_BloodBanks_BloodBankId",
                table: "Recipients",
                column: "BloodBankId",
                principalTable: "BloodBanks",
                principalColumn: "BloodBankId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodInventories_BloodBanks_BloodBankId",
                table: "BloodInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Donors_BloodBanks_BloodBankId",
                table: "Donors");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipients_BloodBanks_BloodBankId",
                table: "Recipients");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58c49942-d5c6-4b15-9a56-878e4333e07b", "AQAAAAIAAYagAAAAEA8n1vuYe9lMtu9aJ2bcnLnf5hgfz2UwIDQy3prU/jTc8RWk8KY1557p0Cx2XKQs4Q==", "dddaa798-0037-46b8-a337-3b9d9e8cbcfb" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_BloodInventories_BloodBanks_BloodBankId",
                table: "BloodInventories",
                column: "BloodBankId",
                principalTable: "BloodBanks",
                principalColumn: "BloodBankId",
                onDelete: ReferentialAction.Cascade);

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
    }
}
