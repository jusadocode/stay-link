using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stay_link.Server.Auth.Model;
using stay_link.Server.Data;
using stay_link.Server.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        /// <summary>
        /// Gets a list of all bookings or the authenticated user's bookings.
        /// </summary>
        /// <returns>A list of bookings.</returns>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Booking>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (User.IsInRole(BookingRoles.Admin))
            {
                var bookings = await _context.Bookings.ToListAsync();
                return Ok(bookings); // Admins get all bookings
            }

            var userBookings = await _context.Bookings
                                              .Where(b => b.UserID == userId)
                                              .ToListAsync();

            return Ok(userBookings); // Regular users get their own bookings
        }

        /// <summary>
        /// Gets a specific booking by ID.
        /// </summary>
        /// <param name="id">The ID of the booking.</param>
        /// <returns>The booking details.</returns>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Booking))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound(new { message = "Booking not found." });
            }

            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!User.IsInRole(BookingRoles.Admin) && userId != booking.UserID)
            {
                return Forbid(); // Non-admin users can only access their own bookings
            }

            return Ok(booking);
        }

        /// <summary>
        /// Creates a new booking.
        /// </summary>
        /// <param name="bookingDTO">The booking details.</param>
        /// <returns>The created booking.</returns>
        [HttpPost]
        [Authorize(Roles = BookingRoles.BookingUser)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Booking))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<Booking>> PostBooking(BookingDTO bookingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var room = await _context.Rooms.FindAsync(bookingDTO.RoomId);
            if (bookingDTO.RoomId != null && room == null)
            {
                return BadRequest(new { message = "Room not found." });
            }

            if (bookingDTO.HotelId != null)
            {
                var hotel = await _context.Hotels.FindAsync(bookingDTO.HotelId);
                if (hotel == null)
                {
                    return BadRequest(new { message = "Hotel not found." });
                }
            }

            if (room != null && room.HotelID != bookingDTO.HotelId)
            {
                return UnprocessableEntity(new { message = "Room does not belong to the specified hotel." });
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

            return CreatedAtAction(nameof(GetBooking), new { id = booking.ID }, booking);
        }

        /// <summary>
        /// Updates an existing booking by ID.
        /// </summary>
        /// <param name="id">The ID of the booking.</param>
        /// <param name="bookingDTO">The updated booking details.</param>
        /// <returns>204 No Content on success.</returns>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> PutBooking(int id, BookingDTO bookingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound(new { message = "Booking not found." });
            }

            if (!User.IsInRole(BookingRoles.Admin) &&
                User.FindFirstValue(JwtRegisteredClaimNames.Sub) != booking.UserID)
            {
                return Forbid();
            }

            var room = await _context.Rooms.FindAsync(bookingDTO.RoomId);
            if (bookingDTO.RoomId != null && room == null)
            {
                return BadRequest(new { message = "Room not found." });
            }

            if (bookingDTO.HotelId != null)
            {
                var hotel = await _context.Hotels.FindAsync(bookingDTO.HotelId);
                if (hotel == null)
                {
                    return BadRequest(new { message = "Hotel not found." });
                }
            }

            if (room != null && room.HotelID != bookingDTO.HotelId)
            {
                return UnprocessableEntity(new { message = "Room does not belong to the specified hotel." });
            }

            // Update booking properties
            booking.CheckInDate = DateOnly.Parse(bookingDTO.CheckInDate);
            booking.CheckOutDate = DateOnly.Parse(bookingDTO.CheckOutDate);
            booking.RoomId = bookingDTO.RoomId;
            booking.HotelId = bookingDTO.HotelId;
            booking.BreakfastRequests = bookingDTO.BreakfastRequests;

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound(new { message = "Booking not found." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a booking by ID.
        /// </summary>
        /// <param name="id">The ID of the booking.</param>
        /// <returns>204 No Content on success.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = BookingRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound(new { message = "Booking not found." });
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.ID == id);
        }
    }
}
