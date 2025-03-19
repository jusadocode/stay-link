using stay_link.Server.Models;

namespace stay_link.Server.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetBookings(string userId, bool isAdmin);
        Task<Booking?> GetBooking(int id, string userId, bool isAdmin);
        Task<Booking> CreateBooking(BookingDTO bookingDTO, string userId);
        Task<bool> UpdateBooking(int id, BookingDTO bookingDTO, string userId, bool isAdmin);
        Task<bool> DeleteBooking(int id);
    }
}