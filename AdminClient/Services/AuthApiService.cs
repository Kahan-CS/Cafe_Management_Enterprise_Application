using AdminClient.Messages;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace AdminClient.Services
{
	public class AuthApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
	{
		private readonly HttpClient _httpClient = httpClient;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

		private void AddJwtToHeader()
		{
			var token = _httpContextAccessor.HttpContext?.Session.GetString("JWToken");
			if (!string.IsNullOrWhiteSpace(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			else
			{
				_httpClient.DefaultRequestHeaders.Authorization = null;
			}
		}

		// Registers a new user.
		public async Task<ApiResponseDto> RegisterAsync(RegisterDto registerDto)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync("/api/register", registerDto);
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadFromJsonAsync<ApiResponseDto>();
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[AuthApiService] Register failed: {ex.Message}");
				return new ApiResponseDto { Success = false, Message = ex.Message };
			}
		}

		// Logs in a user and retrieves a JWT token.
		public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync("/api/login", loginDto);
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[AuthApiService] Login failed: {ex.Message}");
				return new LoginResponseDto { Success = false, Token = string.Empty, Message = ex.Message };
			}
		}

		// Logs out the current user.
		public async Task<ApiResponseDto> LogoutAsync()
		{
			try
			{
				var response = await _httpClient.PostAsync("/api/logout", null);
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadFromJsonAsync<ApiResponseDto>();
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[AuthApiService] Logout failed: {ex.Message}");
				return new ApiResponseDto { Success = false, Message = ex.Message };
			}
		}

		// Sends a password reset link.
		public async Task<ApiResponseDto> ResetPasswordAsync(ResetPasswordDto resetDto)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync("/api/reset-password", resetDto);
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadFromJsonAsync<ApiResponseDto>();
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[AuthApiService] ResetPassword failed: {ex.Message}");
				return new ApiResponseDto { Success = false, Message = ex.Message };
			}
		}

		// Confirms the password reset.
		public async Task<ApiResponseDto> ResetPasswordConfirmAsync(ResetPasswordConfirmDto resetConfirmDto)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync("/api/reset-password-confirm", resetConfirmDto);
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadFromJsonAsync<ApiResponseDto>();
			}
			catch (HttpRequestException ex)
			{
				Console.Error.WriteLine($"[AuthApiService] ResetPasswordConfirm failed: {ex.Message}");
				return new ApiResponseDto { Success = false, Message = ex.Message };
			}
		}
	}
}
