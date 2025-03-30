using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stay_link.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbTableRenamesUnderscores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roleclaims_roles_roleid",
                table: "roleclaims");

            migrationBuilder.DropForeignKey(
                name: "FK_userclaims_users_userid",
                table: "userclaims");

            migrationBuilder.DropForeignKey(
                name: "FK_userlogins_users_userid",
                table: "userlogins");

            migrationBuilder.DropForeignKey(
                name: "FK_usertokens_users_userid",
                table: "usertokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usertokens",
                table: "usertokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userlogins",
                table: "userlogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userclaims",
                table: "userclaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roleclaims",
                table: "roleclaims");

            migrationBuilder.RenameTable(
                name: "usertokens",
                newName: "user_tokens");

            migrationBuilder.RenameTable(
                name: "userlogins",
                newName: "user_logins");

            migrationBuilder.RenameTable(
                name: "userclaims",
                newName: "user_claims");

            migrationBuilder.RenameTable(
                name: "roleclaims",
                newName: "role_claims");

            migrationBuilder.RenameIndex(
                name: "IX_userlogins_userid",
                table: "user_logins",
                newName: "IX_user_logins_userid");

            migrationBuilder.RenameIndex(
                name: "IX_userclaims_userid",
                table: "user_claims",
                newName: "IX_user_claims_userid");

            migrationBuilder.RenameIndex(
                name: "IX_roleclaims_roleid",
                table: "role_claims",
                newName: "IX_role_claims_roleid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_tokens",
                table: "user_tokens",
                columns: new[] { "userid", "loginprovider", "name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_logins",
                table: "user_logins",
                columns: new[] { "loginprovider", "providerkey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_claims",
                table: "user_claims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_claims",
                table: "role_claims",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_role_claims_roles_roleid",
                table: "role_claims",
                column: "roleid",
                principalTable: "roles",
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
                name: "FK_user_tokens_users_userid",
                table: "user_tokens",
                column: "userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_claims_roles_roleid",
                table: "role_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_user_claims_users_userid",
                table: "user_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_user_logins_users_userid",
                table: "user_logins");

            migrationBuilder.DropForeignKey(
                name: "FK_user_tokens_users_userid",
                table: "user_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_tokens",
                table: "user_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_logins",
                table: "user_logins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_claims",
                table: "user_claims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role_claims",
                table: "role_claims");

            migrationBuilder.RenameTable(
                name: "user_tokens",
                newName: "usertokens");

            migrationBuilder.RenameTable(
                name: "user_logins",
                newName: "userlogins");

            migrationBuilder.RenameTable(
                name: "user_claims",
                newName: "userclaims");

            migrationBuilder.RenameTable(
                name: "role_claims",
                newName: "roleclaims");

            migrationBuilder.RenameIndex(
                name: "IX_user_logins_userid",
                table: "userlogins",
                newName: "IX_userlogins_userid");

            migrationBuilder.RenameIndex(
                name: "IX_user_claims_userid",
                table: "userclaims",
                newName: "IX_userclaims_userid");

            migrationBuilder.RenameIndex(
                name: "IX_role_claims_roleid",
                table: "roleclaims",
                newName: "IX_roleclaims_roleid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usertokens",
                table: "usertokens",
                columns: new[] { "userid", "loginprovider", "name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_userlogins",
                table: "userlogins",
                columns: new[] { "loginprovider", "providerkey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_userclaims",
                table: "userclaims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roleclaims",
                table: "roleclaims",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_roleclaims_roles_roleid",
                table: "roleclaims",
                column: "roleid",
                principalTable: "roles",
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
    }
}
