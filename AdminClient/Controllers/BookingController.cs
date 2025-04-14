using AdminClient.Entities;
using AdminClient.Messages;
using AdminClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminClient.Controllers
{
	public class BookingController(BookingApiService bookingService) : Controller
	{
		private readonly BookingApiService _bookingService = bookingService;

		// Get all bookings
		public async Task<IActionResult> Index()
		{
			var bookings = await _bookingService.GetAllBookingsAsync();

			if (bookings.Count == 0)
			{
				ViewBag.ApiError = "Could not retrieve bookings. Please check the API connection.";
			}

			return View(bookings);
		}

		// Get bookings for a particular user
		public async Task<IActionResult> FilterByUser(int userId)
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

		// Cancel booking
		[HttpPost]
		public async Task<IActionResult> Cancel(int id)
		{
			await _bookingService.DeleteBookingAsync(id);
			return RedirectToAction("Index");
		}
	}
}
