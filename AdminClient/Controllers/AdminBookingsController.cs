using AdminClient.Messages;
using AdminClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminClient.Controllers
{
    public class AdminBookingsController : Controller
    {
        private readonly AdminBookingApiService _bookingService;

        public AdminBookingsController(AdminBookingApiService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET: /AdminBookings
        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return View(bookings);
        }

        // GET: /AdminBookings/User/{userId}
        public async Task<IActionResult> UserBookings(string userId)
        {
            var bookings = await _bookingService.GetBookingsByUserAsync(userId);
            return View(bookings);
        }

        // GET: /AdminBookings/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            // In a full implementation, you might have a method to retrieve a single booking.
            // For now, we retrieve all bookings and then filter.
            var bookings = await _bookingService.GetAllBookingsAsync();
            var booking = bookings.FirstOrDefault(b => b.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: /AdminBookings/Edit/{id}
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return View(bookingDto);
            }

            await _bookingService.UpdateBookingAsync(id, bookingDto);
            return RedirectToAction("Index");
        }

        // POST: /AdminBookings/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookingService.DeleteBookingAsync(id);
            return RedirectToAction("Index");
        }

        // GET: /AdminBookings/UpdatePricing
        public IActionResult UpdatePricing()
        {
            return View();
        }

        // POST: /AdminBookings/UpdatePricing
        [HttpPost]
        public async Task<IActionResult> UpdatePricing(PricingUpdateDto pricingDto)
        {
            if (!ModelState.IsValid)
            {
                return View(pricingDto);
            }

            var response = await _bookingService.UpdateBookingPricingAsync(pricingDto);
            if (response.Success)
                return RedirectToAction("Index");

            ModelState.AddModelError("", response.Message);
            return View(pricingDto);
        }
    }
}
