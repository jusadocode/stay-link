using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stay_link.Server.DTO;
using stay_link.Server.Models.Auth;
using stay_link.Server.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace stay_link.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<BookingUser> _userManager;
        private readonly JwtTokenService _jwtTokenService;
        private readonly SessionService _sessionService;

        public AuthController(
            UserManager<BookingUser> userManager,
            JwtTokenService jwtTokenService,
            SessionService sessionService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _sessionService = sessionService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO userDTO)
        {
            var user = await _userManager.FindByNameAsync(userDTO.Username);
            if (user != null)
                return UnprocessableEntity(new { message = "Username is taken" });

            var newUser = new BookingUser
            {
                Email = userDTO.Email,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                UserName = userDTO.Username
            };

            var createUserResult = await _userManager.CreateAsync(newUser, userDTO.Password);
            if (!createUserResult.Succeeded)
                return UnprocessableEntity(new { message = "User creation failed" });

            await _userManager.AddToRoleAsync(newUser, BookingRoles.BookingUser);

            return Created("/api/auth/register", new { message = "User successfully created" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            var user = await _userManager.FindByNameAsync(userDTO.Username)
                       ?? await _userManager.FindByEmailAsync(userDTO.Username);

            if (user == null)
                return UnprocessableEntity(new { message = "User doesn't exist" });

            if (!await _userManager.CheckPasswordAsync(user, userDTO.Password))
                return UnprocessableEntity(new { message = "Password was incorrect" });

            var roles = await _userManager.GetRolesAsync(user);
            var sessionId = Guid.NewGuid();
            var expiresAt = DateTime.UtcNow.AddHours(72);
            var accessToken = _jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
            var refreshToken = _jwtTokenService.CreateRefreshToken(sessionId, user.Id);

            await _sessionService.CreateSessionAsync(sessionId, user.Id, refreshToken, expiresAt);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                Expires = expiresAt
            };

            Response.Cookies.Append("AccessToken", accessToken, cookieOptions);
            Response.Cookies.Append("RefreshToken", refreshToken, cookieOptions);

            return Ok(new SuccessfulLoginDTO(user.Id, roles));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAccessToken()
        {
            if (!Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
                return UnprocessableEntity(new { message = "Missing refresh token" });

            if (!_jwtTokenService.TryParseRefreshToken(refreshToken, out var claims))
                return UnprocessableEntity(new { message = "Invalid refresh token" });

            var sessionId = claims.FindFirstValue("SessionId");
            if (string.IsNullOrWhiteSpace(sessionId))
                return UnprocessableEntity(new { message = "Session ID missing" });

            var sessionGuid = Guid.Parse(sessionId);
            if (!await _sessionService.IsSessionValidAsync(sessionGuid, refreshToken))
                return UnprocessableEntity(new { message = "Session invalid" });

            var userId = claims.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return UnprocessableEntity(new { message = "User not found" });

            var roles = await _userManager.GetRolesAsync(user);
            var newAccessToken = _jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
            var newRefreshToken = _jwtTokenService.CreateRefreshToken(sessionGuid, user.Id);
            var expiresAt = DateTime.UtcNow.AddHours(72);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                Expires = expiresAt
            };

            Response.Cookies.Append("AccessToken", newAccessToken, cookieOptions);
            Response.Cookies.Append("RefreshToken", newRefreshToken, cookieOptions);
            await _sessionService.ExtendSessionAsync(sessionGuid, newRefreshToken, expiresAt);

            return Ok(new { message = "Access token refreshed" });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            if (!Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
                return UnprocessableEntity(new { message = "Missing refresh token" });

            if (!_jwtTokenService.TryParseRefreshToken(refreshToken, out var claims))
                return UnprocessableEntity(new { message = "Invalid refresh token" });

            var sessionId = claims.FindFirstValue("SessionId");
            if (string.IsNullOrWhiteSpace(sessionId))
                return UnprocessableEntity(new { message = "Missing session ID" });

            await _sessionService.InvalidateSessionAsync(Guid.Parse(sessionId));
            Response.Cookies.Delete("AccessToken");
            Response.Cookies.Delete("RefreshToken");

            return Ok(new { message = "Logged out successfully" });
        }
    }

    public record RegisterUserDTO(string Username, string FirstName, string LastName, string Email, string Password);
    public record LoginUserDTO(string Username, string Password);
    public record SuccessfulLoginDTO(string userId, IList<string> roles);
}
