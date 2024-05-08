using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeevanRakt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cascadedeletetoonetoonerelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "462cbf09-5d58-4180-acde-4bca625c22b1", "AQAAAAIAAYagAAAAEC7k8lDQ9oRGCXSAP+TcfnwhWp/qt7C5NTsCjvOJ8ExZuHfyzvGnRyjtQ5MrSINlkA==", "f146b7db-ea05-44ae-a341-d811132089e9" });

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
    }
}
