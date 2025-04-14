using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    // Admin controller to manage all users.
    [ApiController]
    [Route("api/admin/users")]
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminUsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: /admin/users
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users
                .Select(u => new { u.Id, u.Name, u.Email, u.UserName })
                .ToList();
            return Ok(users);
        }

        // GET: /admin/users/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            return Ok(new { user.Id, user.Name, user.Email, user.UserName });
        }

        // PUT: /admin/users/{userId}
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateProfileModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            user.Name = model.Name;
            user.Email = model.Email;
            // Update other fields as necessary.

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return Ok(new { message = "User updated successfully." });

            return BadRequest(result.Errors);
        }

        // DELETE: /admin/users/{userId}
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return Ok(new { message = "User deleted successfully." });

            return BadRequest(result.Errors);
        }
    }
}
