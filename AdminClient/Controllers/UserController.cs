using AdminClient.Messages;
using AdminClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminClient.Controllers
{
	public class UserController(UserApiService userApiService) : Controller
	{
		private readonly UserApiService _userApiService = userApiService;

		// GET: /User
		public async Task<IActionResult> Index()
		{
			var users = await _userApiService.GetAllUsersAsync();

			if (users.Count == 0)
			{
				ViewBag.ApiError = "Could not retrieve users. Please check the API connection.";
			}

			return View(users);
		}

		// GET: /User/Details/{userId}
		public async Task<IActionResult> Details(string userId)
		{
			var user = await _userApiService.GetUserByIdAsync(userId);
			if (user == null)
				return NotFound();

			return View(user);
		}

		// GET: /User/Edit/{userId}
		public async Task<IActionResult> Edit(string userId)
		{
			var user = await _userApiService.GetUserByIdAsync(userId);
			if (user == null)
				return NotFound();

			return View(user);
		}

		// POST: /User/Edit/{userId}
		[HttpPost]
		public async Task<IActionResult> Edit(string userId, UserDto userDto)
		{
			if (!ModelState.IsValid)
				return View(userDto);

			var response = await _userApiService.UpdateUserAsync(userId, userDto);
			if (response.Success)
				return RedirectToAction(nameof(Index));

			ModelState.AddModelError("", response.Message);
			return View(userDto);
		}

		// POST: /User/Deactivate/{userId}
		[HttpPost]
		public async Task<IActionResult> Deactivate(string userId)
		{
			var response = await _userApiService.DeleteUserAsync(userId);

			if (!response.Success)
			{
				TempData["ApiError"] = response.Message;
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
