using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace stay_link.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "feature");

            migrationBuilder.DropTable(
                name: "roomfeature");

            migrationBuilder.AddColumn<int[]>(
                name: "features",
                table: "rooms",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);

            migrationBuilder.AddColumn<int>(
                name: "floornumber",
                table: "rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "rooms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "roomusages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    roomid = table.Column<int>(type: "integer", nullable: false),
                    generalwear = table.Column<double>(type: "double precision", nullable: false),
                    cleaningstate = table.Column<string>(type: "text", nullable: false),
                    timesbookedthisyear = table.Column<int>(type: "integer", nullable: false),
                    timesbookedsincemaintenance = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roomusages", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "roomusages");

            migrationBuilder.DropColumn(
                name: "features",
                table: "rooms");

            migrationBuilder.DropColumn(
                name: "floornumber",
                table: "rooms");

            migrationBuilder.DropColumn(
                name: "title",
                table: "rooms");

            migrationBuilder.CreateTable(
                name: "feature",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feature", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roomfeature",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    condition = table.Column<string>(type: "text", nullable: false),
                    featureid = table.Column<string>(type: "text", nullable: false),
                    roomid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roomfeature", x => x.id);
                });
        }
    }
}
