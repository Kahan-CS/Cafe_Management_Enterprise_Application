using AdminClient.Messages;

namespace AdminClient.Services
{
	public class BookingApiService(HttpClient httpClient)
	{
		private readonly HttpClient _httpClient = httpClient;

		// Get all bookings
		public async Task<List<BookingDto>> GetAllBookingsAsync()
		{
			try
			{
				var response = await _httpClient.GetFromJsonAsync<List<BookingDto>>("/api/admin/bookings");
				return response ?? [];
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[BookingApiService] Failed to fetch all bookings: {ex.Message}");
				return [];
			}
		}

		// Get bookings by user
		public async Task<List<BookingDto>> GetBookingsByUserAsync(string userId)
		{
			try
			{
				var response = await _httpClient.GetFromJsonAsync<List<BookingDto>>($"/api/admin/bookings/user/{userId}");
				return response ?? [];
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[BookingApiService] Failed to fetch bookings for user {userId}: {ex.Message}");
				return [];
			}
		}

		// Update a specific booking
		public async Task UpdateBookingAsync(int bookingId, BookingDto updatedBooking)
		{
			try
			{
				var response = await _httpClient.PutAsJsonAsync($"/api/admin/bookings/{bookingId}", updatedBooking);
				response.EnsureSuccessStatusCode();
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[BookingApiService] Failed to update booking {bookingId}: {ex.Message}");
			}
		}

		// Delete a booking
		public async Task DeleteBookingAsync(int bookingId)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"/api/admin/bookings/{bookingId}");
				response.EnsureSuccessStatusCode();
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[BookingApiService] Failed to delete booking {bookingId}: {ex.Message}");
			}
		}

		// Update pricing
		public async Task<ApiResponseDto> UpdateBookingPricingAsync(PricingUpdateDto pricingDto)
		{
			try
			{
				var response = await _httpClient.PutAsJsonAsync("/api/admin/bookings/pricing", pricingDto);
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadFromJsonAsync<ApiResponseDto>() ?? new ApiResponseDto { Success = false, Message = "Empty response." };
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[BookingApiService] Failed to update booking pricing: {ex.Message}");
				return new ApiResponseDto { Success = false, Message = ex.Message };
			}
		}
	}
}
