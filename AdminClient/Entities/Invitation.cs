using System.ComponentModel.DataAnnotations;
using AdminClient.Shared.Enums;

namespace AdminClient.Entities
{
	public class Invitation
	{
		// PK
		[Required]
		public int InvitationId { get; set; }

		// FK to map to a booking
		[Required(ErrorMessage = "Please select a party.")]
		public int BookingId { get; set; }

		public Booking? Booking { get; set; }

		// Guest Name
		[Required(ErrorMessage = "Please enter a guest name.")]
		public string GuestName { get; set; } = string.Empty;

		public DateTime SentAt { get; set; }

		// Invitation status
		[Required(ErrorMessage = "Please select an invitation status.")]
		public InvitationStatus Status { get; set; } = InvitationStatus.InviteNotSent;
	}
}
