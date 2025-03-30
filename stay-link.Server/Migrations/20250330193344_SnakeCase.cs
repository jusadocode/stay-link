using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stay_link.Server.Migrations
{
    /// <inheritdoc />
    public partial class SnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingBookingFeature",
                table: "BookingBookingFeature");

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
                newName: "room_usages");

            migrationBuilder.RenameTable(
                name: "RoomFeatures",
                newName: "room_features");

            migrationBuilder.RenameTable(
                name: "RoleClaims",
                newName: "role_claims");

            migrationBuilder.RenameTable(
                name: "BookingFeature",
                newName: "booking_feature");

            migrationBuilder.RenameTable(
                name: "BookingBookingFeature",
                newName: "booking_booking_feature");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "users",
                newName: "user_name");

            migrationBuilder.RenameColumn(
                name: "TwoFactorEnabled",
                table: "users",
                newName: "two_factor_enabled");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                table: "users",
                newName: "security_stamp");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberConfirmed",
                table: "users",
                newName: "phone_number_confirmed");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "users",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "NormalizedUserName",
                table: "users",
                newName: "normalized_user_name");

            migrationBuilder.RenameColumn(
                name: "NormalizedEmail",
                table: "users",
                newName: "normalized_email");

            migrationBuilder.RenameColumn(
                name: "LockoutEnd",
                table: "users",
                newName: "lockout_end");

            migrationBuilder.RenameColumn(
                name: "LockoutEnabled",
                table: "users",
                newName: "lockout_enabled");

            migrationBuilder.RenameColumn(
                name: "EmailConfirmed",
                table: "users",
                newName: "email_confirmed");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "users",
                newName: "concurrency_stamp");

            migrationBuilder.RenameColumn(
                name: "AccessFailedCount",
                table: "users",
                newName: "access_failed_count");

            migrationBuilder.RenameIndex(
                name: "UserNameIndex",
                table: "users",
                newName: "user_name_index");

            migrationBuilder.RenameIndex(
                name: "EmailIndex",
                table: "users",
                newName: "email_index");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "sessions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "sessions",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "LastRefreshToken",
                table: "sessions",
                newName: "last_refresh_token");

            migrationBuilder.RenameColumn(
                name: "IsRevoked",
                table: "sessions",
                newName: "is_revoked");

            migrationBuilder.RenameColumn(
                name: "InitiatedAt",
                table: "sessions",
                newName: "initiated_at");

            migrationBuilder.RenameColumn(
                name: "ExpiresAt",
                table: "sessions",
                newName: "expires_at");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_UserId",
                table: "sessions",
                newName: "i_x_sessions_user_id");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "rooms",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "rooms",
                newName: "summary");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "rooms",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "RoomType",
                table: "rooms",
                newName: "room_type");

            migrationBuilder.RenameColumn(
                name: "MaxOccupancy",
                table: "rooms",
                newName: "max_occupancy");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "rooms",
                newName: "image_url");

            migrationBuilder.RenameColumn(
                name: "HotelID",
                table: "rooms",
                newName: "hotel_i_d");

            migrationBuilder.RenameColumn(
                name: "FloorNumber",
                table: "rooms",
                newName: "floor_number");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "rooms",
                newName: "i_d");

            migrationBuilder.RenameIndex(
                name: "IX_room_room_features_room_id",
                table: "room_room_features",
                newName: "i_x_room_room_features_room_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "roles",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "roles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "NormalizedName",
                table: "roles",
                newName: "normalized_name");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "roles",
                newName: "concurrency_stamp");

            migrationBuilder.RenameIndex(
                name: "RoleNameIndex",
                table: "roles",
                newName: "role_name_index");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "hotels",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "hotels",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "hotels",
                newName: "image_url");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "hotels",
                newName: "i_d");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "bookings",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "bookings",
                newName: "user_i_d");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "bookings",
                newName: "total_price");

            migrationBuilder.RenameColumn(
                name: "TotalGuests",
                table: "bookings",
                newName: "total_guests");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "bookings",
                newName: "room_id");

            migrationBuilder.RenameColumn(
                name: "HotelId",
                table: "bookings",
                newName: "hotel_id");

            migrationBuilder.RenameColumn(
                name: "CheckOutDate",
                table: "bookings",
                newName: "check_out_date");

            migrationBuilder.RenameColumn(
                name: "CheckInDate",
                table: "bookings",
                newName: "check_in_date");

            migrationBuilder.RenameColumn(
                name: "BreakfastRequests",
                table: "bookings",
                newName: "breakfast_requests");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                table: "bookings",
                newName: "booking_i_d");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "bookings",
                newName: "i_d");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_RoomId",
                table: "bookings",
                newName: "i_x_bookings_room_id");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_BookingID",
                table: "bookings",
                newName: "i_x_bookings_booking_i_d");

            migrationBuilder.RenameIndex(
                name: "IX_booking_room_features_room_feature_id",
                table: "booking_room_features",
                newName: "i_x_booking_room_features_room_feature_id");

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
                newName: "login_provider");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "user_tokens",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "user_roles",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "user_roles",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                table: "user_roles",
                newName: "i_x_user_roles_role_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "user_logins",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ProviderDisplayName",
                table: "user_logins",
                newName: "provider_display_name");

            migrationBuilder.RenameColumn(
                name: "ProviderKey",
                table: "user_logins",
                newName: "provider_key");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                table: "user_logins",
                newName: "login_provider");

            migrationBuilder.RenameIndex(
                name: "IX_UserLogins_UserId",
                table: "user_logins",
                newName: "i_x_user_logins_user_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "user_claims",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "user_claims",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "user_claims",
                newName: "claim_value");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "user_claims",
                newName: "claim_type");

            migrationBuilder.RenameIndex(
                name: "IX_UserClaims_UserId",
                table: "user_claims",
                newName: "i_x_user_claims_user_id");

            migrationBuilder.RenameColumn(
                name: "TimesBookedThisYear",
                table: "room_usages",
                newName: "times_booked_this_year");

            migrationBuilder.RenameColumn(
                name: "TimesBookedSinceMaintenance",
                table: "room_usages",
                newName: "times_booked_since_maintenance");

            migrationBuilder.RenameColumn(
                name: "RoomID",
                table: "room_usages",
                newName: "room_i_d");

            migrationBuilder.RenameColumn(
                name: "GeneralWear",
                table: "room_usages",
                newName: "general_wear");

            migrationBuilder.RenameColumn(
                name: "CleaningState",
                table: "room_usages",
                newName: "cleaning_state");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "room_usages",
                newName: "i_d");

            migrationBuilder.RenameIndex(
                name: "IX_RoomUsages_RoomID",
                table: "room_usages",
                newName: "i_x_room_usages_room_i_d");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "room_features",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "room_features",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "room_features",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ExtraCost",
                table: "room_features",
                newName: "extra_cost");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "role_claims",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "role_claims",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "role_claims",
                newName: "claim_value");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "role_claims",
                newName: "claim_type");

            migrationBuilder.RenameIndex(
                name: "IX_RoleClaims_RoleId",
                table: "role_claims",
                newName: "i_x_role_claims_role_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "booking_feature",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "booking_feature",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "booking_feature",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ExtraCost",
                table: "booking_feature",
                newName: "extra_cost");

            migrationBuilder.RenameColumn(
                name: "BookingsID",
                table: "booking_booking_feature",
                newName: "bookings_i_d");

            migrationBuilder.RenameColumn(
                name: "BookingPreferencesId",
                table: "booking_booking_feature",
                newName: "booking_preferences_id");

            migrationBuilder.RenameIndex(
                name: "IX_BookingBookingFeature_BookingsID",
                table: "booking_booking_feature",
                newName: "i_x_booking_booking_feature_bookings_i_d");

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
                column: "i_d");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                table: "roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hotels",
                table: "hotels",
                column: "i_d");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bookings",
                table: "bookings",
                column: "i_d");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_tokens",
                table: "user_tokens",
                columns: new[] { "user_id", "login_provider", "name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles",
                columns: new[] { "user_id", "role_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_logins",
                table: "user_logins",
                columns: new[] { "login_provider", "provider_key" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_claims",
                table: "user_claims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_room_usages",
                table: "room_usages",
                column: "i_d");

            migrationBuilder.AddPrimaryKey(
                name: "PK_room_features",
                table: "room_features",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_claims",
                table: "role_claims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_booking_feature",
                table: "booking_feature",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_booking_booking_feature",
                table: "booking_booking_feature",
                columns: new[] { "booking_preferences_id", "bookings_i_d" });

            migrationBuilder.AddForeignKey(
                name: "f_k_booking_booking_feature__booking_feature_booking_preferences_~",
                table: "booking_booking_feature",
                column: "booking_preferences_id",
                principalTable: "booking_feature",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_booking_booking_feature__bookings_bookings_i_d",
                table: "booking_booking_feature",
                column: "bookings_i_d",
                principalTable: "bookings",
                principalColumn: "i_d",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_booking_room_features__bookings_booking_id",
                table: "booking_room_features",
                column: "booking_id",
                principalTable: "bookings",
                principalColumn: "i_d",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_booking_room_features__room_features_room_feature_id",
                table: "booking_room_features",
                column: "room_feature_id",
                principalTable: "room_features",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_bookings__rooms_room_id",
                table: "bookings",
                column: "room_id",
                principalTable: "rooms",
                principalColumn: "i_d",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_bookings_bookings_booking_i_d",
                table: "bookings",
                column: "booking_i_d",
                principalTable: "bookings",
                principalColumn: "i_d");

            migrationBuilder.AddForeignKey(
                name: "f_k_role_claims_roles_role_id",
                table: "role_claims",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_room_room_features__room_features_room_feature_id",
                table: "room_room_features",
                column: "room_feature_id",
                principalTable: "room_features",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_room_room_features__rooms_room_id",
                table: "room_room_features",
                column: "room_id",
                principalTable: "rooms",
                principalColumn: "i_d",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_room_usages_rooms_room_i_d",
                table: "room_usages",
                column: "room_i_d",
                principalTable: "rooms",
                principalColumn: "i_d",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_sessions_users_user_id",
                table: "sessions",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_user_claims__users_user_id",
                table: "user_claims",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_user_logins__users_user_id",
                table: "user_logins",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_user_roles__users_user_id",
                table: "user_roles",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_user_roles_roles_role_id",
                table: "user_roles",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_user_tokens__users_user_id",
                table: "user_tokens",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_booking_booking_feature__booking_feature_booking_preferences_~",
                table: "booking_booking_feature");

            migrationBuilder.DropForeignKey(
                name: "f_k_booking_booking_feature__bookings_bookings_i_d",
                table: "booking_booking_feature");

            migrationBuilder.DropForeignKey(
                name: "f_k_booking_room_features__bookings_booking_id",
                table: "booking_room_features");

            migrationBuilder.DropForeignKey(
                name: "f_k_booking_room_features__room_features_room_feature_id",
                table: "booking_room_features");

            migrationBuilder.DropForeignKey(
                name: "f_k_bookings__rooms_room_id",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "f_k_bookings_bookings_booking_i_d",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "f_k_role_claims_roles_role_id",
                table: "role_claims");

            migrationBuilder.DropForeignKey(
                name: "f_k_room_room_features__room_features_room_feature_id",
                table: "room_room_features");

            migrationBuilder.DropForeignKey(
                name: "f_k_room_room_features__rooms_room_id",
                table: "room_room_features");

            migrationBuilder.DropForeignKey(
                name: "f_k_room_usages_rooms_room_i_d",
                table: "room_usages");

            migrationBuilder.DropForeignKey(
                name: "f_k_sessions_users_user_id",
                table: "sessions");

            migrationBuilder.DropForeignKey(
                name: "f_k_user_claims__users_user_id",
                table: "user_claims");

            migrationBuilder.DropForeignKey(
                name: "f_k_user_logins__users_user_id",
                table: "user_logins");

            migrationBuilder.DropForeignKey(
                name: "f_k_user_roles__users_user_id",
                table: "user_roles");

            migrationBuilder.DropForeignKey(
                name: "f_k_user_roles_roles_role_id",
                table: "user_roles");

            migrationBuilder.DropForeignKey(
                name: "f_k_user_tokens__users_user_id",
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
                name: "PK_room_usages",
                table: "room_usages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_room_features",
                table: "room_features");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role_claims",
                table: "role_claims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_booking_feature",
                table: "booking_feature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_booking_booking_feature",
                table: "booking_booking_feature");

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
                name: "room_usages",
                newName: "RoomUsages");

            migrationBuilder.RenameTable(
                name: "room_features",
                newName: "RoomFeatures");

            migrationBuilder.RenameTable(
                name: "role_claims",
                newName: "RoleClaims");

            migrationBuilder.RenameTable(
                name: "booking_feature",
                newName: "BookingFeature");

            migrationBuilder.RenameTable(
                name: "booking_booking_feature",
                newName: "BookingBookingFeature");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_name",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "two_factor_enabled",
                table: "Users",
                newName: "TwoFactorEnabled");

            migrationBuilder.RenameColumn(
                name: "security_stamp",
                table: "Users",
                newName: "SecurityStamp");

            migrationBuilder.RenameColumn(
                name: "phone_number_confirmed",
                table: "Users",
                newName: "PhoneNumberConfirmed");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "Users",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "normalized_user_name",
                table: "Users",
                newName: "NormalizedUserName");

            migrationBuilder.RenameColumn(
                name: "normalized_email",
                table: "Users",
                newName: "NormalizedEmail");

            migrationBuilder.RenameColumn(
                name: "lockout_end",
                table: "Users",
                newName: "LockoutEnd");

            migrationBuilder.RenameColumn(
                name: "lockout_enabled",
                table: "Users",
                newName: "LockoutEnabled");

            migrationBuilder.RenameColumn(
                name: "email_confirmed",
                table: "Users",
                newName: "EmailConfirmed");

            migrationBuilder.RenameColumn(
                name: "concurrency_stamp",
                table: "Users",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "access_failed_count",
                table: "Users",
                newName: "AccessFailedCount");

            migrationBuilder.RenameIndex(
                name: "user_name_index",
                table: "Users",
                newName: "UserNameIndex");

            migrationBuilder.RenameIndex(
                name: "email_index",
                table: "Users",
                newName: "EmailIndex");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Sessions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Sessions",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "last_refresh_token",
                table: "Sessions",
                newName: "LastRefreshToken");

            migrationBuilder.RenameColumn(
                name: "is_revoked",
                table: "Sessions",
                newName: "IsRevoked");

            migrationBuilder.RenameColumn(
                name: "initiated_at",
                table: "Sessions",
                newName: "InitiatedAt");

            migrationBuilder.RenameColumn(
                name: "expires_at",
                table: "Sessions",
                newName: "ExpiresAt");

            migrationBuilder.RenameIndex(
                name: "i_x_sessions_user_id",
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
                name: "price",
                table: "Rooms",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "room_type",
                table: "Rooms",
                newName: "RoomType");

            migrationBuilder.RenameColumn(
                name: "max_occupancy",
                table: "Rooms",
                newName: "MaxOccupancy");

            migrationBuilder.RenameColumn(
                name: "image_url",
                table: "Rooms",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "hotel_i_d",
                table: "Rooms",
                newName: "HotelID");

            migrationBuilder.RenameColumn(
                name: "floor_number",
                table: "Rooms",
                newName: "FloorNumber");

            migrationBuilder.RenameColumn(
                name: "i_d",
                table: "Rooms",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "i_x_room_room_features_room_id",
                table: "room_room_features",
                newName: "IX_room_room_features_room_id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Roles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "normalized_name",
                table: "Roles",
                newName: "NormalizedName");

            migrationBuilder.RenameColumn(
                name: "concurrency_stamp",
                table: "Roles",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameIndex(
                name: "role_name_index",
                table: "Roles",
                newName: "RoleNameIndex");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Hotels",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Hotels",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "image_url",
                table: "Hotels",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "i_d",
                table: "Hotels",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Bookings",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "user_i_d",
                table: "Bookings",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "total_price",
                table: "Bookings",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "total_guests",
                table: "Bookings",
                newName: "TotalGuests");

            migrationBuilder.RenameColumn(
                name: "room_id",
                table: "Bookings",
                newName: "RoomId");

            migrationBuilder.RenameColumn(
                name: "hotel_id",
                table: "Bookings",
                newName: "HotelId");

            migrationBuilder.RenameColumn(
                name: "check_out_date",
                table: "Bookings",
                newName: "CheckOutDate");

            migrationBuilder.RenameColumn(
                name: "check_in_date",
                table: "Bookings",
                newName: "CheckInDate");

            migrationBuilder.RenameColumn(
                name: "breakfast_requests",
                table: "Bookings",
                newName: "BreakfastRequests");

            migrationBuilder.RenameColumn(
                name: "booking_i_d",
                table: "Bookings",
                newName: "BookingID");

            migrationBuilder.RenameColumn(
                name: "i_d",
                table: "Bookings",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "i_x_bookings_room_id",
                table: "Bookings",
                newName: "IX_Bookings_RoomId");

            migrationBuilder.RenameIndex(
                name: "i_x_bookings_booking_i_d",
                table: "Bookings",
                newName: "IX_Bookings_BookingID");

            migrationBuilder.RenameIndex(
                name: "i_x_booking_room_features_room_feature_id",
                table: "booking_room_features",
                newName: "IX_booking_room_features_room_feature_id");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "UserTokens",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "UserTokens",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "login_provider",
                table: "UserTokens",
                newName: "LoginProvider");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "UserTokens",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "UserRoles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "UserRoles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "i_x_user_roles_role_id",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "UserLogins",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "provider_display_name",
                table: "UserLogins",
                newName: "ProviderDisplayName");

            migrationBuilder.RenameColumn(
                name: "provider_key",
                table: "UserLogins",
                newName: "ProviderKey");

            migrationBuilder.RenameColumn(
                name: "login_provider",
                table: "UserLogins",
                newName: "LoginProvider");

            migrationBuilder.RenameIndex(
                name: "i_x_user_logins_user_id",
                table: "UserLogins",
                newName: "IX_UserLogins_UserId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "UserClaims",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "UserClaims",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "claim_value",
                table: "UserClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "claim_type",
                table: "UserClaims",
                newName: "ClaimType");

            migrationBuilder.RenameIndex(
                name: "i_x_user_claims_user_id",
                table: "UserClaims",
                newName: "IX_UserClaims_UserId");

            migrationBuilder.RenameColumn(
                name: "times_booked_this_year",
                table: "RoomUsages",
                newName: "TimesBookedThisYear");

            migrationBuilder.RenameColumn(
                name: "times_booked_since_maintenance",
                table: "RoomUsages",
                newName: "TimesBookedSinceMaintenance");

            migrationBuilder.RenameColumn(
                name: "room_i_d",
                table: "RoomUsages",
                newName: "RoomID");

            migrationBuilder.RenameColumn(
                name: "general_wear",
                table: "RoomUsages",
                newName: "GeneralWear");

            migrationBuilder.RenameColumn(
                name: "cleaning_state",
                table: "RoomUsages",
                newName: "CleaningState");

            migrationBuilder.RenameColumn(
                name: "i_d",
                table: "RoomUsages",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "i_x_room_usages_room_i_d",
                table: "RoomUsages",
                newName: "IX_RoomUsages_RoomID");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "RoomFeatures",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "RoomFeatures",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "RoomFeatures",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "extra_cost",
                table: "RoomFeatures",
                newName: "ExtraCost");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "RoleClaims",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "RoleClaims",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "claim_value",
                table: "RoleClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "claim_type",
                table: "RoleClaims",
                newName: "ClaimType");

            migrationBuilder.RenameIndex(
                name: "i_x_role_claims_role_id",
                table: "RoleClaims",
                newName: "IX_RoleClaims_RoleId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "BookingFeature",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "BookingFeature",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "BookingFeature",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "extra_cost",
                table: "BookingFeature",
                newName: "ExtraCost");

            migrationBuilder.RenameColumn(
                name: "bookings_i_d",
                table: "BookingBookingFeature",
                newName: "BookingsID");

            migrationBuilder.RenameColumn(
                name: "booking_preferences_id",
                table: "BookingBookingFeature",
                newName: "BookingPreferencesId");

            migrationBuilder.RenameIndex(
                name: "i_x_booking_booking_feature_bookings_i_d",
                table: "BookingBookingFeature",
                newName: "IX_BookingBookingFeature_BookingsID");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingBookingFeature",
                table: "BookingBookingFeature",
                columns: new[] { "BookingPreferencesId", "BookingsID" });

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
    }
}
