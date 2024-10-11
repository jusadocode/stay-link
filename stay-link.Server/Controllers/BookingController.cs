using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stay_link.Server.Data;
using stay_link.Server.Models;

namespace stay_link.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly BookingContext _context;

        public BookingController(BookingContext context)
        {
            _context = context;
        }

        // GET: api/booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return await _context.Booking
                .ToListAsync();

        }

        // GET: api/booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Booking.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // POST: api/booking
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(BookingDTO bookingDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            
            if (bookingDTO.RoomId != null)
            {
                var room = await _context.Room.FindAsync(bookingDTO.RoomId);
                if (room == null)
                     return BadRequest("Invalid Room ID.");
            }

            if (bookingDTO.HotelId != null)
            {
                var hotel = await _context.Hotel.FindAsync(bookingDTO.HotelId);
                if (hotel == null)
                    return BadRequest("Invalid Hotel ID.");

            }

            // Ensure the Room and Hotel objects are properly attached or created
            var booking = new Booking
            {
                CheckInDate = DateOnly.Parse(bookingDTO.CheckInDate),
                CheckOutDate = DateOnly.Parse(bookingDTO.CheckOutDate),
                RoomId = bookingDTO.RoomId,
                HotelId = bookingDTO.HotelId,
                BreakfastRequests = bookingDTO.BreakfastRequests
            };

            _context.Booking.Add(booking);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.ID }, booking);
        }

        // PUT: api/booking/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, BookingDTO bookingDTO)
        {
            // Retrieve the existing booking from the database
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            // Update booking properties from the DTO
            booking.CheckInDate = DateOnly.Parse(bookingDTO.CheckInDate);
            booking.CheckOutDate = DateOnly.Parse(bookingDTO.CheckOutDate);
            booking.RoomId = bookingDTO.RoomId;
            booking.HotelId = bookingDTO.HotelId;
            booking.BreakfastRequests = bookingDTO.BreakfastRequests;

            // Mark the booking entity as modified
            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/booking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.ID == id);
        }
    }
}
