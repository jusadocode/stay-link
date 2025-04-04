using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stay_link.Server.Models;
using stay_link.Server.Data;

namespace stay_link.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly BookingContext _context;

        public HotelsController(BookingContext context)
        {
            _context = context;
        }

        // GET: api/Hotels
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Hotel>))]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();
            return Ok(hotels); // Returning 200 OK with a list of hotels
        }

        // GET: api/Hotels/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Hotel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
                
            if (hotel == null)
            {
                return NotFound(new { message = "Hotel not found." }); // More uniform message
            }

            return Ok(hotel); // Return the found hotel
        }

        // GET: api/Hotels/{id}/Rooms
        [HttpGet("{id}/Rooms")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Room>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<IEnumerable<Room>>> GetHotelRooms(int id)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
                
            if (hotel == null)
            {
                return NotFound(new { message = "Hotel not found." }); // More uniform message
            }

            var rooms = await _context.Rooms.Where(r => r.HotelId == hotel.Id).ToListAsync();
                
            return Ok(rooms); // Returning the list of rooms in the hotel
        }

        // PUT: api/Hotels/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = BookingRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutHotel(int id, Hotel hotelUpdated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data provided.", errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
            }

            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound(new { message = "Hotel not found." });
            }

            hotel.Name = hotelUpdated.Name;
            hotel.Address = hotelUpdated.Address;
            hotel.ImageUrl = hotelUpdated.ImageUrl;

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
                {
                    return NotFound(new { message = "Hotel not found." });
                }
                throw; 
            }

            return NoContent();
        }

        // POST: api/Hotels
        [HttpPost]
        [Authorize(Roles = BookingRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Hotel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data provided." });
            }

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotel); // Use nameof for refactoring safety
        }

        // DELETE: api/Hotels/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = BookingRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound(new { message = "Hotel not found." });
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return _context.Hotels.Any(e => e.Id == id);
        }
    }
}
