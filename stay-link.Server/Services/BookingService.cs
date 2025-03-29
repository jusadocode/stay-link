using Microsoft.EntityFrameworkCore;
using stay_link.Server.Data;
using stay_link.Server.DTO;
using stay_link.Server.Models;

namespace stay_link.Server.Services
{
    public class BookingService 
    {
        private readonly BookingContext _context;

        public BookingService(BookingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetBookings(string userId, bool isAdmin)
        {
            return isAdmin
                ? await _context.Bookings.ToListAsync()
                : await _context.Bookings.Where(b => b.UserID == userId).ToListAsync();
        }

        public async Task<Booking?> GetBooking(int id, string userId, bool isAdmin)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return null;

            if (!isAdmin && booking.UserID != userId)
                return null; // User is not allowed to see this booking

            return booking;
        }

        public async Task<Booking> CreateBooking(BookingDTO bookingDTO, string userId)
        {
            var room = await _context.Rooms.FindAsync(bookingDTO.RoomId);
            if (bookingDTO.RoomId != null && room == null)
                throw new Exception("Room not found.");

            if (bookingDTO.HotelId != null)
            {
                var hotel = await _context.Hotels.FindAsync(bookingDTO.HotelId);
                if (hotel == null)
                    throw new Exception("Hotel not found.");
            }

            if (room != null && room.HotelID != bookingDTO.HotelId)
                throw new Exception("Room does not belong to the specified hotel.");

            var booking = new Booking
            {
                CheckInDate = DateOnly.Parse(bookingDTO.CheckInDate),
                CheckOutDate = DateOnly.Parse(bookingDTO.CheckOutDate),
                RoomId = bookingDTO.RoomId,
                HotelId = bookingDTO.HotelId,
                BreakfastRequests = bookingDTO.BreakfastRequests,
                UserID = userId
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> UpdateBooking(int id, BookingDTO bookingDTO, string userId, bool isAdmin)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return false;

            if (!isAdmin && booking.UserID != userId)
                return false; // Unauthorized

            var room = await _context.Rooms.FindAsync(bookingDTO.RoomId);
            if (bookingDTO.RoomId != null && room == null)
                throw new Exception("Room not found.");

            if (bookingDTO.HotelId != null)
            {
                var hotel = await _context.Hotels.FindAsync(bookingDTO.HotelId);
                if (hotel == null)
                    throw new Exception("Hotel not found.");
            }

            if (room != null && room.HotelID != bookingDTO.HotelId)
                throw new Exception("Room does not belong to the specified hotel.");

            booking.CheckInDate = DateOnly.Parse(bookingDTO.CheckInDate);
            booking.CheckOutDate = DateOnly.Parse(bookingDTO.CheckOutDate);
            booking.RoomId = bookingDTO.RoomId;
            booking.HotelId = bookingDTO.HotelId;
            booking.BreakfastRequests = bookingDTO.BreakfastRequests;

            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
