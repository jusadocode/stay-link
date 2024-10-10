using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stay_link.Server.Models;
using stay_link.Server.Data;

namespace stay_link.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly BookingContext _context;

        public RoomController(BookingContext context)
        {
            _context = context;
        }

        // GET: api/Room
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            return await _context.Room.ToListAsync();
        }

        // GET: api/Room/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _context.Room.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        // POST: api/Room
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            _context.Room.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoom), new { id = room.ID }, room);
        }

        // PUT: api/Room/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room updatedRoom)
        {
            var room = await _context.Room.FindAsync(id);

            if(room == null)
            {
                return NotFound();
            }

            room.Summary = updatedRoom.Summary;
            room.RoomType = updatedRoom.RoomType;
            room.HotelID = updatedRoom.HotelID;
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Room/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Room.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomExists(int id)
        {
            return _context.Room.Any(e => e.ID == id);
        }
    }
}
