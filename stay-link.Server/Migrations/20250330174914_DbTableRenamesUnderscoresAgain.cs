using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stay_link.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbTableRenamesUnderscoresAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aspnetuserroles_roles_roleid",
                table: "aspnetuserroles");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetuserroles_users_userid",
                table: "aspnetuserroles");

            migrationBuilder.DropForeignKey(
                name: "FK_bookingbookingfeature_bookingfeature_bookingpreferencesid",
                table: "bookingbookingfeature");

            migrationBuilder.DropForeignKey(
                name: "FK_roomusages_rooms_roomid",
                table: "roomusages");

            migrationBuilder.DropTable(
                name: "booking_room_feature");

            migrationBuilder.DropTable(
                name: "room_room_feature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roomusages",
                table: "roomusages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roomfeatures",
                table: "roomfeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bookingfeature",
                table: "bookingfeature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetuserroles",
                table: "aspnetuserroles");

            migrationBuilder.RenameTable(
                name: "roomusages",
                newName: "room_usage");

            migrationBuilder.RenameTable(
                name: "roomfeatures",
                newName: "room_features");

            migrationBuilder.RenameTable(
                name: "bookingfeature",
                newName: "booking_features");

            migrationBuilder.RenameTable(
                name: "aspnetuserroles",
                newName: "user_roles");

            migrationBuilder.RenameIndex(
                name: "IX_roomusages_roomid",
                table: "room_usage",
                newName: "IX_room_usage_roomid");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetuserroles_roleid",
                table: "user_roles",
                newName: "IX_user_roles_roleid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_room_usage",
                table: "room_usage",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_room_features",
                table: "room_features",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_booking_features",
                table: "booking_features",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles",
                columns: new[] { "userid", "roleid" });

            migrationBuilder.CreateTable(
                name: "booking_room_features",
                columns: table => new
                {
                    booking_id = table.Column<int>(type: "integer", nullable: false),
                    room_feature_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_room_features", x => new { x.booking_id, x.room_feature_id });
                    table.ForeignKey(
                        name: "FK_booking_room_features_bookings_booking_id",
                        column: x => x.booking_id,
                        principalTable: "bookings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_room_features_room_features_room_feature_id",
                        column: x => x.room_feature_id,
                        principalTable: "room_features",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "room_room_features",
                columns: table => new
                {
                    room_feature_id = table.Column<int>(type: "integer", nullable: false),
                    room_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_room_features", x => new { x.room_feature_id, x.room_id });
                    table.ForeignKey(
                        name: "FK_room_room_features_room_features_room_feature_id",
                        column: x => x.room_feature_id,
                        principalTable: "room_features",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_room_room_features_rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_booking_room_features_room_feature_id",
                table: "booking_room_features",
                column: "room_feature_id");

            migrationBuilder.CreateIndex(
                name: "IX_room_room_features_room_id",
                table: "room_room_features",
                column: "room_id");

            migrationBuilder.AddForeignKey(
                name: "FK_bookingbookingfeature_booking_features_bookingpreferencesid",
                table: "bookingbookingfeature",
                column: "bookingpreferencesid",
                principalTable: "booking_features",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_room_usage_rooms_roomid",
                table: "room_usage",
                column: "roomid",
                principalTable: "rooms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_roles_roles_roleid",
                table: "user_roles",
                column: "roleid",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_roles_users_userid",
                table: "user_roles",
                column: "userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookingbookingfeature_booking_features_bookingpreferencesid",
                table: "bookingbookingfeature");

            migrationBuilder.DropForeignKey(
                name: "FK_room_usage_rooms_roomid",
                table: "room_usage");

            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_roles_roleid",
                table: "user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_users_userid",
                table: "user_roles");

            migrationBuilder.DropTable(
                name: "booking_room_features");

            migrationBuilder.DropTable(
                name: "room_room_features");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_room_usage",
                table: "room_usage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_room_features",
                table: "room_features");

            migrationBuilder.DropPrimaryKey(
                name: "PK_booking_features",
                table: "booking_features");

            migrationBuilder.RenameTable(
                name: "user_roles",
                newName: "aspnetuserroles");

            migrationBuilder.RenameTable(
                name: "room_usage",
                newName: "roomusages");

            migrationBuilder.RenameTable(
                name: "room_features",
                newName: "roomfeatures");

            migrationBuilder.RenameTable(
                name: "booking_features",
                newName: "bookingfeature");

            migrationBuilder.RenameIndex(
                name: "IX_user_roles_roleid",
                table: "aspnetuserroles",
                newName: "IX_aspnetuserroles_roleid");

            migrationBuilder.RenameIndex(
                name: "IX_room_usage_roomid",
                table: "roomusages",
                newName: "IX_roomusages_roomid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetuserroles",
                table: "aspnetuserroles",
                columns: new[] { "userid", "roleid" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_roomusages",
                table: "roomusages",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roomfeatures",
                table: "roomfeatures",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bookingfeature",
                table: "bookingfeature",
                column: "id");

            migrationBuilder.CreateTable(
                name: "booking_room_feature",
                columns: table => new
                {
                    booking_id = table.Column<int>(type: "integer", nullable: false),
                    room_feature_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_room_feature", x => new { x.booking_id, x.room_feature_id });
                    table.ForeignKey(
                        name: "FK_booking_room_feature_bookings_booking_id",
                        column: x => x.booking_id,
                        principalTable: "bookings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_room_feature_roomfeatures_room_feature_id",
                        column: x => x.room_feature_id,
                        principalTable: "roomfeatures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "room_room_feature",
                columns: table => new
                {
                    room_feature_id = table.Column<int>(type: "integer", nullable: false),
                    room_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_room_feature", x => new { x.room_feature_id, x.room_id });
                    table.ForeignKey(
                        name: "FK_room_room_feature_roomfeatures_room_feature_id",
                        column: x => x.room_feature_id,
                        principalTable: "roomfeatures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_room_room_feature_rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_booking_room_feature_room_feature_id",
                table: "booking_room_feature",
                column: "room_feature_id");

            migrationBuilder.CreateIndex(
                name: "IX_room_room_feature_room_id",
                table: "room_room_feature",
                column: "room_id");

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetuserroles_roles_roleid",
                table: "aspnetuserroles",
                column: "roleid",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetuserroles_users_userid",
                table: "aspnetuserroles",
                column: "userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookingbookingfeature_bookingfeature_bookingpreferencesid",
                table: "bookingbookingfeature",
                column: "bookingpreferencesid",
                principalTable: "bookingfeature",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_roomusages_rooms_roomid",
                table: "roomusages",
                column: "roomid",
                principalTable: "rooms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
