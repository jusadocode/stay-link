using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stay_link.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddingExtraCostToFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "extracost",
                table: "roomfeatures",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "extracost",
                table: "roomfeatures");
        }
    }
}
