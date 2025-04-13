using System.Threading.Tasks;
using CustomerClient.ViewModels;
using System.Net.Http;
using System.Net.Http.Json;


namespace CustomerClient.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OrderViewModel> CreateOrderAsync(OrderViewModel order)
        {
            // Replace with a real API call when available:
            // var response = await _httpClient.PostAsJsonAsync("api/orders", order);
            // response.EnsureSuccessStatusCode();
            // return await response.Content.ReadFromJsonAsync<OrderViewModel>();

            // Simulate order creation:
            order.Id = 1; // mock id assignment
            return await Task.FromResult(order);
        }

        public async Task<OrderViewModel> GetOrderByIdAsync(int orderId)
        {
            // Replace with a real API call when available:
            // var response = await _httpClient.GetAsync($"api/orders/{orderId}");
            // response.EnsureSuccessStatusCode();
            // return await response.Content.ReadFromJsonAsync<OrderViewModel>();

            // Return a mock order:
            return await Task.FromResult(new OrderViewModel
            {
                Id = orderId,
                Items = "Espresso, Croissant",
                TotalPrice = 15.50m
            });
        }
    }
}
