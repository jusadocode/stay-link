using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stay_link.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbTableRenames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aspnetroleclaims_aspnetroles_roleid",
                table: "aspnetroleclaims");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetuserclaims_aspnetusers_userid",
                table: "aspnetuserclaims");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetuserlogins_aspnetusers_userid",
                table: "aspnetuserlogins");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetuserroles_aspnetroles_roleid",
                table: "aspnetuserroles");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetuserroles_aspnetusers_userid",
                table: "aspnetuserroles");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetusertokens_aspnetusers_userid",
                table: "aspnetusertokens");

            migrationBuilder.DropForeignKey(
                name: "FK_sessions_aspnetusers_userid",
                table: "sessions");

            migrationBuilder.DropTable(
                name: "bookingroomfeature");

            migrationBuilder.DropTable(
                name: "roomroomfeature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetusertokens",
                table: "aspnetusertokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetusers",
                table: "aspnetusers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetuserlogins",
                table: "aspnetuserlogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetuserclaims",
                table: "aspnetuserclaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetroles",
                table: "aspnetroles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetroleclaims",
                table: "aspnetroleclaims");

            migrationBuilder.RenameTable(
                name: "aspnetusertokens",
                newName: "usertokens");

            migrationBuilder.RenameTable(
                name: "aspnetusers",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "aspnetuserlogins",
                newName: "userlogins");

            migrationBuilder.RenameTable(
                name: "aspnetuserclaims",
                newName: "userclaims");

            migrationBuilder.RenameTable(
                name: "aspnetroles",
                newName: "roles");

            migrationBuilder.RenameTable(
                name: "aspnetroleclaims",
                newName: "roleclaims");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetuserlogins_userid",
                table: "userlogins",
                newName: "IX_userlogins_userid");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetuserclaims_userid",
                table: "userclaims",
                newName: "IX_userclaims_userid");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetroleclaims_roleid",
                table: "roleclaims",
                newName: "IX_roleclaims_roleid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usertokens",
                table: "usertokens",
                columns: new[] { "userid", "loginprovider", "name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userlogins",
                table: "userlogins",
                columns: new[] { "loginprovider", "providerkey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_userclaims",
                table: "userclaims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                table: "roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roleclaims",
                table: "roleclaims",
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
                name: "FK_roleclaims_roles_roleid",
                table: "roleclaims",
                column: "roleid",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sessions_users_userid",
                table: "sessions",
                column: "userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userclaims_users_userid",
                table: "userclaims",
                column: "userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userlogins_users_userid",
                table: "userlogins",
                column: "userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_usertokens_users_userid",
                table: "usertokens",
                column: "userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aspnetuserroles_roles_roleid",
                table: "aspnetuserroles");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetuserroles_users_userid",
                table: "aspnetuserroles");

            migrationBuilder.DropForeignKey(
                name: "FK_roleclaims_roles_roleid",
                table: "roleclaims");

            migrationBuilder.DropForeignKey(
                name: "FK_sessions_users_userid",
                table: "sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_userclaims_users_userid",
                table: "userclaims");

            migrationBuilder.DropForeignKey(
                name: "FK_userlogins_users_userid",
                table: "userlogins");

            migrationBuilder.DropForeignKey(
                name: "FK_usertokens_users_userid",
                table: "usertokens");

            migrationBuilder.DropTable(
                name: "booking_room_feature");

            migrationBuilder.DropTable(
                name: "room_room_feature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usertokens",
                table: "usertokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userlogins",
                table: "userlogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userclaims",
                table: "userclaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roleclaims",
                table: "roleclaims");

            migrationBuilder.RenameTable(
                name: "usertokens",
                newName: "aspnetusertokens");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "aspnetusers");

            migrationBuilder.RenameTable(
                name: "userlogins",
                newName: "aspnetuserlogins");

            migrationBuilder.RenameTable(
                name: "userclaims",
                newName: "aspnetuserclaims");

            migrationBuilder.RenameTable(
                name: "roles",
                newName: "aspnetroles");

            migrationBuilder.RenameTable(
                name: "roleclaims",
                newName: "aspnetroleclaims");

            migrationBuilder.RenameIndex(
                name: "IX_userlogins_userid",
                table: "aspnetuserlogins",
                newName: "IX_aspnetuserlogins_userid");

            migrationBuilder.RenameIndex(
                name: "IX_userclaims_userid",
                table: "aspnetuserclaims",
                newName: "IX_aspnetuserclaims_userid");

            migrationBuilder.RenameIndex(
                name: "IX_roleclaims_roleid",
                table: "aspnetroleclaims",
                newName: "IX_aspnetroleclaims_roleid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetusertokens",
                table: "aspnetusertokens",
                columns: new[] { "userid", "loginprovider", "name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetusers",
                table: "aspnetusers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetuserlogins",
                table: "aspnetuserlogins",
                columns: new[] { "loginprovider", "providerkey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetuserclaims",
                table: "aspnetuserclaims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetroles",
                table: "aspnetroles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetroleclaims",
                table: "aspnetroleclaims",
                column: "id");

            migrationBuilder.CreateTable(
                name: "bookingroomfeature",
                columns: table => new
                {
                    bookingid = table.Column<int>(type: "integer", nullable: false),
                    roomfeatureid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookingroomfeature", x => new { x.bookingid, x.roomfeatureid });
                    table.ForeignKey(
                        name: "FK_bookingroomfeature_bookings_bookingid",
                        column: x => x.bookingid,
                        principalTable: "bookings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bookingroomfeature_roomfeatures_roomfeatureid",
                        column: x => x.roomfeatureid,
                        principalTable: "roomfeatures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "roomroomfeature",
                columns: table => new
                {
                    roomfeatureid = table.Column<int>(type: "integer", nullable: false),
                    roomid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roomroomfeature", x => new { x.roomfeatureid, x.roomid });
                    table.ForeignKey(
                        name: "FK_roomroomfeature_roomfeatures_roomfeatureid",
                        column: x => x.roomfeatureid,
                        principalTable: "roomfeatures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_roomroomfeature_rooms_roomid",
                        column: x => x.roomid,
                        principalTable: "rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookingroomfeature_roomfeatureid",
                table: "bookingroomfeature",
                column: "roomfeatureid");

            migrationBuilder.CreateIndex(
                name: "IX_roomroomfeature_roomid",
                table: "roomroomfeature",
                column: "roomid");

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetroleclaims_aspnetroles_roleid",
                table: "aspnetroleclaims",
                column: "roleid",
                principalTable: "aspnetroles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetuserclaims_aspnetusers_userid",
                table: "aspnetuserclaims",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetuserlogins_aspnetusers_userid",
                table: "aspnetuserlogins",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetuserroles_aspnetroles_roleid",
                table: "aspnetuserroles",
                column: "roleid",
                principalTable: "aspnetroles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetuserroles_aspnetusers_userid",
                table: "aspnetuserroles",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetusertokens_aspnetusers_userid",
                table: "aspnetusertokens",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sessions_aspnetusers_userid",
                table: "sessions",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
