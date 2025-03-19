using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace stay_link.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangingRoomFeatureToClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "features",
                table: "rooms");

            migrationBuilder.CreateTable(
                name: "roomfeatures",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    bookingid = table.Column<int>(type: "integer", nullable: true),
                    roomid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roomfeatures", x => x.id);
                    table.ForeignKey(
                        name: "FK_roomfeatures_bookings_bookingid",
                        column: x => x.bookingid,
                        principalTable: "bookings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_roomfeatures_rooms_roomid",
                        column: x => x.roomid,
                        principalTable: "rooms",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_roomfeatures_bookingid",
                table: "roomfeatures",
                column: "bookingid");

            migrationBuilder.CreateIndex(
                name: "IX_roomfeatures_roomid",
                table: "roomfeatures",
                column: "roomid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "roomfeatures");

            migrationBuilder.AddColumn<int[]>(
                name: "features",
                table: "rooms",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);
        }
    }
}
