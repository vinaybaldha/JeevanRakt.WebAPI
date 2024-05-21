using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeevanRakt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class relationofuserwithdonor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "662843e5-9c9d-4195-802b-345242d7d8f8", "AQAAAAIAAYagAAAAEOMol5aDRoG+bFt3lLhR3M+X5SgQU4n05Z2hBGKrDcYZ7aDlLtz5wdkYn08HjGfFEA==", "102865c0-e07b-4185-98eb-6b3f542f4274" });

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_UserId",
                table: "Recipients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Donors_UserId",
                table: "Donors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donors_AspNetUsers_UserId",
                table: "Donors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipients_AspNetUsers_UserId",
                table: "Recipients",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donors_AspNetUsers_UserId",
                table: "Donors");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipients_AspNetUsers_UserId",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Recipients_UserId",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Donors_UserId",
                table: "Donors");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cee455ca-48f1-4c28-b3bc-ba0ea869e134", "AQAAAAIAAYagAAAAELAH1UU2RqZTFogEVfEIqAydgtcXhentMSfzSRVRRHh6z3bTXow+RMDI5bESnLOLcw==", "99336868-b6c1-47a4-b593-8e26c1b9b9d1" });
        }
    }
}
