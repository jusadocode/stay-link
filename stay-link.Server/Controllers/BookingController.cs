using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            // Ensure the Room and Hotel objects are properly attached or created
            if (booking.RoomId != null)
            {
                var room = await _context.Room.FindAsync(booking.RoomId);
                if (room != null)
                {
                    booking.RoomId = room.ID;
                }
                else
                {
                    return BadRequest("Invalid Room ID.");
                }
            }

            if (booking.HotelId != null)
            {
                var hotel = await _context.Hotel.FindAsync(booking.HotelId);
                if (hotel.ID != null)
                {
                    booking.HotelId = hotel.ID;
                }
                else
                {
                    return BadRequest("Invalid Hotel ID.");
                }
            }

            _context.Booking.Add(booking);


            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.ID }, booking);
        }

        // PUT: api/booking/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.ID)
            {
                return BadRequest();
            }

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
