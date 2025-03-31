using AutoMapper;
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
                : await _context.Bookings.Where(b => b.UserID == userId).ToListAsync();

            return _mapper.Map<IEnumerable<BookingDTO>>(bookings);

        }

        public async Task<Booking?> GetBooking(int id, string userId, bool isAdmin)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return null;

            if (!isAdmin && booking.UserID != userId)
                return null; // User is not allowed to see this booking

            return booking;
        }

        public async Task<BookingDTO> CreateBooking(CreateBookingDTO bookingDTO, string userId)
        {
            var rooms = await _context.Rooms
                  .Where(r => bookingDTO.RoomIds.Contains(r.ID))
                  .ToListAsync();

            //if (bookingDTO.HotelId != null)
            //{
            //    var hotel = await _context.Hotels.FindAsync(bookingDTO.HotelId);
            //    if (hotel == null)
            //        throw new Exception("Hotel not found.");
            //}

            if (rooms.Count != bookingDTO.RoomIds.Count)
                throw new Exception("One or more selected rooms were not found.");


            var booking = new Booking
            {
                CheckInDate = DateOnly.Parse(bookingDTO.CheckInDate),
                CheckOutDate = DateOnly.Parse(bookingDTO.CheckOutDate),
                Rooms = rooms,
                //HotelId = bookingDTO.HotelId,
                BreakfastRequests = bookingDTO.BreakfastRequests,
                UserID = userId
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookingDTO>(booking);
        }

        public async Task<bool> UpdateBooking(int id, BookingDTO bookingDTO, string userId, bool isAdmin)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null) return false;

            if (!isAdmin && booking.UserID != userId)
                return false; // Unauthorized

            var rooms = await _context.Rooms
                 .Where(r => bookingDTO.RoomIds.Contains(r.ID))
                 .ToListAsync();

            if (rooms.Count != bookingDTO.RoomIds.Count)
                throw new Exception("One or more selected rooms were not found.");

            if (bookingDTO.HotelId != null)
            {
                var hotel = await _context.Hotels.FindAsync(bookingDTO.HotelId);
                if (hotel == null)
                    throw new Exception("Hotel not found.");
            }

            booking.CheckInDate = DateOnly.Parse(bookingDTO.CheckInDate);
            booking.CheckOutDate = DateOnly.Parse(bookingDTO.CheckOutDate);
            booking.Rooms = rooms;
            //booking.HotelId = bookingDTO.HotelId;
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
