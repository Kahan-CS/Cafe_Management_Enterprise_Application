using System.ComponentModel.DataAnnotations;

namespace AdminClient.Entities
{
	public enum Role
	{
		Admin,
		Customer,
	}

	public class User
	{
		public int UserId { get; set; }

		[Required(ErrorMessage = "Please enter a name.")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter an email.")]
		public string Email { get; set; } = string.Empty;

		// TODO: use a hash, not plaintext!
		[Required(ErrorMessage = "Please provide a password.")]
		public string Password { get; set; } = string.Empty;

		// The user's role
		[Required(ErrorMessage = "Please select a role for the user.")]
		public Role Role { get; set; } = Role.Customer;
	}
}
