using System.ComponentModel.DataAnnotations;

namespace stay_link.Server.Models
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
        public virtual BookingUser User { get; set; }

    }
}
