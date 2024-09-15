using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentService.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReservedPrice = table.Column<int>(type: "integer", nullable: false),
                    Landlord = table.Column<string>(type: "text", nullable: true),
                    LandlordContactNo = table.Column<string>(type: "text", nullable: true),
                    Tennant = table.Column<string>(type: "text", nullable: true),
                    RentAmount = table.Column<int>(type: "integer", nullable: true),
                    CurrentHighBid = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConfirmedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Beds = table.Column<int>(type: "integer", nullable: false),
                    Baths = table.Column<int>(type: "integer", nullable: false),
                    Balconies = table.Column<int>(type: "integer", nullable: true),
                    HouseSize = table.Column<int>(type: "integer", nullable: false),
                    HouseSizeUnit = table.Column<int>(type: "integer", nullable: false),
                    LandSize = table.Column<int>(type: "integer", nullable: true),
                    LandSizeUnit = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AvailableFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Rents_RentId",
                        column: x => x.RentId,
                        principalTable: "Rents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_RentId",
                table: "Items",
                column: "RentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Rents");
        }
    }
}
