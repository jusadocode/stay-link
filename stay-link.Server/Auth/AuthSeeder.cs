﻿
using Microsoft.AspNetCore.Identity;
using stay_link.Server.Auth.Model;

namespace stay_link.Server.Auth
{
    public class AuthSeeder
    {

        UserManager<BookingUser> _userManager;
        RoleManager<IdentityRole> _roleManager;

        public AuthSeeder(UserManager<BookingUser> userManager, RoleManager<IdentityRole> roleManager) {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task SeedAsync()
        {
            await AddDefaultRolesAsync();
            await AddAdminUserAsync();
        }

        private async Task AddAdminUserAsync()
        {
            var newAdminUser = new BookingUser()
            {
                UserName = "admin",
                Email = "admin@admin.com"
            };

            var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
            if(existingAdminUser == null)
            {
                var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "NotHardcodedAtAll!1");
                if (createAdminUserResult.Succeeded) {
                    await _userManager.AddToRolesAsync(newAdminUser, BookingRoles.All);
                }
            }
        }

        private async Task AddDefaultRolesAsync()
        {
            foreach(var role in BookingRoles.All)
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