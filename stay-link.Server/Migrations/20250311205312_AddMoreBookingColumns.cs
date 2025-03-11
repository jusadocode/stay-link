using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stay_link.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreBookingColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropIndex(
                name: "IX_bookings_userid",
                table: "bookings");

            migrationBuilder.AddColumn<int>(
                name: "totalguests",
                table: "bookings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "totalprice",
                table: "bookings",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalguests",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "totalprice",
                table: "bookings");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_userid",
                table: "bookings",
                column: "userid");
        }
    }
}
