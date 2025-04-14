namespace AdminClient.Messages
{
    public class PricingUpdateDto
    {
        public int BookingId { get; set; }
        public decimal NewPricing { get; set; }
    }
}
