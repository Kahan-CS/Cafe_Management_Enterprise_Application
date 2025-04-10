using System.ComponentModel.DataAnnotations;

namespace AdminClient.Entities
{
	// Possible statuses for an invitation
	public enum Status
	{
		InviteNotSent,
		InviteSent,
		RespondedYes,
		RespondedNo
	}

	public class Invitation
	{
		// PK
		[Required]
		public int InvitationId { get; set; }

		// FK to map to a booking
		[Required(ErrorMessage = "Please select a party.")]
		public int BookingId { get; set; }

		// Guest Name
		[Required(ErrorMessage = "Please enter a guest name.")]
		public string GuestName { get; set; } = string.Empty;

		public DateTime SentAt { get; set; }

		// Invitation status, using enum defined above
		[Required(ErrorMessage = "Please select an invitation status.")]
		public Status Status { get; set; } = Status.InviteNotSent;
		public Booking? Booking { get; set; }

		// Convert Status enum to a user-friendly string
		public string GetStatusDisplayString()
		{
			return Status switch
			{
				Status.InviteNotSent => "Invite not sent",
				Status.InviteSent => "Invite sent",
				Status.RespondedYes => "Responded yes",
				Status.RespondedNo => "Responded no",
				_ => "Unknown status"
			};
		}
	}
}
