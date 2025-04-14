using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    // Controller for user profile management endpoints.
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: /users/my
        [HttpGet("my")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            return Ok(new { user.Id, user.Name, user.Email, user.UserName });
        }

        // PUT: /users/my
        [HttpPut("my")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateProfileModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            user.Name = model.Name;
            user.Email = model.Email;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return Ok(new { message = "Profile updated successfully." });

            return BadRequest(result.Errors);
        }

        // DELETE: /users/my
        [HttpDelete("my")]
        public async Task<IActionResult> DeleteMyAccount()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return Ok(new { message = "Account deactivated/deleted successfully." });

            return BadRequest(result.Errors);
        }
    }
}
