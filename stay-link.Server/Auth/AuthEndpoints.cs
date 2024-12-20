using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stay_link.Server.Auth.Model;
using stay_link.Server.Controllers;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Policy;

namespace stay_link.Server.Auth
{
    public static class AuthEndpoints
    {
        public static void AddAuthApi(this WebApplication app)
        {
            app.MapPost("api/accounts", async (UserManager<BookingUser> userManager, RegisterUserDTO userDTO) =>
            {
                var user = await userManager.FindByNameAsync(userDTO.Username);
                if (user != null)
                {
                    return Results.UnprocessableEntity(new { message = "Username is taken" });
                }

                var newUser = new BookingUser()
                {
                    Email = userDTO.Email,
                    UserName = userDTO.Username
                };

                var createUserResult = await userManager.CreateAsync(newUser, userDTO.Password);

                if (!createUserResult.Succeeded)
                    return Results.UnprocessableEntity(new { message = "Some error" });

                await userManager.AddToRoleAsync(newUser, BookingRoles.BookingUser);

                return Results.Created("/api/accounts", new { message = "User successfully created" });

            })
            .Produces<ErrorResponse>(StatusCodes.Status201Created)  // 201 Created
            .Produces<ErrorResponse>(StatusCodes.Status422UnprocessableEntity); // 422 UnprocessableEntity


            app.MapPost("api/login", async (UserManager<BookingUser> userManager, JwtTokenService jwtTokenService, SessionService sessionService, HttpContext httpContext, LoginUserDTO userDTO) =>
            {
                var user = await userManager.FindByNameAsync(userDTO.Username);

                if (user == null)
                {
                    user = await userManager.FindByEmailAsync(userDTO.Username);
                    if (user == null)
                        return Results.UnprocessableEntity(new { message = "User doesn't exist" });

                }

                var isPasswordValid = await userManager.CheckPasswordAsync(user, userDTO.Password);

                if (!isPasswordValid)
                    return Results.UnprocessableEntity(new { message = "Password was incorrect" });

                var roles = await userManager.GetRolesAsync(user);

                var sessionId = Guid.NewGuid();
                var expiresAt = DateTime.UtcNow.AddMinutes(60);
                var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
                var refreshToken = jwtTokenService.CreateRefreshToken(sessionId, user.Id, expiresAt);

                await sessionService.CreateSessionAsync(sessionId, user.Id, refreshToken, expiresAt);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Expires = expiresAt,
                    Secure = true,
                };

                httpContext.Response.Cookies.Append("AccessToken", accessToken, cookieOptions);
                httpContext.Response.Cookies.Append("RefreshToken", refreshToken, cookieOptions);

                return Results.Ok(new SuccessfulLoginDTO(user.Id, roles));

            })
            .Produces<SuccessfulLoginDTO>(StatusCodes.Status200OK)  // 201 Created
            .Produces<ErrorResponse>(StatusCodes.Status422UnprocessableEntity); // 422 UnprocessableEntity;

            app.MapPost("api/accessToken", async (UserManager<BookingUser> userManager, JwtTokenService jwtTokenService, SessionService sessionService, HttpContext httpContext) =>
            {
                if (!httpContext.Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
                {
                    return Results.UnprocessableEntity(new { message = "Refresh token not found in cookies" });
                }

                if (!jwtTokenService.TryParseRefreshToken(refreshToken, out var claims))
                {
                    return Results.UnprocessableEntity(new { message = "Failed to parse refresh token" });
                }

                var sessionId = claims.FindFirstValue("SessionId");
                if (string.IsNullOrWhiteSpace(sessionId))
                {
                    return Results.UnprocessableEntity(new { message = "Session not found" });
                }

                var sessionIdAsGuid = Guid.Parse(sessionId);
                if (!await sessionService.IsSessionValidAsync(sessionIdAsGuid, refreshToken))
                {
                    return Results.UnprocessableEntity(new { message = "Session invalid" });
                }

                var userId = claims.FindFirstValue(JwtRegisteredClaimNames.Sub);

                var user = await userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return Results.UnprocessableEntity(new { message = "User not found" });
                }

                var roles = await userManager.GetRolesAsync(user);

                var expiresAt = DateTime.UtcNow.AddDays(1);
                var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
                var newRefreshToken = jwtTokenService.CreateRefreshToken(sessionIdAsGuid, user.Id, expiresAt);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Expires = expiresAt,
                    Secure = true
                };

                httpContext.Response.Cookies.Append("AccessToken", accessToken, cookieOptions);
                httpContext.Response.Cookies.Append("RefreshToken", newRefreshToken, cookieOptions);

                await sessionService.ExtendSessionAsync(sessionIdAsGuid, newRefreshToken, expiresAt);

                return Results.Ok(new { message = "Access token refreshed" });

            })
            .Produces<ErrorResponse>(StatusCodes.Status200OK)  // 201 Created
            .Produces<ErrorResponse>(StatusCodes.Status422UnprocessableEntity); // 422 UnprocessableEntity;;

            app.MapPost("api/logout", async (UserManager<BookingUser> userManager, JwtTokenService jwtTokenService, SessionService sessionService, HttpContext httpContext) =>
            {
                if (!httpContext.Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
                {
                    return Results.UnprocessableEntity(new { message = "Did not find refresh token in cookies" });
                }

                if (!httpContext.Request.Cookies.TryGetValue("AccessToken", out var accessToken))
                {
                    return Results.UnprocessableEntity(new { message = "Did not find access token in cookies" });
                }

                if (!jwtTokenService.TryParseRefreshToken(refreshToken, out var claims))
                {
                    return Results.UnprocessableEntity(new { message = "Error parsing refresh token" });
                }

                var sessionId = claims.FindFirstValue("SessionId");
                if (string.IsNullOrWhiteSpace(sessionId))
                {
                    return Results.UnprocessableEntity(new { message = "Did not find sessionId in token" });
                }

                await sessionService.InvalidateSessionAsync(Guid.Parse(sessionId));
                httpContext.Response.Cookies.Delete("AccessToken");
                httpContext.Response.Cookies.Delete("RefreshToken");

                return Results.Ok(new { message = "Logged out successfully" });

            })
            .Produces<ErrorResponse>(StatusCodes.Status200OK)  // 201 Created
            .Produces<ErrorResponse>(StatusCodes.Status422UnprocessableEntity); // 422 UnprocessableEntity;
        }

        public record RegisterUserDTO(string Username, string Email, string Password);
        public record LoginUserDTO(string Username, string Password);
        public record SuccessfulLoginDTO(string userId, IList<string> roles);
    }
}
