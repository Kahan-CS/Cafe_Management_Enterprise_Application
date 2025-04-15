using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http;

namespace AdminClient.Services
{
	// The base API service is meant to abstract the handling of JWT tokens for all other services
	public class BaseApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
	{
		protected readonly HttpClient _httpClient = httpClient;
		protected readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
		protected readonly IConfiguration _configuration = configuration;

		// Adds JWT token to the Authorization header of every request
		protected void AddJwtToHeader()
		{
			var token = _httpContextAccessor.HttpContext?.Session.GetString("JWToken");

			// If token exists, add it to the Authorization header
			if (!string.IsNullOrWhiteSpace(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			else
			{
				// If no token exists, clear the header
				_httpClient.DefaultRequestHeaders.Authorization = null;
			}
		}
	}
}
