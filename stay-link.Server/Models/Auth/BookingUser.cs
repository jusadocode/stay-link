using Microsoft.AspNetCore.Identity;

namespace stay_link.Server.Models.Auth
{
    public class BookingUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
