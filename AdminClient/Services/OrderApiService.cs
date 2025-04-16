using AdminClient.Messages;

namespace AdminClient.Services
{
	public class OrderApiService(HttpClient httpClient)
	{
		private readonly HttpClient _httpClient = httpClient;

		// Get all orders
		public async Task<List<OrderDto>> GetAllOrdersAsync()
		{
			try
			{
				var response = await _httpClient.GetFromJsonAsync<List<OrderDto>>("/api/admin/orders");
				return response ?? [];
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[OrderApiService] Failed to fetch all orders: {ex.Message}");
				return [];
			}
		}

		// Get all orders by one user
		public async Task<List<OrderDto>> GetOrdersByUserAsync(int userId)
		{
			try
			{
				var response = await _httpClient.GetFromJsonAsync<List<OrderDto>>($"/api/admin/orders/user/{userId}");
				return response ?? [];
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[OrderApiService] Failed to fetch orders for user {userId}: {ex.Message}");
				return [];
			}
		}

		// Update order (e.g. to change order status)
		public async Task UpdateOrderAsync(int orderId, OrderDto updatedOrder)
		{
			try
			{
				await _httpClient.PutAsJsonAsync($"/api/admin/orders/{orderId}", updatedOrder);
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[OrderApiService] Failed to update order {orderId}: {ex.Message}");
			}
		}

		// Delete order
		public async Task DeleteOrderAsync(int orderId)
		{
			try
			{
				await _httpClient.DeleteAsync($"/api/admin/orders/{orderId}");
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[OrderApiService] Failed to delete order {orderId}: {ex.Message}");
			}
		}
	}
}
