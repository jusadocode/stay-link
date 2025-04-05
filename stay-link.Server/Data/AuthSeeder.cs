
using Microsoft.AspNetCore.Identity;
using stay_link.Server.Models;

namespace stay_link.Server.Data
{
    public class AuthSeeder
    {

        UserManager<BookingUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        IConfiguration _configuration;

        public AuthSeeder(UserManager<BookingUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration = null)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task SeedAsync()
        {
            await AddDefaultRolesAsync();
            await AddBookingUsersAsync();
            await AddAdminUserAsync();
        }

        private async Task AddAdminUserAsync()
        {
            var newAdminUser = new BookingUser()
            {
                UserName = "admin",
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@admin.com"
            };

            var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
            if (existingAdminUser == null)
            {
                var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, Environment.GetEnvironmentVariable("ADMIN_PASSWORD"));
                if (createAdminUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newAdminUser, BookingRoles.All);
                }
            }
        }

        private async Task AddBookingUsersAsync()
        {
            var newBookingUser1 = new BookingUser()
            {
                UserName = "hughe3",
                FirstName = "Hughes",
                LastName = "Carlston",
                Email = "hughes@gmail.com"
            };

            var newBookingUser2 = new BookingUser()
            {
                UserName = "johnee3",
                FirstName = "John",
                LastName = "Marston",
                Email = "johnm@gmail.com"
            };

            var existingBookingUser = await _userManager.FindByNameAsync(newBookingUser1.UserName);
            if (existingBookingUser == null)
            {
                var createBookingUserResult = await _userManager.CreateAsync(newBookingUser1, Environment.GetEnvironmentVariable("USER_PASSWORD"));
                if (createBookingUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newBookingUser1, [BookingRoles.BookingUser]);
                }
            }

            existingBookingUser = await _userManager.FindByNameAsync(newBookingUser2.UserName);
            if (existingBookingUser == null)
            {
                var createBookingUserResult = await _userManager.CreateAsync(newBookingUser2, Environment.GetEnvironmentVariable("USER_PASSWORD"));
                if (createBookingUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newBookingUser2, [BookingRoles.BookingUser]);
                }
            }
        }

        private async Task AddDefaultRolesAsync()
        {
            foreach (var role in BookingRoles.All)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
