using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using stay_link.Server.Models;
using stay_link.Server.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly BookingService _bookingService;

    public BookingsController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<BookingDTO>>> GetBookings()
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        var isAdmin = User.IsInRole(BookingRoles.Admin);

        var bookings = await _bookingService.GetBookings(userId, isAdmin);
        return Ok(bookings);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Booking>> GetBooking(int id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        var isAdmin = User.IsInRole(BookingRoles.Admin);

        var booking = await _bookingService.GetBooking(id, userId, isAdmin);
        if (booking == null) return NotFound(new { message = "Booking not found." });

        return Ok(booking);
    }

    [HttpPost]
    [Authorize(Roles = BookingRoles.BookingUser)]
    public async Task<ActionResult<BookingDTO>> PostBooking(CreateBookingDTO bookingDTO)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        

        var booking = await _bookingService.CreateBooking(bookingDTO, userId);

        return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutBooking(int id, BookingDTO bookingDTO)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        var isAdmin = User.IsInRole(BookingRoles.Admin);

        var success = await _bookingService.UpdateBooking(id, bookingDTO, userId, isAdmin);
        if (!success) return NotFound(new { message = "Booking not found." });

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = BookingRoles.Admin)]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        var success = await _bookingService.DeleteBooking(id);
        if (!success) return NotFound(new { message = "Booking not found." });

        return NoContent();
    }
}