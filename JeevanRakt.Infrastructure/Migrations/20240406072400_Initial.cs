using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeevanRakt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodInventories",
                columns: table => new
                {
                    InventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryBloodType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventoryQuantityInMl = table.Column<int>(type: "int", nullable: false),
                    InventoryExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodInventories", x => x.InventoryId);
                });

            migrationBuilder.CreateTable(
                name: "Donors",
                columns: table => new
                {
                    DonorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DonorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonorBloodType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonorContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donors", x => x.DonorId);
                });

            migrationBuilder.CreateTable(
                name: "Recipients",
                columns: table => new
                {
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientBloodType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipients", x => x.RecipientId);
                });

            migrationBuilder.CreateTable(
                name: "BloodDonations",
                columns: table => new
                {
                    DonationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DonorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DonationBloodType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonationQuantityInMl = table.Column<int>(type: "int", nullable: false),
                    DonationIsTested = table.Column<bool>(type: "bit", nullable: false),
                    InventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodDonations", x => x.DonationId);
                    table.ForeignKey(
                        name: "FK_BloodDonations_BloodInventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "BloodInventories",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BloodDonations_Donors_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Donors",
                        principalColumn: "DonorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloodRequests",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestBloodType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestQuantityInMl = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestIsFulfilled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodRequests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_BloodRequests_Recipients_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipients",
                        principalColumn: "RecipientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BloodDonations_DonorId",
                table: "BloodDonations",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodDonations_InventoryId",
                table: "BloodDonations",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodRequests_RecipientId",
                table: "BloodRequests",
                column: "RecipientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodDonations");

            migrationBuilder.DropTable(
                name: "BloodRequests");

            migrationBuilder.DropTable(
                name: "BloodInventories");

            migrationBuilder.DropTable(
                name: "Donors");

            migrationBuilder.DropTable(
                name: "Recipients");
        }
    }
}
