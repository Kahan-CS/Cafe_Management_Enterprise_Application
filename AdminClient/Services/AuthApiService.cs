using AdminClient.Messages;

namespace AdminClient.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;

        public AuthApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
