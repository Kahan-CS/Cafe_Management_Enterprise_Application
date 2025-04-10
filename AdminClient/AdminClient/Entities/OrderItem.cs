namespace AdminClient.Entities
{
	public class OrderItem
	{
		public int OrderItemId { get; set; }

		// FK
		public int OrderId { get; set; }

		public int MenuItemId { get; set; }
		
		public int Quantity { get; set; }
		
		public decimal Price { get; set; }
	}
}
