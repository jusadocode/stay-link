using Microsoft.AspNetCore.Identity;
using stay_link.Server.Auth.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace stay_link.Server.Auth
{
    public static class AuthEndpoints
    {
        public static void AddAuthApi(this WebApplication app)
        {
            app.MapPost("api/accounts", async (UserManager<BookingUser> userManager, RegisterUserDTO userDTO) =>
            {
                var user = await userManager.FindByNameAsync(userDTO.Username);
                if (user != null) {
                    return Results.UnprocessableEntity("Username is taken");
                }

                var newUser = new BookingUser()
                {
                    Email = userDTO.Email,
                    UserName = userDTO.Username
                };

                var createUserResult = await userManager.CreateAsync(newUser, userDTO.Password);

                if (!createUserResult.Succeeded)
                    return Results.UnprocessableEntity("Some error");

                await userManager.AddToRoleAsync(newUser, BookingRoles.BookingUser);

                return Results.Created();

            });


            app.MapPost("api/login", async (UserManager<BookingUser> userManager, JwtTokenService jwtTokenService, SessionService sessionService, HttpContext httpContext, RegisterUserDTO userDTO) =>
            {
                var user = await userManager.FindByNameAsync(userDTO.Username);

                if (user == null)
                {
                    return Results.UnprocessableEntity("User doesn't exist");
                }

                var isPasswordValid = await userManager.CheckPasswordAsync(user, userDTO.Password);

                if(!isPasswordValid) 
                    return Results.UnprocessableEntity("Password was incorrect");

                var roles = await userManager.GetRolesAsync(user);

                var sessionId = Guid.NewGuid();
                var expiresAt = DateTime.UtcNow.AddMinutes(15);
                var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
                var refreshToken = jwtTokenService.CreateRefreshToken(sessionId, user.Id, expiresAt);

                await sessionService.CreateSessionAsync(sessionId, user.Id, refreshToken, expiresAt);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = expiresAt
                };

                httpContext.Response.Cookies.Append("RefreshToken", refreshToken, cookieOptions);

                return Results.Ok(new SuccessfulLoginDTO(accessToken));

            });

            app.MapPost("api/accessToken", async (UserManager<BookingUser> userManager, JwtTokenService jwtTokenService, SessionService sessionService, HttpContext httpContext) =>
            {
                if(!httpContext.Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
                {
                    return Results.UnprocessableEntity();
                }

                if (!jwtTokenService.TryParseRefreshToken(refreshToken, out var claims))
                {
                    return Results.UnprocessableEntity();
                }

                var sessionId = claims.FindFirstValue("SessionId");
                if(string.IsNullOrWhiteSpace(sessionId))
                {
                    return Results.UnprocessableEntity();
                }

                var sessionIdAsGuid = Guid.Parse(sessionId);
                if(!await sessionService.IsSessionValidAsync(sessionIdAsGuid, refreshToken))
                {
                    return Results.UnprocessableEntity();
                }

                var userId = claims.FindFirstValue(JwtRegisteredClaimNames.Sub);

                var user = await userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return Results.UnprocessableEntity();
                }

                var roles = await userManager.GetRolesAsync(user);

                var expiresAt = DateTime.UtcNow.AddDays(3);
                var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
                var newRefreshToken = jwtTokenService.CreateRefreshToken(sessionIdAsGuid, user.Id, expiresAt);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = expiresAt
                };

                httpContext.Response.Cookies.Append("RefreshToken", newRefreshToken, cookieOptions);

                await sessionService.ExtendSessionAsync(sessionIdAsGuid, newRefreshToken, expiresAt);

                return Results.Ok(new SuccessfulLoginDTO(accessToken));

            });

            app.MapPost("api/logout", async (UserManager<BookingUser> userManager, JwtTokenService jwtTokenService, SessionService sessionService, HttpContext httpContext) =>
            {
                if (!httpContext.Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
                {
                    return Results.UnprocessableEntity();
                }

                if (!jwtTokenService.TryParseRefreshToken(refreshToken, out var claims))
                {
                    return Results.UnprocessableEntity();
                }

                var sessionId = claims.FindFirstValue("SessionId");
                if (string.IsNullOrWhiteSpace(sessionId))
                {
                    return Results.UnprocessableEntity();
                }

                await sessionService.InvalidateSessionAsync(Guid.Parse(sessionId));
                httpContext.Response.Cookies.Delete(refreshToken);

                return Results.Ok();

            });
        }

        public record RegisterUserDTO(string Username, string Email, string Password);
        public record LoginUserDTO(string userName, string password);
        public record SuccessfulLoginDTO(string accessToken);


    }
}
