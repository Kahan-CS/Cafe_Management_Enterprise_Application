namespace WebAPI.Controllers
{
    using global::WebAPI.Data;
    using global::WebAPI.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;
    using System.Threading.Tasks;

    namespace WebAPI.Controllers
    {
        [Route("api/bookings")]
        [ApiController]
        [Authorize]
        public class BookingsController : ControllerBase
        {
            private readonly WebAPIDbContext _context;

            public BookingsController(WebAPIDbContext context)
            {
                _context = context;
            }

            // POST: /api/bookings
            [HttpPost]
            public async Task<IActionResult> CreateBooking([FromBody] Booking booking)
            {
                // Assign the currently logged-in user as the creator
                string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                booking.CreatedByUserId = currentUserId;

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetBooking), new { id = booking.BookingId }, booking);
            }

            // GET: /api/bookings/my
            [HttpGet("my")]
            public async Task<IActionResult> GetMyBookings()
            {
                string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var bookings = await _context.Bookings
                    .Include(b => b.Invitations)
                    .Where(b => b.CreatedByUserId == currentUserId)
                    .ToListAsync();
                return Ok(bookings);
            }

            // GET: /api/bookings/{id}
            [HttpGet("{id}")]
            public async Task<IActionResult> GetBooking(int id)
            {
                string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var booking = await _context.Bookings
                    .Include(b => b.Invitations)
                    .FirstOrDefaultAsync(b => b.BookingId == id && b.CreatedByUserId == currentUserId);
                if (booking == null)
                    return NotFound();
                return Ok(booking);
            }

            // PUT: /api/bookings/{id}
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking updatedBooking)
            {
                string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id && b.CreatedByUserId == currentUserId);
                if (booking == null)
                    return NotFound();

                // Update fields – you might want to restrict which fields can be updated.
                booking.Description = updatedBooking.Description;
                booking.EventDate = updatedBooking.EventDate;
                booking.Location = updatedBooking.Location;

                await _context.SaveChangesAsync();
                return Ok(booking);
            }

            // DELETE: /api/bookings/{id}
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteBooking(int id)
            {
                string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id && b.CreatedByUserId == currentUserId);
                if (booking == null)
                    return NotFound();

                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Booking canceled successfully." });
            }

            // POST: /api/bookings/{id}/invite
            [HttpPost("{id}/invite")]
            public async Task<IActionResult> InviteGuests(int id, [FromBody] Invitation invitation)
            {
                // Ensure the booking belongs to the current user
                string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id && b.CreatedByUserId == currentUserId);
                if (booking == null)
                    return NotFound();

                // Associate the invitation with the booking
                invitation.BookingId = id;
                invitation.Status = InvitationStatus.InviteNotSent;
                _context.Invitations.Add(invitation);
                await _context.SaveChangesAsync();
                return Ok(invitation);
            }

            // PUT: /api/bookings/{id}/responses/{guestId}
            [HttpPut("{id}/responses/{guestId}")]
            public async Task<IActionResult> UpdateGuestResponse(int id, int guestId, [FromBody] Invitation updatedInvitation)
            {
                // Ensure the booking belongs to the current user
                string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id && b.CreatedByUserId == currentUserId);
                if (booking == null)
                    return NotFound();

                var invitation = await _context.Invitations.FirstOrDefaultAsync(i => i.InvitationId == guestId && i.BookingId == id);
                if (invitation == null)
                    return NotFound();

                // Update RSVP status (and any other editable fields)
                invitation.Status = updatedInvitation.Status;
                await _context.SaveChangesAsync();
                return Ok(invitation);
            }
        }
    }

}
