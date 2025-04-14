namespace AdminClient.Messages
{
	public class BookingDto
	{
		required public string BookingId { get; set; }
		required public string UserId { get; set; }
		public string Description { get; set; } = string.Empty;
		public int TableNumber { get; set; }
		public DateTime EventTime { get; set; }

		public List<InvitationDto> Invitations { get; set; } = [];
	}
}
