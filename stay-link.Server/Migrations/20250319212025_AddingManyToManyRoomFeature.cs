using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace stay_link.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddingManyToManyRoomFeature : Migration
    {
        [ExcludeFromCodeCoverage]
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roomfeatures_bookings_bookingid",
                table: "roomfeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_roomfeatures_rooms_roomid",
                table: "roomfeatures");

            migrationBuilder.DropIndex(
                name: "IX_roomfeatures_bookingid",
                table: "roomfeatures");

            migrationBuilder.DropIndex(
                name: "IX_roomfeatures_roomid",
                table: "roomfeatures");

            migrationBuilder.DropColumn(
                name: "bookingid",
                table: "roomfeatures");

            migrationBuilder.DropColumn(
                name: "roomid",
                table: "roomfeatures");

            migrationBuilder.AlterColumn<decimal>(
                name: "extracost",
                table: "roomfeatures",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "bookings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "bookingroomfeature",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "integer", nullable: false),
                    RoomFeatureID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookingroomfeature", x => new { x.BookingID, x.RoomFeatureID });
                    table.ForeignKey(
                        name: "FK_bookingroomfeature_bookings_BookingID",
                        column: x => x.BookingID,
                        principalTable: "bookings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bookingroomfeature_roomfeatures_RoomFeatureID",
                        column: x => x.RoomFeatureID,
                        principalTable: "roomfeatures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "roomroomfeature",
                columns: table => new
                {
                    RoomFeatureID = table.Column<int>(type: "integer", nullable: false),
                    RoomID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roomroomfeature", x => new { x.RoomFeatureID, x.RoomID });
                    table.ForeignKey(
                        name: "FK_roomroomfeature_roomfeatures_RoomFeatureID",
                        column: x => x.RoomFeatureID,
                        principalTable: "roomfeatures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_roomroomfeature_rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookingroomfeature_RoomFeatureID",
                table: "bookingroomfeature",
                column: "RoomFeatureID");

            migrationBuilder.CreateIndex(
                name: "IX_roomroomfeature_RoomID",
                table: "roomroomfeature",
                column: "RoomID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookingroomfeature");

            migrationBuilder.DropTable(
                name: "roomroomfeature");

            migrationBuilder.DropColumn(
                name: "status",
                table: "bookings");

            migrationBuilder.AlterColumn<double>(
                name: "extracost",
                table: "roomfeatures",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "bookingid",
                table: "roomfeatures",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "roomid",
                table: "roomfeatures",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_roomfeatures_bookingid",
                table: "roomfeatures",
                column: "bookingid");

            migrationBuilder.CreateIndex(
                name: "IX_roomfeatures_roomid",
                table: "roomfeatures",
                column: "roomid");

            migrationBuilder.AddForeignKey(
                name: "FK_roomfeatures_bookings_bookingid",
                table: "roomfeatures",
                column: "bookingid",
                principalTable: "bookings",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_roomfeatures_rooms_roomid",
                table: "roomfeatures",
                column: "roomid",
                principalTable: "rooms",
                principalColumn: "id");
        }
    }
}
