namespace stay_link.Server.Models.Auth
{
    public class BookingRoles
    {
        public const string Admin = nameof(Admin);
        public const string BookingUser = nameof(BookingUser);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, BookingUser };
    }
}
