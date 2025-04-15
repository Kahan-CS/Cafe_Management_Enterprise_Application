namespace AdminClient.Messages
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string CreatedByUserId { get; set; } = string.Empty;
        public int NumberOfInvitations { get; set; }
    }
}
