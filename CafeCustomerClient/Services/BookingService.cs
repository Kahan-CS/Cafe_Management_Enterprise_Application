using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CafeCustomerClient.ViewModels;

namespace CafeCustomerClient.Services
{
    public class BookingService : IBookingService
    {
        private readonly HttpClient _httpClient;
        public BookingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BookingViewModel>> GetBookingsAsync()
        {
            // Replace with real API call when available:
            // var response = await _httpClient.GetAsync("api/bookings/my");
            // response.EnsureSuccessStatusCode();
            // return await response.Content.ReadFromJsonAsync<IEnumerable<BookingViewModel>>();

            // Mock data for now:
            return new List<BookingViewModel>
            {
                new BookingViewModel
                {
                    BookingDate = DateTime.Today,
                    BookingTime = new TimeSpan(18, 0, 0),
                    GuestCount = 5,
                    BookingDescription = "Team Meeting (Mock)",
                    InvitedGuests = new List<string> { "user1@example.com", "user2@example.com" }
                }
            };
        }

        public async Task<BookingViewModel> CreateBookingAsync(BookingViewModel booking)
        {
            // Replace with the real API call when ready:
            // var response = await _httpClient.PostAsJsonAsync("api/bookings", booking);
            // response.EnsureSuccessStatusCode();
            // return await response.Content.ReadFromJsonAsync<BookingViewModel>();

            // Simulated response for now:
            booking.BookingDescription += " (Mock)";
            return await Task.FromResult(booking);
        }
    }
}
