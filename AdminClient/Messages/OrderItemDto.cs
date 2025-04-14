namespace AdminClient.Messages
{
	public class OrderItemDto
	{
		public int OrderItemId { get; set; }
		public int OrderId { get; set; }
		public int MenuItemId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}
}
