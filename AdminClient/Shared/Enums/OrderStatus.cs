namespace AdminClient.Shared.Enums
{
	// Possible order status values
	public enum OrderStatus
	{
		Pending,        // Placed but not started
		InProgress,     // Being prepared
		Ready,          // Ready for pickup/delivery
		Completed,      // Already delivered
		Cancelled       // Cancelled by user or staff
	}
}
