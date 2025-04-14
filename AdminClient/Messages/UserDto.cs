using AdminClient.Shared.Enums;

namespace AdminClient.Messages
{
	public class UserDto
	{
		public int UserId { get; set; }

		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;

		//public UserRole Role { get; set; } = UserRole.Customer;
	}
}
