using AdminClient.Messages;
using AdminClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminClient.Controllers
{
	public class BookingController(BookingApiService bookingService) : Controller
	{
		private readonly BookingApiService _bookingService = bookingService;

		// GET: All bookings
		public async Task<IActionResult> Index()
		{
			var bookings = await _bookingService.GetAllBookingsAsync();

			if (bookings.Count == 0)
			{
				ViewBag.ApiError = "Could not retrieve bookings. Please check the API connection.";
			}

			return View(bookings);
		}

		// GET: Bookings filtered by user
		public async Task<IActionResult> FilterByUser(string userId)
		{
			try
			{
				var bookings = await _bookingService.GetBookingsByUserAsync(userId);
				return View("Index", bookings);
			}
			catch (Exception ex)
			{
				ViewBag.ApiError = ex.Message;
				return View("Index", new List<BookingDto>());
			}
		}

		// POST: Cancel booking
		[HttpPost]
		public async Task<IActionResult> Cancel(int id)
		{
			await _bookingService.DeleteBookingAsync(id);
			return RedirectToAction("Index");
		}

		// GET: Edit booking
		public async Task<IActionResult> Edit(int id)
		{
			var bookings = await _bookingService.GetAllBookingsAsync();
			var booking = bookings.FirstOrDefault(b => b.BookingId == id);
			if (booking == null)
			{
				return NotFound();
			}
			return View(booking);
		}

		// POST: Edit booking
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

		// GET: Update pricing
		public IActionResult UpdatePricing()
		{
			return View();
		}

		// POST: Update pricing
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
