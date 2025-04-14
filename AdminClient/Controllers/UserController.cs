using AdminClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminClient.Controllers
{
	public class UserController(UserApiService userApiService) : Controller
	{
		private readonly UserApiService _userApiService = userApiService;

		public async Task<IActionResult> Index()
		{
			var users = await _userApiService.GetAllUsersAsync();

			if (users.Count == 0)
			{
				ViewBag.ApiError = "Could not retrieve users. Please check the API connection.";
			}

			return View(users);
		}

		[HttpPost]
		public async Task<IActionResult> Deactivate(int userId)
		{
			await _userApiService.DeleteUserAsync(userId);
			return RedirectToAction(nameof(Index));
		}
	}
}
