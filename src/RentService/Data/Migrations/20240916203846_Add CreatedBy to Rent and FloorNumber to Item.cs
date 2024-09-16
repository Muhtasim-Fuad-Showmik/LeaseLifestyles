using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentService.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedBytoRentandFloorNumbertoItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Rents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FloorNumber",
                table: "Items",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "FloorNumber",
                table: "Items");
        }
    }
}
