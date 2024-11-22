using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stay_link.Server.Auth.Model;
using stay_link.Server.Data;
using stay_link.Server.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace stay_link.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly BookingContext _context;

        public BookingsController(BookingContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (User.IsInRole(BookingRoles.Admin))
            {
                var bookings = await _context.Bookings.ToListAsync();
                return Ok(bookings); 
            }

            var userBookings = await _context.Bookings
                                              .Where(b => b.UserID == userId) 
                                              .ToListAsync();

            if (!userBookings.Any())
            {
                return NotFound(new { message = "No bookings found for this user." });
            }

            return Ok(userBookings); 
        }


        // GET: api/Bookings/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null) {
                return NotFound(new { message = "Booking not found." });
            }

            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (User.IsInRole(BookingRoles.Admin))
            {
                return Ok(booking);
            }

            if (userId != booking.UserID)
            {
                return Forbid(); 
            }

            return Ok(booking); 
        }

        // POST: api/Bookings
        [HttpPost]
        [Authorize(Roles = BookingRoles.BookingUser)]
        public async Task<ActionResult<Booking>> PostBooking(BookingDTO bookingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request if model state is invalid
            }

            var room = await _context.Rooms.FindAsync(bookingDTO.RoomId);
            if (bookingDTO.RoomId != null && room == null)
            {
                return BadRequest(new { message = "Room not found." }); // Consistent bad request message for invalid room
            }

            if (bookingDTO.HotelId != null)
            {
                var hotel = await _context.Hotels.FindAsync(bookingDTO.HotelId);
                if (hotel == null)
                {
                    return BadRequest(new { message = "Hotel not found." }); // Consistent bad request message for invalid hotel
                }
            }

            if (room != null && room.HotelID != bookingDTO.HotelId)
            {
                return UnprocessableEntity(new { message = "Room does not belong to the specified hotel." }); // Consistent unprocessable entity message
            }

            var booking = new Booking
            {
                CheckInDate = DateOnly.Parse(bookingDTO.CheckInDate),
                CheckOutDate = DateOnly.Parse(bookingDTO.CheckOutDate),
                RoomId = bookingDTO.RoomId,
                HotelId = bookingDTO.HotelId,
                BreakfastRequests = bookingDTO.BreakfastRequests,
                UserID = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBooking), new { id = booking.ID }, booking); // 201 Created
        }

        // PUT: api/Bookings/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutBooking(int id, BookingDTO bookingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(); // 400 Bad Request if model state is invalid
            }
            
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound(new { message = "Booking not found." }); // Consistent not found message
            }

            if (!User.IsInRole(BookingRoles.Admin) &&
                User.FindFirstValue(JwtRegisteredClaimNames.Sub) != booking.UserID)
            {
                return Forbid();
            }

            var room = await _context.Rooms.FindAsync(bookingDTO.RoomId);
            if (bookingDTO.RoomId != null && room == null)
            {
                return BadRequest(new { message = "Room not found." }); // Consistent bad request message for invalid room
            }

            if (bookingDTO.HotelId != null)
            {
                var hotel = await _context.Hotels.FindAsync(bookingDTO.HotelId);
                if (hotel == null)
                {
                    return BadRequest(new { message = "Hotel not found." }); // Consistent bad request message for invalid hotel
                }
            }

            if (room != null && room.HotelID != bookingDTO.HotelId)
            {
                return UnprocessableEntity(new { message = "Room does not belong to the specified hotel." }); // Consistent unprocessable entity message
            }

            // Update booking properties from the DTO
            booking.CheckInDate = DateOnly.Parse(bookingDTO.CheckInDate);
            booking.CheckOutDate = DateOnly.Parse(bookingDTO.CheckOutDate);
            booking.RoomId = bookingDTO.RoomId;
            booking.HotelId = bookingDTO.HotelId;
            booking.BreakfastRequests = bookingDTO.BreakfastRequests;

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(); // Save changes
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound(new { message = "Booking not found." }); // Consistent not found message
                }
                else
                {
                    throw; // Unexpected error, rethrow
                }
            }

            return NoContent(); // 204 No Content on successful update
        }

        // DELETE: api/Bookings/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = BookingRoles.Admin)]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound(new { message = "Booking not found." }); // Consistent not found message
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content on successful deletion
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.ID == id);
        }
    }
}
