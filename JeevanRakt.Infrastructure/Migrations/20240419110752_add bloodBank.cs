using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeevanRakt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addbloodBank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodBanks",
                columns: table => new
                {
                    BloodBankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodBankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodBanks", x => x.BloodBankId);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2cfe254f-3766-4782-957a-3aa92ec52a22", "AQAAAAIAAYagAAAAEICVerqBmbIuJ34k48fNrFA+pvs8ZdqLRtoOW33zpRHWFqq0M1IhwGGxyWiwyXceWw==", "90ba485a-84dd-4a50-8fb2-414af2934637" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodBanks");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dbeb407d-ed9d-4830-b4f5-dae452c51df2", "AQAAAAIAAYagAAAAEBuaBG00cz1mJB3wJVp4tVzNfty+GAM4fckkAQNIl4VjPqyjWg90g3QSsmtky2JjaQ==", "07cae45f-2eb2-41f2-a0ba-7e3299d2b33a" });
        }
    }
}
