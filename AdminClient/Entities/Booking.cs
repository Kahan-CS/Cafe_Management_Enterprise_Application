using System.ComponentModel.DataAnnotations;

namespace AdminClient.Entities
{
	public class Booking
	{
		// PK
		public int BookingId { get; set; }

		// FK of the User who created the booking
		public int UserId { get; set; }

		// List of invitations
		public List<Invitation> Invitations { get; set; } = [];

		// Description
		[Required(ErrorMessage = "Please enter a description.")]
		public string Description { get; set; } = string.Empty;

		// Date and time of event
		[Required(ErrorMessage = "Please enter an event date.")]
		[FutureDate(ErrorMessage = "Event date must be in the future.")]
		public DateTime BookingDateTime { get; set; }

		// Custom validation attribute to ensure event date is in the future
		public class FutureDateAttribute : ValidationAttribute
		{
			protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
			{
				if (value is DateTime bookingDateTime)
				{
					if (bookingDateTime <= DateTime.Today)
					{
						return new ValidationResult("The event date must be in the future.");
					}
				}
				return ValidationResult.Success;
			}
		}
	}
}
