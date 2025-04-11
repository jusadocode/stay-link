using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using stay_link.Server.Data;
using stay_link.Server.DTO;
using stay_link.Server.Models;

namespace stay_link.Server.Services
{
    public class BookingService 
    {
        private readonly BookingContext _context;
        private readonly IMapper _mapper;

        public BookingService(BookingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingDTO>> GetBookings(string userId, bool isAdmin)
        {
            var bookings = isAdmin
                ? await _context.Bookings.ToListAsync()
                : await _context.Bookings.Where(b => b.UserId == userId).ToListAsync();

            return _mapper.Map<IEnumerable<BookingDTO>>(bookings);

        }

        public async Task<BookingDTO?> GetBooking(int id, string userId, bool isAdmin)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return null;

            if (!isAdmin && booking.UserId != userId)
                return null; // User is not allowed to see this booking

            return _mapper.Map<BookingDTO>(booking);
        }

        public async Task<BookingDTO> CreateBooking(CreateBookingDTO bookingDTO, string userId, bool isAdmin)
        {
            var rooms = await _context.Rooms
                .Where(r => bookingDTO.RoomIds.Contains(r.Id))
                .ToListAsync();

            if (rooms.Count != bookingDTO.RoomIds.Count)
                throw new Exception("One or more selected rooms were not found.");

            string displayName;

            if (isAdmin)
            {
                if (string.IsNullOrWhiteSpace(bookingDTO.DisplayName))
                    throw new Exception("Display name must be provided for admin-created bookings.");

                displayName = bookingDTO.DisplayName;
            }
            else
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                    throw new Exception("User not found.");

                displayName = $"{user.FirstName} {user.LastName}";
            }

            var booking = new Booking
            {
                CheckInDate = DateOnly.Parse(bookingDTO.CheckInDate),
                CheckOutDate = DateOnly.Parse(bookingDTO.CheckOutDate),
                Rooms = rooms,
                DisplayName = displayName,
                BreakfastRequests = bookingDTO.BreakfastRequests,
                UserId = userId,
                CreationTime = DateTime.UtcNow
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookingDTO>(booking);
        }

        public async Task<bool> UpdateBooking(int id, BookingDTO bookingDTO, string userId, bool isAdmin)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null) return false;

            if (!isAdmin && booking.User.Id != userId)
                return false; // Unauthorized

            var rooms = await _context.Rooms
                 .Where(r => bookingDTO.RoomIds.Contains(r.Id))
                 .ToListAsync();

            if (rooms.Count != bookingDTO.RoomIds.Count)
                throw new Exception("One or more selected rooms were not found.");

            //if (bookingDTO.HotelId != null)
            //{
            //    var hotel = await _context.Hotels.FindAsync(bookingDTO.HotelId);
            //    if (hotel == null)
            //        throw new Exception("Hotel not found.");
            //}

            booking.CheckInDate = DateOnly.Parse(bookingDTO.CheckInDate);
            booking.CheckOutDate = DateOnly.Parse(bookingDTO.CheckOutDate);
            booking.DisplayName = bookingDTO.DisplayName;
            booking.Rooms.Clear();
            booking.Rooms = rooms;
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
