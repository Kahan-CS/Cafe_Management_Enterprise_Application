using AdminClient.Messages;

namespace AdminClient.Services
{
	public class UserApiService(HttpClient httpClient)
	{
		private readonly HttpClient _httpClient = httpClient;

		// Retrieve all users
		public async Task<List<UserDto>> GetAllUsersAsync()
		{
			// Gracefully return empty list if HTTP request fails
			try
			{
				var response = await _httpClient.GetFromJsonAsync<List<UserDto>>("/admin/users");
				return response ?? [];
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[UserApiService] Failed to fetch users: {ex.Message}");
				return [];
			}
		}

		// Get a user by UserID
		public async Task<UserDto?> GetUserByIdAsync(int userId)
		{
			try
			{
				return await _httpClient.GetFromJsonAsync<UserDto>($"/admin/users/{userId}");
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[UserApiService] Failed to fetch user {userId}: {ex.Message}");
				return null;
			}
		}

		// Update a user
		public async Task UpdateUserAsync(int userId, UserDto updatedUser)
		{
			try
			{
				await _httpClient.PutAsJsonAsync($"/admin/users/{userId}", updatedUser);
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[UserApiService] Failed to update user {userId}: {ex.Message}");
			}
		}

		// Delete a user
		public async Task DeleteUserAsync(int userId)
		{
			try
			{
				await _httpClient.DeleteAsync($"/admin/users/{userId}");
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[UserApiService] Failed to delete user {userId}: {ex.Message}");
			}
		}
	}
}
