using AdminClient.Shared.Enums;

namespace AdminClient.Messages
{
	public class InvitationDto
	{
		public int InvitationId { get; set; }
		public int BookingId { get; set; }
		public string GuestName { get; set; } = string.Empty;
		public DateTime SentAt { get; set; }
		public InvitationStatus Status { get; set; } = InvitationStatus.InviteNotSent;
	}
}
