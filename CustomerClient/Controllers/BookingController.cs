using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerClient.Services;
using CustomerClient.ViewModels;

namespace CustomerClient.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET: /Booking/Create
        public IActionResult Create()
        {
            return View(new BookingViewModel());
        }

        // POST: /Booking/Create
        [HttpPost]
        public async Task<IActionResult> Create(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var createdBooking = await _bookingService.CreateBookingAsync(model);
                return RedirectToAction("List");
            }
            return View(model);
        }

        // GET: /Booking/List
        public async Task<IActionResult> List()
        {
            var bookings = await _bookingService.GetBookingsAsync();
            return View(bookings);
        }
    }
}
