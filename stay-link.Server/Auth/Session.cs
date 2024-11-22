using stay_link.Server.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace stay_link.Server.Auth
{
    public class Session
    {
        public Guid Id { get; set; }
        public string LastRefreshToken { get; set; }
        public DateTimeOffset InitiatedAt { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
        [Required]
        public string UserId { get; set; }
        public BookingUser User { get; set; }

    }
}
