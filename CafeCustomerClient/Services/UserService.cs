using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CafeCustomerClient.ViewModels;

namespace CafeCustomerClient.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> LoginAsync(LoginViewModel loginModel)
        {
            // Replace with real API call when available:
            // var response = await _httpClient.PostAsJsonAsync("api/login", loginModel);
            // return response.IsSuccessStatusCode;

            // Simulated successful login:
            return await Task.FromResult(true);
        }

        public async Task<bool> RegisterAsync(RegisterViewModel registerModel)
        {
            // Replace with real API call when available:
            // var response = await _httpClient.PostAsJsonAsync("api/register", registerModel);
            // return response.IsSuccessStatusCode;

            // Simulated successful registration:
            return await Task.FromResult(true);
        }

        public async Task LogoutAsync()
        {
            // Replace with a real API call when available:
            // await _httpClient.PostAsync("api/logout", null);
            await Task.CompletedTask;
        }
    }

}
