using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stay_link.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemovingHotelIdFromBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hotel_id",
                table: "bookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "hotel_id",
                table: "bookings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
