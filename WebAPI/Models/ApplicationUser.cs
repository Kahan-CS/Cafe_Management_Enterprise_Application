using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Additional property of requiring a full name
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; } = string.Empty;
    }
}
