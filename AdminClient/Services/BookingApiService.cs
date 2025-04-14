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
				var response = await _httpClient.GetFromJsonAsync<List<BookingDto>>("/admin/bookings");
				return response ?? [];
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[BookingApiService] Failed to fetch all bookings: {ex.Message}");
				return [];
			}
		}

		// Get bookings by user
		public async Task<List<BookingDto>> GetBookingsByUserAsync(int userId)
		{
			try
			{
				var response = await _httpClient.GetFromJsonAsync<List<BookingDto>>($"/admin/bookings/user/{userId}");
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
				await _httpClient.PutAsJsonAsync($"/admin/bookings/{bookingId}", updatedBooking);
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
				await _httpClient.DeleteAsync($"/admin/bookings/{bookingId}");
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[BookingApiService] Failed to delete booking {bookingId}: {ex.Message}");
			}
		}
	}
}
