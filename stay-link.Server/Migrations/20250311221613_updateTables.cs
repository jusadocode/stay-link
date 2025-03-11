using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace stay_link.Server.Migrations
{
    /// <inheritdoc />
    public partial class updateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomFeature",
                table: "RoomFeature");

            migrationBuilder.RenameTable(
                name: "RoomFeature",
                newName: "roomfeature");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "roomfeature",
                newName: "roomid");

            migrationBuilder.RenameColumn(
                name: "FeatureId",
                table: "roomfeature",
                newName: "featureid");

            migrationBuilder.RenameColumn(
                name: "Condition",
                table: "roomfeature",
                newName: "condition");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "roomfeature",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roomfeature",
                table: "roomfeature",
                column: "id");

            migrationBuilder.CreateTable(
                name: "feature",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feature", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "feature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roomfeature",
                table: "roomfeature");

            migrationBuilder.RenameTable(
                name: "roomfeature",
                newName: "RoomFeature");

            migrationBuilder.RenameColumn(
                name: "roomid",
                table: "RoomFeature",
                newName: "RoomId");

            migrationBuilder.RenameColumn(
                name: "featureid",
                table: "RoomFeature",
                newName: "FeatureId");

            migrationBuilder.RenameColumn(
                name: "condition",
                table: "RoomFeature",
                newName: "Condition");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "RoomFeature",
                newName: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomFeature",
                table: "RoomFeature",
                column: "ID");
        }
    }
}
