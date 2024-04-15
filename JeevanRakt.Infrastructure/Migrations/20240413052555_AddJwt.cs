using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JeevanRakt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddJwt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("839314e0-0f0d-4f97-82e3-6484e2ecca15"), "839314E0-0F0D-4F97-82E3-6484E2ECCA15", "Admin", "ADMIN" },
                    { new Guid("d1d3a4b1-17fb-496e-b65f-65a472d831d9"), "D1D3A4B1-17FB-496E-B65F-65A472D831D9", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("839314e0-0f0d-4f97-82e3-6484e2ecca15"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d1d3a4b1-17fb-496e-b65f-65a472d831d9"));
        }
    }
}
