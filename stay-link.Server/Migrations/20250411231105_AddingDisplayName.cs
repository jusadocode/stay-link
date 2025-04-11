using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stay_link.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddingDisplayName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "group_name",
                table: "bookings");

            migrationBuilder.AddColumn<string>(
                name: "display_name",
                table: "bookings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "display_name",
                table: "bookings");

            migrationBuilder.AddColumn<string>(
                name: "group_name",
                table: "bookings",
                type: "text",
                nullable: true);
        }
    }
}
