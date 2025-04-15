using AdminClient.Messages;

namespace AdminClient.Services
{
    public class AdminUserApiService
    {
        private readonly HttpClient _httpClient;

        public AdminUserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all users from the API at /admin/users
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            try
            {
                var users = await _httpClient.GetFromJsonAsync<List<UserDto>>("/admin/users");
                return users ?? new List<UserDto>();
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"[AdminUserApiService] Failed to fetch users: {ex.Message}");
                return new List<UserDto>();
            }
        }

        // Get a user by ID from the API at /admin/users/{userId}
        public async Task<UserDto?> GetUserByIdAsync(string userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<UserDto>($"/admin/users/{userId}");
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"[AdminUserApiService] Failed to fetch user {userId}: {ex.Message}");
                return null;
            }
        }

        // Update user details via PUT /admin/users/{userId}
        public async Task<ApiResponseDto> UpdateUserAsync(string userId, UserDto updatedUser)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/admin/users/{userId}", updatedUser);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ApiResponseDto>() ?? new ApiResponseDto();
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"[AdminUserApiService] Failed to update user {userId}: {ex.Message}");
                return new ApiResponseDto { Success = false, Message = ex.Message };
            }
        }

        // Delete a user via DELETE /admin/users/{userId}
        public async Task<ApiResponseDto> DeleteUserAsync(string userId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/admin/users/{userId}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ApiResponseDto>() ?? new ApiResponseDto();
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"[AdminUserApiService] Failed to delete user {userId}: {ex.Message}");
                return new ApiResponseDto { Success = false, Message = ex.Message };
            }
        }
    }
}
