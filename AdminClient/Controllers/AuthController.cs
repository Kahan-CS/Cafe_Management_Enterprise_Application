using AdminClient.Messages;
using AdminClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminClient.Controllers
{
	public class AuthController(AuthApiService authService) : Controller
	{
		private readonly AuthApiService _authService = authService;

		// GET: /Auth/Register
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		// POST: /Auth/Register
		[HttpPost]
		public async Task<IActionResult> Register(RegisterDto model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var result = await _authService.RegisterAsync(model);
			if (result.Success)
			{
				// If registration succeeds, redirect to login.
				return RedirectToAction("Login");
			}
			ModelState.AddModelError("", result.Message);
			return View(model);
		}

		// GET: /Auth/Login
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		// POST: /Auth/Login
		[HttpPost]
		public async Task<IActionResult> Login(LoginDto model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var result = await _authService.LoginAsync(model);
			if (result.Success)
			{
				// Store the token in session or cookie as appropriate.
				HttpContext.Session.SetString("AuthToken", result.Token);
				return RedirectToAction("Index", "Home");
			}
			ModelState.AddModelError("", result.Message);
			return View(model);
		}

		// POST: /Auth/Logout
		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _authService.LogoutAsync();
			HttpContext.Session.Remove("AuthToken");
			return RedirectToAction("Login");
		}

		// GET: /Auth/ResetPassword
		[HttpGet]
		public IActionResult ResetPassword()
		{
			return View();
		}

		// POST: /Auth/ResetPassword
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var result = await _authService.ResetPasswordAsync(model);
			if (result.Success)
			{
				// You might redirect to a page that informs the user to check their email.
				return RedirectToAction("ResetPasswordConfirmation");
			}
			ModelState.AddModelError("", result.Message);
			return View(model);
		}

		// GET: /Auth/ResetPasswordConfirmation
		[HttpGet]
		public IActionResult ResetPasswordConfirmation()
		{
			return View();
		}

		// TODO: add actions to handle ResetPasswordConfirm
	}
}
