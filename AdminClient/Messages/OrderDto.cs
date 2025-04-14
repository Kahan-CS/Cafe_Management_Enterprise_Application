using AdminClient.Shared.Enums;

namespace AdminClient.Messages
{
	public class OrderDto
	{
		public int OrderId { get; set; }
		public int UserId { get; set; }
		public OrderStatus Status { get; set; }
		public DateTime TimeCreated { get; set; }

		public List<OrderItemDto> Items { get; set; } = [];
	}
}
