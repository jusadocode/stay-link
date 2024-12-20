using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stay_link.Server.Models;
using stay_link.Server.Data;
using Microsoft.AspNetCore.Authorization;
using stay_link.Server.Auth.Model;

namespace stay_link.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly BookingContext _context;

        public RoomsController(BookingContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Room>))]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            return Ok(rooms); // Wrap the result in Ok()
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Room))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return NotFound(new { message = "Room not found." }); // Add a message for consistency
            }

            return Ok(room); // Return the room wrapped in Ok()
        }

        // POST: api/Rooms
        [HttpPost]
        [Authorize(Roles = BookingRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Room))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid room data.", errors = ModelState }); // Custom error message
            }

            var hotel = await _context.Hotels.FindAsync(room.HotelID);
            if (hotel == null)
            {
                return UnprocessableEntity(new { message = "Specified hotel does not exist." }); // Wrap the message in an object
            }

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoom), new { id = room.ID }, room); // Using nameof for safety
        }

        // PUT: api/Rooms/5
        [HttpPut("{id}")]
        [Authorize(Roles = BookingRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutRoom(int id, Room updatedRoom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid room data.", errors = ModelState }); // Custom error message
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound(new { message = "Room not found." }); // Add a message for consistency
            }

            // Check if the hotel exists before updating
            var hotel = await _context.Hotels.FindAsync(updatedRoom.HotelID);
            if (hotel == null)
            {
                return UnprocessableEntity(new { message = "Specified hotel does not exist." }); // Wrap the message in an object
            }

            // Update room properties
            room.Summary = updatedRoom.Summary;
            room.RoomType = updatedRoom.RoomType;
            room.HotelID = updatedRoom.HotelID;
            room.Price = updatedRoom.Price;
            room.MaxOccupancy = updatedRoom.MaxOccupancy;

            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return NotFound(new { message = "Room not found." }); // Add a message for consistency
                }
                throw; // Rethrow the exception for unexpected errors
            }

            return NoContent(); // Return 204 No Content on success
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        [Authorize(Roles = BookingRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound(new { message = "Room not found." }); // Add a message for consistency
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 No Content on success
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.ID == id);
        }
    }
}
