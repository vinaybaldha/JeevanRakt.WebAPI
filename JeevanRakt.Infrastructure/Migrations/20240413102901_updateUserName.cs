using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeevanRakt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f891f842-d2c9-4bf9-8b44-1031a64f7f42", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEE1aLOsGy36w/FXBr+GB67YWy6M0OvhPmsMAVG6glFBihlkaKe8sOoLm8gz7A15MSg==", "9ea23889-5e1d-4d9e-b4ea-0cab5701e947" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bba83451-9c4b-423b-91b6-254ff7bfd600"),
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ebad5c2e-bdc2-4997-87b1-4e5ea171a02e", "ADMIN", "AQAAAAIAAYagAAAAEN3S+VrbmcwlTlm54rNDv1yb17mrBl5mwy3pt7SZkijRX/1p6ib1ooh1awYvfcXTuQ==", "8608d55f-355f-4c8a-9959-3badd0cff426" });
        }
    }
}
