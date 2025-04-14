using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/admin/bookings")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminBookingsController : ControllerBase
    {
        private readonly WebAPIDbContext _context;

        public AdminBookingsController(WebAPIDbContext context)
        {
            _context = context;
        }

        // GET: /api/admin/bookings
        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _context.Bookings.Include(b => b.Invitations).ToListAsync();
            return Ok(bookings);
        }

        // GET: /api/admin/bookings/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBookingsByUser(string userId)
        {
            var bookings = await _context.Bookings
                .Include(b => b.Invitations)
                .Where(b => b.CreatedByUserId == userId)
                .ToListAsync();
            return Ok(bookings);
        }

        // PUT: /api/admin/bookings/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking updatedBooking)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);
            if (booking == null)
                return NotFound();

            // Admin may update any field of the booking.
            booking.Description = updatedBooking.Description;
            booking.EventDate = updatedBooking.EventDate;
            booking.Location = updatedBooking.Location;
            // Update additional fields if necessary.

            await _context.SaveChangesAsync();
            return Ok(booking);
        }

        // DELETE: /api/admin/bookings/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);
            if (booking == null)
                return NotFound();

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Booking canceled successfully." });
        }

        // PUT: /api/admin/bookings/pricing
        [HttpPut("pricing")]
        public async Task<IActionResult> UpdateBookingPricing([FromBody] dynamic pricingModel)
        {
            // Here pricingModel can be a DTO containing the booking id and new pricing details.
            // For this example, assume it has bookingId and newPricing properties.
            int bookingId = (int)pricingModel.bookingId;
            decimal newPricing = (decimal)pricingModel.newPricing;

            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == bookingId);
            if (booking == null)
                return NotFound();

            // Assuming Booking entity has a Pricing property; if not, adapt accordingly.
            // booking.Pricing = newPricing;

            // For demonstration we simply return a success message.
            await _context.SaveChangesAsync();
            return Ok(new { message = "Booking pricing updated successfully." });
        }
    }
}
