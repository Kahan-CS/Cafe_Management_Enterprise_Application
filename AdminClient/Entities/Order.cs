using System.ComponentModel.DataAnnotations;
using AdminClient.Shared.Enums;

namespace AdminClient.Entities
{
	public class Order
	{
		// PK
		public int OrderId { get; set; }

		// FK
		[Required]
		public int UserId { get; set; }

		public OrderStatus Status { get; set; } = OrderStatus.Pending;
		
		public DateTime TimeCreated { get; set; }

		public List<OrderItem> Items { get; set; } = [];
	}
}
