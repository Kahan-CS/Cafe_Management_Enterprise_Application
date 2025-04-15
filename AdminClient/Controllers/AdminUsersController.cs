using AdminClient.Messages;
using AdminClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminClient.Controllers
{
    public class AdminUsersController : Controller
    {
        private readonly AdminUserApiService _userService;

        public AdminUsersController(AdminUserApiService userService)
        {
            _userService = userService;
        }

        // GET: /AdminUsers
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        // GET: /AdminUsers/Details/{id}
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // GET: /AdminUsers/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: /AdminUsers/Edit/{id}
        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserDto userDto)
        {
            if (!ModelState.IsValid)
                return View(userDto);

            var response = await _userService.UpdateUserAsync(id, userDto);
            if (response.Success)
                return RedirectToAction("Index");

            ModelState.AddModelError("", response.Message);
            return View(userDto);
        }

        // POST: /AdminUsers/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _userService.DeleteUserAsync(id);
            if (response.Success)
                return RedirectToAction("Index");

            return BadRequest(response.Message);
        }
    }
}
