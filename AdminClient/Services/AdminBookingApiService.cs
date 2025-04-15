using AdminClient.Messages;

namespace AdminClient.Services
{
    public class AdminBookingApiService
    {
        private readonly HttpClient _httpClient;

        public AdminBookingApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Retrieve all bookings via GET /admin/bookings
        public async Task<List<BookingDto>> GetAllBookingsAsync()
        {
            try
            {
                var bookings = await _httpClient.GetFromJsonAsync<List<BookingDto>>("/admin/bookings");
                return bookings ?? new List<BookingDto>();
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"[AdminBookingApiService] Error fetching all bookings: {ex.Message}");
                return new List<BookingDto>();
            }
        }

        // Retrieve bookings for a specific user via GET /admin/bookings/user/{userId}
        public async Task<List<BookingDto>> GetBookingsByUserAsync(string userId)
        {
            try
            {
                var bookings = await _httpClient.GetFromJsonAsync<List<BookingDto>>($"/admin/bookings/user/{userId}");
                return bookings ?? new List<BookingDto>();
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"[AdminBookingApiService] Error fetching bookings for user {userId}: {ex.Message}");
                return new List<BookingDto>();
            }
        }

        // Update any booking via PUT /admin/bookings/{id}
        public async Task UpdateBookingAsync(int bookingId, BookingDto updatedBooking)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/admin/bookings/{bookingId}", updatedBooking);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"[AdminBookingApiService] Error updating booking {bookingId}: {ex.Message}");
            }
        }

        // Delete any booking via DELETE /admin/bookings/{id}
        public async Task DeleteBookingAsync(int bookingId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/admin/bookings/{bookingId}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"[AdminBookingApiService] Error deleting booking {bookingId}: {ex.Message}");
            }
        }

        // Update booking pricing via PUT /admin/bookings/pricing
        public async Task<ApiResponseDto> UpdateBookingPricingAsync(PricingUpdateDto pricingDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("/admin/bookings/pricing", pricingDto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ApiResponseDto>();
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"[AdminBookingApiService] Error updating pricing: {ex.Message}");
                return new ApiResponseDto { Success = false, Message = ex.Message };
            }
        }
    }
}
