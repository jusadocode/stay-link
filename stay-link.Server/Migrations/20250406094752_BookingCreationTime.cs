using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stay_link.Server.Migrations
{
    /// <inheritdoc />
    public partial class BookingCreationTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "creation_time",
                table: "bookings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "creation_time",
                table: "bookings");
        }
    }
}
