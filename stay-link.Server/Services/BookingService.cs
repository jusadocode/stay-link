using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using stay_link.Server.Data;
using stay_link.Server.DTO;
using stay_link.Server.DTO.Bookings;
using stay_link.Server.Models;
using stay_link.Server.Models.Bookings;
using stay_link.Server.Models.Enums;
using stay_link.Server.Models.RoomOperations;
using stay_link.Server.Models.Rooms;

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
                return null; 

            return _mapper.Map<BookingDTO>(booking);
        }

        public async Task<BookingDTO> CreateBooking(CreateBookingDTO bookingDTO, string userId, bool isAdmin)
        {
            var rooms = await _context.Rooms
                .Where(r => bookingDTO.RoomIds.Contains(r.Id))
                .ToListAsync();

            var closures = await _context.RoomClosure
                .Where(r => bookingDTO.RoomIds.Contains(r.Id))
                .ToListAsync();
            



            if (rooms.Count != bookingDTO.RoomIds.Count)
                throw new Exception("One or more selected rooms were not found.");

            string displayName;

            if (isAdmin)
            {
                if (string.IsNullOrWhiteSpace(bookingDTO.DisplayName))
                    throw new Exception("Display name must be provided for admin-created bookings.");

                var bookingWithSameName = _context.Bookings.FirstOrDefault(b => b.DisplayName == bookingDTO.DisplayName);


                if(bookingWithSameName != null)
                {
                    throw new Exception("Booking with specified name exists");
                }

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

            var closureDate = booking.CheckOutDate.AddDays(1);

            foreach (var room in rooms)
            {
                var closure = new RoomClosure
                {
                    RoomId = room.Id,
                    StartDate = closureDate,
                    EndDate = closureDate,
                    Reason = "Post-booking cleanup"
                };
                _context.RoomClosure.Add(closure);
            }

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
            var booking = await _context.Bookings
                .Include(b => b.Rooms) 
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null) return false;

            var closureDate = booking.CheckOutDate.AddDays(1);

            var closuresToRemove = await _context.RoomClosure
                .Where(rc =>
                    rc.StartDate == closureDate &&
                    rc.Reason == "Post-booking cleanup" &&
                    booking.Rooms.Select(r => r.Id).Contains(rc.RoomId)
                )
                .ToListAsync();

            _context.RoomClosure.RemoveRange(closuresToRemove);
            _context.Bookings.Remove(booking);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelBooking(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);

            if (booking == null || booking.Status == BookingStatus.Cancelled)
                return false;

            // Only allow cancellation if check-in is more than 24h away
            if (booking.CheckInDate.ToDateTime(TimeOnly.MinValue) <= DateTime.UtcNow.AddHours(24))
                return false;

            booking.Status = BookingStatus.Cancelled;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckInBooking(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);

            if (booking == null || booking.Status != BookingStatus.Confirmed)
                return false;

            // Optional: Only allow check-in on the day of or within a certain time window
            if (booking.CheckInDate.ToDateTime(TimeOnly.MinValue) > DateTime.UtcNow)
                return false;

            booking.Status = BookingStatus.CheckedIn;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckOutBooking(int bookingId)
        {
            var booking = await _context.Bookings
                .Include(b => b.Rooms)
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking == null || booking.Status != BookingStatus.CheckedIn)
                return false;

            if (booking.CheckOutDate.ToDateTime(TimeOnly.MinValue) > DateTime.UtcNow)
                return false;

            foreach (var room in booking.Rooms)
            {
                room.RoomUsage.GeneralWear += booking.TotalGuests * 3;
            }

            booking.Status = BookingStatus.CheckedOut;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
