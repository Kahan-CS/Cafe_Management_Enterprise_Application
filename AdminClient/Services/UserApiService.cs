using AdminClient.Messages;

namespace AdminClient.Services
{
	public class UserApiService(HttpClient httpClient)
	{
		private readonly HttpClient _httpClient = httpClient;

		// Retrieve all users
		public async Task<List<UserDto>> GetAllUsersAsync()
		{
			try
			{
				var response = await _httpClient.GetFromJsonAsync<List<UserDto>>("/api/admin/users");
				return response ?? [];
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[UserApiService] Failed to fetch users: {ex.Message}");
				return [];
			}
		}

		// Get a user by ID (string for Identity GUID)
		public async Task<UserDto?> GetUserByIdAsync(string userId)
		{
			try
			{
				return await _httpClient.GetFromJsonAsync<UserDto>($"/api/admin/users/{userId}");
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[UserApiService] Failed to fetch user {userId}: {ex.Message}");
				return null;
			}
		}

		// Update a user
		public async Task<ApiResponseDto> UpdateUserAsync(string userId, UserDto updatedUser)
		{
			try
			{
				var response = await _httpClient.PutAsJsonAsync($"/api/admin/users/{userId}", updatedUser);
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadFromJsonAsync<ApiResponseDto>() ?? new ApiResponseDto();
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[UserApiService] Failed to update user {userId}: {ex.Message}");
				return new ApiResponseDto { Success = false, Message = ex.Message };
			}
		}

		// Delete a user
		public async Task<ApiResponseDto> DeleteUserAsync(string userId)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"/api/admin/users/{userId}");
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadFromJsonAsync<ApiResponseDto>() ?? new ApiResponseDto();
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[UserApiService] Failed to delete user {userId}: {ex.Message}");
				return new ApiResponseDto { Success = false, Message = ex.Message };
			}
		}
	}
}
