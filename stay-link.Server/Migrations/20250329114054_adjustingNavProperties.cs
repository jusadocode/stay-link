using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace stay_link.Server.Migrations
{
    /// <inheritdoc />
    public partial class adjustingNavProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_roleid",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_userid",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_userid",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_roleid",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_userid",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_userid",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_bookingroomfeature_bookings_BookingID",
                table: "bookingroomfeature");

            migrationBuilder.DropForeignKey(
                name: "FK_bookingroomfeature_roomfeatures_RoomFeatureID",
                table: "bookingroomfeature");

            migrationBuilder.DropForeignKey(
                name: "FK_roomroomfeature_roomfeatures_RoomFeatureID",
                table: "roomroomfeature");

            migrationBuilder.DropForeignKey(
                name: "FK_roomroomfeature_rooms_RoomID",
                table: "roomroomfeature");

            migrationBuilder.DropForeignKey(
                name: "FK_sessions_AspNetUsers_userid",
                table: "sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "aspnetusertokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "aspnetusers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "aspnetuserroles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "aspnetuserlogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "aspnetuserclaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "aspnetroles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "aspnetroleclaims");

            migrationBuilder.RenameColumn(
                name: "RoomID",
                table: "roomroomfeature",
                newName: "roomid");

            migrationBuilder.RenameColumn(
                name: "RoomFeatureID",
                table: "roomroomfeature",
                newName: "roomfeatureid");

            migrationBuilder.RenameIndex(
                name: "IX_roomroomfeature_RoomID",
                table: "roomroomfeature",
                newName: "IX_roomroomfeature_roomid");

            migrationBuilder.RenameColumn(
                name: "RoomFeatureID",
                table: "bookingroomfeature",
                newName: "roomfeatureid");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                table: "bookingroomfeature",
                newName: "bookingid");

            migrationBuilder.RenameIndex(
                name: "IX_bookingroomfeature_RoomFeatureID",
                table: "bookingroomfeature",
                newName: "IX_bookingroomfeature_roomfeatureid");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_roleid",
                table: "aspnetuserroles",
                newName: "IX_aspnetuserroles_roleid");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_userid",
                table: "aspnetuserlogins",
                newName: "IX_aspnetuserlogins_userid");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_userid",
                table: "aspnetuserclaims",
                newName: "IX_aspnetuserclaims_userid");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_roleid",
                table: "aspnetroleclaims",
                newName: "IX_aspnetroleclaims_roleid");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "roomfeatures",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "bookingid",
                table: "bookings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetusertokens",
                table: "aspnetusertokens",
                columns: new[] { "userid", "loginprovider", "name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetusers",
                table: "aspnetusers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetuserroles",
                table: "aspnetuserroles",
                columns: new[] { "userid", "roleid" });

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
                name: "bookingfeature",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    extracost = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookingfeature", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bookingbookingfeature",
                columns: table => new
                {
                    bookingpreferencesid = table.Column<int>(type: "integer", nullable: false),
                    bookingsid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookingbookingfeature", x => new { x.bookingpreferencesid, x.bookingsid });
                    table.ForeignKey(
                        name: "FK_bookingbookingfeature_bookingfeature_bookingpreferencesid",
                        column: x => x.bookingpreferencesid,
                        principalTable: "bookingfeature",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bookingbookingfeature_bookings_bookingsid",
                        column: x => x.bookingsid,
                        principalTable: "bookings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_roomusages_roomid",
                table: "roomusages",
                column: "roomid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bookings_bookingid",
                table: "bookings",
                column: "bookingid");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_roomid",
                table: "bookings",
                column: "roomid");

            migrationBuilder.CreateIndex(
                name: "IX_bookingbookingfeature_bookingsid",
                table: "bookingbookingfeature",
                column: "bookingsid");

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
                name: "FK_bookingroomfeature_bookings_bookingid",
                table: "bookingroomfeature",
                column: "bookingid",
                principalTable: "bookings",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookingroomfeature_roomfeatures_roomfeatureid",
                table: "bookingroomfeature",
                column: "roomfeatureid",
                principalTable: "roomfeatures",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_bookings_bookingid",
                table: "bookings",
                column: "bookingid",
                principalTable: "bookings",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_rooms_roomid",
                table: "bookings",
                column: "roomid",
                principalTable: "rooms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_roomroomfeature_roomfeatures_roomfeatureid",
                table: "roomroomfeature",
                column: "roomfeatureid",
                principalTable: "roomfeatures",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_roomroomfeature_rooms_roomid",
                table: "roomroomfeature",
                column: "roomid",
                principalTable: "rooms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_roomusages_rooms_roomid",
                table: "roomusages",
                column: "roomid",
                principalTable: "rooms",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_bookingroomfeature_bookings_bookingid",
                table: "bookingroomfeature");

            migrationBuilder.DropForeignKey(
                name: "FK_bookingroomfeature_roomfeatures_roomfeatureid",
                table: "bookingroomfeature");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_bookings_bookingid",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_rooms_roomid",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_roomroomfeature_roomfeatures_roomfeatureid",
                table: "roomroomfeature");

            migrationBuilder.DropForeignKey(
                name: "FK_roomroomfeature_rooms_roomid",
                table: "roomroomfeature");

            migrationBuilder.DropForeignKey(
                name: "FK_roomusages_rooms_roomid",
                table: "roomusages");

            migrationBuilder.DropForeignKey(
                name: "FK_sessions_aspnetusers_userid",
                table: "sessions");

            migrationBuilder.DropTable(
                name: "bookingbookingfeature");

            migrationBuilder.DropTable(
                name: "bookingfeature");

            migrationBuilder.DropIndex(
                name: "IX_roomusages_roomid",
                table: "roomusages");

            migrationBuilder.DropIndex(
                name: "IX_bookings_bookingid",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_bookings_roomid",
                table: "bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetusertokens",
                table: "aspnetusertokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetusers",
                table: "aspnetusers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetuserroles",
                table: "aspnetuserroles");

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

            migrationBuilder.DropColumn(
                name: "bookingid",
                table: "bookings");

            migrationBuilder.RenameTable(
                name: "aspnetusertokens",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "aspnetusers",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "aspnetuserroles",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "aspnetuserlogins",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "aspnetuserclaims",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "aspnetroles",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "aspnetroleclaims",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameColumn(
                name: "roomid",
                table: "roomroomfeature",
                newName: "RoomID");

            migrationBuilder.RenameColumn(
                name: "roomfeatureid",
                table: "roomroomfeature",
                newName: "RoomFeatureID");

            migrationBuilder.RenameIndex(
                name: "IX_roomroomfeature_roomid",
                table: "roomroomfeature",
                newName: "IX_roomroomfeature_RoomID");

            migrationBuilder.RenameColumn(
                name: "roomfeatureid",
                table: "bookingroomfeature",
                newName: "RoomFeatureID");

            migrationBuilder.RenameColumn(
                name: "bookingid",
                table: "bookingroomfeature",
                newName: "BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_bookingroomfeature_roomfeatureid",
                table: "bookingroomfeature",
                newName: "IX_bookingroomfeature_RoomFeatureID");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetuserroles_roleid",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_roleid");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetuserlogins_userid",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_userid");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetuserclaims_userid",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_userid");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetroleclaims_roleid",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_roleid");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "roomfeatures",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "userid", "loginprovider", "name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "userid", "roleid" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "loginprovider", "providerkey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_roleid",
                table: "AspNetRoleClaims",
                column: "roleid",
                principalTable: "AspNetRoles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_userid",
                table: "AspNetUserClaims",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_userid",
                table: "AspNetUserLogins",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_roleid",
                table: "AspNetUserRoles",
                column: "roleid",
                principalTable: "AspNetRoles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_userid",
                table: "AspNetUserRoles",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_userid",
                table: "AspNetUserTokens",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookingroomfeature_bookings_BookingID",
                table: "bookingroomfeature",
                column: "BookingID",
                principalTable: "bookings",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookingroomfeature_roomfeatures_RoomFeatureID",
                table: "bookingroomfeature",
                column: "RoomFeatureID",
                principalTable: "roomfeatures",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_roomroomfeature_roomfeatures_RoomFeatureID",
                table: "roomroomfeature",
                column: "RoomFeatureID",
                principalTable: "roomfeatures",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_roomroomfeature_rooms_RoomID",
                table: "roomroomfeature",
                column: "RoomID",
                principalTable: "rooms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sessions_AspNetUsers_userid",
                table: "sessions",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
