using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stay_link.Server.Migrations
{
    /// <inheritdoc />
    public partial class NoSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_booking_room_features_bookings_booking_id",
                table: "booking_room_features");

            migrationBuilder.DropForeignKey(
                name: "FK_booking_room_features_room_features_room_feature_id",
                table: "booking_room_features");

            migrationBuilder.DropForeignKey(
                name: "FK_bookingbookingfeature_booking_features_bookingpreferencesid",
                table: "bookingbookingfeature");

            migrationBuilder.DropForeignKey(
                name: "FK_bookingbookingfeature_bookings_bookingsid",
                table: "bookingbookingfeature");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_bookings_bookingid",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_rooms_roomid",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_role_claims_roles_roleid",
                table: "role_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_room_room_features_room_features_room_feature_id",
                table: "room_room_features");

            migrationBuilder.DropForeignKey(
                name: "FK_room_room_features_rooms_room_id",
                table: "room_room_features");

            migrationBuilder.DropForeignKey(
                name: "FK_room_usage_rooms_roomid",
                table: "room_usage");

            migrationBuilder.DropForeignKey(
                name: "FK_sessions_users_userid",
                table: "sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_user_claims_users_userid",
                table: "user_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_user_logins_users_userid",
                table: "user_logins");

            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_roles_roleid",
                table: "user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_users_userid",
                table: "user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_user_tokens_users_userid",
                table: "user_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sessions",
                table: "sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rooms",
                table: "rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hotels",
                table: "hotels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bookings",
                table: "bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bookingbookingfeature",
                table: "bookingbookingfeature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_tokens",
                table: "user_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_logins",
                table: "user_logins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_claims",
                table: "user_claims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_room_usage",
                table: "room_usage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_room_features",
                table: "room_features");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role_claims",
                table: "role_claims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_booking_features",
                table: "booking_features");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "sessions",
                newName: "Sessions");

            migrationBuilder.RenameTable(
                name: "rooms",
                newName: "Rooms");

            migrationBuilder.RenameTable(
                name: "roles",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "hotels",
                newName: "Hotels");

            migrationBuilder.RenameTable(
                name: "bookings",
                newName: "Bookings");

            migrationBuilder.RenameTable(
                name: "bookingbookingfeature",
                newName: "BookingBookingFeature");

            migrationBuilder.RenameTable(
                name: "user_tokens",
                newName: "UserTokens");

            migrationBuilder.RenameTable(
                name: "user_roles",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "user_logins",
                newName: "UserLogins");

            migrationBuilder.RenameTable(
                name: "user_claims",
                newName: "UserClaims");

            migrationBuilder.RenameTable(
                name: "room_usage",
                newName: "RoomUsages");

            migrationBuilder.RenameTable(
                name: "room_features",
                newName: "RoomFeatures");

            migrationBuilder.RenameTable(
                name: "role_claims",
                newName: "RoleClaims");

            migrationBuilder.RenameTable(
                name: "booking_features",
                newName: "BookingFeature");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "twofactorenabled",
                table: "Users",
                newName: "TwoFactorEnabled");

            migrationBuilder.RenameColumn(
                name: "securitystamp",
                table: "Users",
                newName: "SecurityStamp");

            migrationBuilder.RenameColumn(
                name: "phonenumberconfirmed",
                table: "Users",
                newName: "PhoneNumberConfirmed");

            migrationBuilder.RenameColumn(
                name: "phonenumber",
                table: "Users",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "passwordhash",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "normalizedusername",
                table: "Users",
                newName: "NormalizedUserName");

            migrationBuilder.RenameColumn(
                name: "normalizedemail",
                table: "Users",
                newName: "NormalizedEmail");

            migrationBuilder.RenameColumn(
                name: "lockoutend",
                table: "Users",
                newName: "LockoutEnd");

            migrationBuilder.RenameColumn(
                name: "lockoutenabled",
                table: "Users",
                newName: "LockoutEnabled");

            migrationBuilder.RenameColumn(
                name: "emailconfirmed",
                table: "Users",
                newName: "EmailConfirmed");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "concurrencystamp",
                table: "Users",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "accessfailedcount",
                table: "Users",
                newName: "AccessFailedCount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "Sessions",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "lastrefreshtoken",
                table: "Sessions",
                newName: "LastRefreshToken");

            migrationBuilder.RenameColumn(
                name: "isrevoked",
                table: "Sessions",
                newName: "IsRevoked");

            migrationBuilder.RenameColumn(
                name: "initiatedat",
                table: "Sessions",
                newName: "InitiatedAt");

            migrationBuilder.RenameColumn(
                name: "expiresat",
                table: "Sessions",
                newName: "ExpiresAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Sessions",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_sessions_userid",
                table: "Sessions",
                newName: "IX_Sessions_UserId");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Rooms",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "summary",
                table: "Rooms",
                newName: "Summary");

            migrationBuilder.RenameColumn(
                name: "roomtype",
                table: "Rooms",
                newName: "RoomType");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Rooms",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "maxoccupancy",
                table: "Rooms",
                newName: "MaxOccupancy");

            migrationBuilder.RenameColumn(
                name: "hotelid",
                table: "Rooms",
                newName: "HotelID");

            migrationBuilder.RenameColumn(
                name: "floornumber",
                table: "Rooms",
                newName: "FloorNumber");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Rooms",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "normalizedname",
                table: "Roles",
                newName: "NormalizedName");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Roles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "concurrencystamp",
                table: "Roles",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Hotels",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "imageurl",
                table: "Hotels",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Hotels",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Hotels",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "Bookings",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "totalprice",
                table: "Bookings",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "totalguests",
                table: "Bookings",
                newName: "TotalGuests");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Bookings",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "roomid",
                table: "Bookings",
                newName: "RoomId");

            migrationBuilder.RenameColumn(
                name: "hotelid",
                table: "Bookings",
                newName: "HotelId");

            migrationBuilder.RenameColumn(
                name: "checkoutdate",
                table: "Bookings",
                newName: "CheckOutDate");

            migrationBuilder.RenameColumn(
                name: "checkindate",
                table: "Bookings",
                newName: "CheckInDate");

            migrationBuilder.RenameColumn(
                name: "breakfastrequests",
                table: "Bookings",
                newName: "BreakfastRequests");

            migrationBuilder.RenameColumn(
                name: "bookingid",
                table: "Bookings",
                newName: "BookingID");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Bookings",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_bookings_roomid",
                table: "Bookings",
                newName: "IX_Bookings_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_bookings_bookingid",
                table: "Bookings",
                newName: "IX_Bookings_BookingID");

            migrationBuilder.RenameColumn(
                name: "bookingsid",
                table: "BookingBookingFeature",
                newName: "BookingsID");

            migrationBuilder.RenameColumn(
                name: "bookingpreferencesid",
                table: "BookingBookingFeature",
                newName: "BookingPreferencesId");

            migrationBuilder.RenameIndex(
                name: "IX_bookingbookingfeature_bookingsid",
                table: "BookingBookingFeature",
                newName: "IX_BookingBookingFeature_BookingsID");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "UserTokens",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "UserTokens",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "loginprovider",
                table: "UserTokens",
                newName: "LoginProvider");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "UserTokens",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "roleid",
                table: "UserRoles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "UserRoles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_user_roles_roleid",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "UserLogins",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "providerdisplayname",
                table: "UserLogins",
                newName: "ProviderDisplayName");

            migrationBuilder.RenameColumn(
                name: "providerkey",
                table: "UserLogins",
                newName: "ProviderKey");

            migrationBuilder.RenameColumn(
                name: "loginprovider",
                table: "UserLogins",
                newName: "LoginProvider");

            migrationBuilder.RenameIndex(
                name: "IX_user_logins_userid",
                table: "UserLogins",
                newName: "IX_UserLogins_UserId");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "UserClaims",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "claimvalue",
                table: "UserClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "claimtype",
                table: "UserClaims",
                newName: "ClaimType");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "UserClaims",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_user_claims_userid",
                table: "UserClaims",
                newName: "IX_UserClaims_UserId");

            migrationBuilder.RenameColumn(
                name: "timesbookedthisyear",
                table: "RoomUsages",
                newName: "TimesBookedThisYear");

            migrationBuilder.RenameColumn(
                name: "timesbookedsincemaintenance",
                table: "RoomUsages",
                newName: "TimesBookedSinceMaintenance");

            migrationBuilder.RenameColumn(
                name: "roomid",
                table: "RoomUsages",
                newName: "RoomID");

            migrationBuilder.RenameColumn(
                name: "generalwear",
                table: "RoomUsages",
                newName: "GeneralWear");

            migrationBuilder.RenameColumn(
                name: "cleaningstate",
                table: "RoomUsages",
                newName: "CleaningState");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "RoomUsages",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_room_usage_roomid",
                table: "RoomUsages",
                newName: "IX_RoomUsages_RoomID");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "RoomFeatures",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "extracost",
                table: "RoomFeatures",
                newName: "ExtraCost");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "RoomFeatures",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "RoomFeatures",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "roleid",
                table: "RoleClaims",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "claimvalue",
                table: "RoleClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "claimtype",
                table: "RoleClaims",
                newName: "ClaimType");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "RoleClaims",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_role_claims_roleid",
                table: "RoleClaims",
                newName: "IX_RoleClaims_RoleId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "BookingFeature",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "extracost",
                table: "BookingFeature",
                newName: "ExtraCost");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "BookingFeature",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "BookingFeature",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Rooms",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hotels",
                table: "Hotels",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingBookingFeature",
                table: "BookingBookingFeature",
                columns: new[] { "BookingPreferencesId", "BookingsID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTokens",
                table: "UserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogins",
                table: "UserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClaims",
                table: "UserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomUsages",
                table: "RoomUsages",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomFeatures",
                table: "RoomFeatures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleClaims",
                table: "RoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingFeature",
                table: "BookingFeature",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_room_features_Bookings_booking_id",
                table: "booking_room_features",
                column: "booking_id",
                principalTable: "Bookings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_booking_room_features_RoomFeatures_room_feature_id",
                table: "booking_room_features",
                column: "room_feature_id",
                principalTable: "RoomFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingBookingFeature_BookingFeature_BookingPreferencesId",
                table: "BookingBookingFeature",
                column: "BookingPreferencesId",
                principalTable: "BookingFeature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingBookingFeature_Bookings_BookingsID",
                table: "BookingBookingFeature",
                column: "BookingsID",
                principalTable: "Bookings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Bookings_BookingID",
                table: "Bookings",
                column: "BookingID",
                principalTable: "Bookings",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaims_Roles_RoleId",
                table: "RoleClaims",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_room_room_features_RoomFeatures_room_feature_id",
                table: "room_room_features",
                column: "room_feature_id",
                principalTable: "RoomFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_room_room_features_Rooms_room_id",
                table: "room_room_features",
                column: "room_id",
                principalTable: "Rooms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUsages_Rooms_RoomID",
                table: "RoomUsages",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_UserId",
                table: "Sessions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_Users_UserId",
                table: "UserClaims",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_Users_UserId",
                table: "UserLogins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_Users_UserId",
                table: "UserTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_booking_room_features_Bookings_booking_id",
                table: "booking_room_features");

            migrationBuilder.DropForeignKey(
                name: "FK_booking_room_features_RoomFeatures_room_feature_id",
                table: "booking_room_features");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingBookingFeature_BookingFeature_BookingPreferencesId",
                table: "BookingBookingFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingBookingFeature_Bookings_BookingsID",
                table: "BookingBookingFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Bookings_BookingID",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaims_Roles_RoleId",
                table: "RoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_room_room_features_RoomFeatures_room_feature_id",
                table: "room_room_features");

            migrationBuilder.DropForeignKey(
                name: "FK_room_room_features_Rooms_room_id",
                table: "room_room_features");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomUsages_Rooms_RoomID",
                table: "RoomUsages");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_UserId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_Users_UserId",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_Users_UserId",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_Users_UserId",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hotels",
                table: "Hotels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingBookingFeature",
                table: "BookingBookingFeature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTokens",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogins",
                table: "UserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClaims",
                table: "UserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomUsages",
                table: "RoomUsages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomFeatures",
                table: "RoomFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleClaims",
                table: "RoleClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingFeature",
                table: "BookingFeature");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Rooms");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Sessions",
                newName: "sessions");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "rooms");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "roles");

            migrationBuilder.RenameTable(
                name: "Hotels",
                newName: "hotels");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "bookings");

            migrationBuilder.RenameTable(
                name: "BookingBookingFeature",
                newName: "bookingbookingfeature");

            migrationBuilder.RenameTable(
                name: "UserTokens",
                newName: "user_tokens");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "user_roles");

            migrationBuilder.RenameTable(
                name: "UserLogins",
                newName: "user_logins");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                newName: "user_claims");

            migrationBuilder.RenameTable(
                name: "RoomUsages",
                newName: "room_usage");

            migrationBuilder.RenameTable(
                name: "RoomFeatures",
                newName: "room_features");

            migrationBuilder.RenameTable(
                name: "RoleClaims",
                newName: "role_claims");

            migrationBuilder.RenameTable(
                name: "BookingFeature",
                newName: "booking_features");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "TwoFactorEnabled",
                table: "users",
                newName: "twofactorenabled");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                table: "users",
                newName: "securitystamp");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberConfirmed",
                table: "users",
                newName: "phonenumberconfirmed");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "users",
                newName: "phonenumber");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "users",
                newName: "passwordhash");

            migrationBuilder.RenameColumn(
                name: "NormalizedUserName",
                table: "users",
                newName: "normalizedusername");

            migrationBuilder.RenameColumn(
                name: "NormalizedEmail",
                table: "users",
                newName: "normalizedemail");

            migrationBuilder.RenameColumn(
                name: "LockoutEnd",
                table: "users",
                newName: "lockoutend");

            migrationBuilder.RenameColumn(
                name: "LockoutEnabled",
                table: "users",
                newName: "lockoutenabled");

            migrationBuilder.RenameColumn(
                name: "EmailConfirmed",
                table: "users",
                newName: "emailconfirmed");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "users",
                newName: "concurrencystamp");

            migrationBuilder.RenameColumn(
                name: "AccessFailedCount",
                table: "users",
                newName: "accessfailedcount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "sessions",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "LastRefreshToken",
                table: "sessions",
                newName: "lastrefreshtoken");

            migrationBuilder.RenameColumn(
                name: "IsRevoked",
                table: "sessions",
                newName: "isrevoked");

            migrationBuilder.RenameColumn(
                name: "InitiatedAt",
                table: "sessions",
                newName: "initiatedat");

            migrationBuilder.RenameColumn(
                name: "ExpiresAt",
                table: "sessions",
                newName: "expiresat");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "sessions",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_UserId",
                table: "sessions",
                newName: "IX_sessions_userid");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "rooms",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "rooms",
                newName: "summary");

            migrationBuilder.RenameColumn(
                name: "RoomType",
                table: "rooms",
                newName: "roomtype");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "rooms",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "MaxOccupancy",
                table: "rooms",
                newName: "maxoccupancy");

            migrationBuilder.RenameColumn(
                name: "HotelID",
                table: "rooms",
                newName: "hotelid");

            migrationBuilder.RenameColumn(
                name: "FloorNumber",
                table: "rooms",
                newName: "floornumber");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "rooms",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "NormalizedName",
                table: "roles",
                newName: "normalizedname");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "roles",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "roles",
                newName: "concurrencystamp");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "roles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "hotels",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "hotels",
                newName: "imageurl");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "hotels",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "hotels",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "bookings",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "bookings",
                newName: "totalprice");

            migrationBuilder.RenameColumn(
                name: "TotalGuests",
                table: "bookings",
                newName: "totalguests");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "bookings",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "bookings",
                newName: "roomid");

            migrationBuilder.RenameColumn(
                name: "HotelId",
                table: "bookings",
                newName: "hotelid");

            migrationBuilder.RenameColumn(
                name: "CheckOutDate",
                table: "bookings",
                newName: "checkoutdate");

            migrationBuilder.RenameColumn(
                name: "CheckInDate",
                table: "bookings",
                newName: "checkindate");

            migrationBuilder.RenameColumn(
                name: "BreakfastRequests",
                table: "bookings",
                newName: "breakfastrequests");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                table: "bookings",
                newName: "bookingid");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "bookings",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_RoomId",
                table: "bookings",
                newName: "IX_bookings_roomid");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_BookingID",
                table: "bookings",
                newName: "IX_bookings_bookingid");

            migrationBuilder.RenameColumn(
                name: "BookingsID",
                table: "bookingbookingfeature",
                newName: "bookingsid");

            migrationBuilder.RenameColumn(
                name: "BookingPreferencesId",
                table: "bookingbookingfeature",
                newName: "bookingpreferencesid");

            migrationBuilder.RenameIndex(
                name: "IX_BookingBookingFeature_BookingsID",
                table: "bookingbookingfeature",
                newName: "IX_bookingbookingfeature_bookingsid");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "user_tokens",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "user_tokens",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                table: "user_tokens",
                newName: "loginprovider");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "user_tokens",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "user_roles",
                newName: "roleid");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "user_roles",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                table: "user_roles",
                newName: "IX_user_roles_roleid");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "user_logins",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "ProviderDisplayName",
                table: "user_logins",
                newName: "providerdisplayname");

            migrationBuilder.RenameColumn(
                name: "ProviderKey",
                table: "user_logins",
                newName: "providerkey");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                table: "user_logins",
                newName: "loginprovider");

            migrationBuilder.RenameIndex(
                name: "IX_UserLogins_UserId",
                table: "user_logins",
                newName: "IX_user_logins_userid");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "user_claims",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "user_claims",
                newName: "claimvalue");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "user_claims",
                newName: "claimtype");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "user_claims",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_UserClaims_UserId",
                table: "user_claims",
                newName: "IX_user_claims_userid");

            migrationBuilder.RenameColumn(
                name: "TimesBookedThisYear",
                table: "room_usage",
                newName: "timesbookedthisyear");

            migrationBuilder.RenameColumn(
                name: "TimesBookedSinceMaintenance",
                table: "room_usage",
                newName: "timesbookedsincemaintenance");

            migrationBuilder.RenameColumn(
                name: "RoomID",
                table: "room_usage",
                newName: "roomid");

            migrationBuilder.RenameColumn(
                name: "GeneralWear",
                table: "room_usage",
                newName: "generalwear");

            migrationBuilder.RenameColumn(
                name: "CleaningState",
                table: "room_usage",
                newName: "cleaningstate");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "room_usage",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_RoomUsages_RoomID",
                table: "room_usage",
                newName: "IX_room_usage_roomid");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "room_features",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ExtraCost",
                table: "room_features",
                newName: "extracost");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "room_features",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "room_features",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "role_claims",
                newName: "roleid");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "role_claims",
                newName: "claimvalue");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "role_claims",
                newName: "claimtype");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "role_claims",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_RoleClaims_RoleId",
                table: "role_claims",
                newName: "IX_role_claims_roleid");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "booking_features",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ExtraCost",
                table: "booking_features",
                newName: "extracost");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "booking_features",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "booking_features",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sessions",
                table: "sessions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rooms",
                table: "rooms",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                table: "roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hotels",
                table: "hotels",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bookings",
                table: "bookings",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bookingbookingfeature",
                table: "bookingbookingfeature",
                columns: new[] { "bookingpreferencesid", "bookingsid" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_tokens",
                table: "user_tokens",
                columns: new[] { "userid", "loginprovider", "name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles",
                columns: new[] { "userid", "roleid" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_logins",
                table: "user_logins",
                columns: new[] { "loginprovider", "providerkey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_claims",
                table: "user_claims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_room_usage",
                table: "room_usage",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_room_features",
                table: "room_features",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_claims",
                table: "role_claims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_booking_features",
                table: "booking_features",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_room_features_bookings_booking_id",
                table: "booking_room_features",
                column: "booking_id",
                principalTable: "bookings",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_booking_room_features_room_features_room_feature_id",
                table: "booking_room_features",
                column: "room_feature_id",
                principalTable: "room_features",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookingbookingfeature_booking_features_bookingpreferencesid",
                table: "bookingbookingfeature",
                column: "bookingpreferencesid",
                principalTable: "booking_features",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookingbookingfeature_bookings_bookingsid",
                table: "bookingbookingfeature",
                column: "bookingsid",
                principalTable: "bookings",
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
                name: "FK_role_claims_roles_roleid",
                table: "role_claims",
                column: "roleid",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_room_room_features_room_features_room_feature_id",
                table: "room_room_features",
                column: "room_feature_id",
                principalTable: "room_features",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_room_room_features_rooms_room_id",
                table: "room_room_features",
                column: "room_id",
                principalTable: "rooms",
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
                name: "FK_sessions_users_userid",
                table: "sessions",
                column: "userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_claims_users_userid",
                table: "user_claims",
                column: "userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_logins_users_userid",
                table: "user_logins",
                column: "userid",
                principalTable: "users",
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

            migrationBuilder.AddForeignKey(
                name: "FK_user_tokens_users_userid",
                table: "user_tokens",
                column: "userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
