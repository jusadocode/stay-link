using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stay_link.Server.Migrations
{
    /// <inheritdoc />
    public partial class MultipleRoomBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_bookings__rooms_room_id",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "f_k_bookings_bookings_booking_i_d",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "i_x_bookings_booking_i_d",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "i_x_bookings_room_id",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "booking_i_d",
                table: "bookings");

            migrationBuilder.CreateTable(
                name: "booking_rooms",
                columns: table => new
                {
                    booking_id = table.Column<int>(type: "integer", nullable: false),
                    room_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_rooms", x => new { x.booking_id, x.room_id });
                    table.ForeignKey(
                        name: "f_k_booking_rooms__bookings_booking_id",
                        column: x => x.booking_id,
                        principalTable: "bookings",
                        principalColumn: "i_d",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_booking_rooms__rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "rooms",
                        principalColumn: "i_d",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "i_x_booking_rooms_room_id",
                table: "booking_rooms",
                column: "room_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booking_rooms");

            migrationBuilder.AddColumn<int>(
                name: "booking_i_d",
                table: "bookings",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "i_x_bookings_booking_i_d",
                table: "bookings",
                column: "booking_i_d");

            migrationBuilder.CreateIndex(
                name: "i_x_bookings_room_id",
                table: "bookings",
                column: "room_id");

            migrationBuilder.AddForeignKey(
                name: "f_k_bookings__rooms_room_id",
                table: "bookings",
                column: "room_id",
                principalTable: "rooms",
                principalColumn: "i_d",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_bookings_bookings_booking_i_d",
                table: "bookings",
                column: "booking_i_d",
                principalTable: "bookings",
                principalColumn: "i_d");
        }
    }
}
