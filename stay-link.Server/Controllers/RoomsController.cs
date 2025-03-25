using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stay_link.Server.Models;
using stay_link.Server.Data;
using Microsoft.AspNetCore.Authorization;
using stay_link.Server.Auth.Model;
using stay_link.Server.Services;
using stay_link.Server.DTO;

namespace stay_link.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // GET: api/Rooms
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Room>))]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            var rooms = await _roomService.GetRooms();
            return Ok(rooms); // Wrap the result in Ok()
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Room))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            var room = await _roomService.GetRoom(id);
            if (room == null)
                return NotFound(new { message = "Room not found." });

            return Ok(room);
        }

        // POST: api/Rooms
        [HttpPost]
        [Authorize(Roles = BookingRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Room))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Room>> PostRoom(CreateRoomDTO room)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid room data.", errors = ModelState });

            try
            {
                var createdRoom = await _roomService.CreateRoom(room);
                return CreatedAtAction(nameof(GetRoom), new { id = createdRoom.ID }, createdRoom);
            }
            catch (System.Exception ex)
            {
                return UnprocessableEntity(new { message = ex.Message });
            }
        }

        // PUT: api/Rooms/5
        [HttpPut("{id}")]
        [Authorize(Roles = BookingRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutRoom(int id, UpdateRoomDTO updatedRoom)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid room data.", errors = ModelState });

            try
            {
                var success = await _roomService.UpdateRoom(id, updatedRoom);
                if (!success)
                    return NotFound(new { message = "Room not found." });

                return NoContent();
            }
            catch (System.Exception ex)
            {
                return UnprocessableEntity(new { message = ex.Message });
            }
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        [Authorize(Roles = BookingRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteRoom(int id)
    {
        var success = await _roomService.DeleteRoom(id);
        if (!success) return NotFound(new { message = "Room not found." });

        return NoContent();
    }

    }
}
