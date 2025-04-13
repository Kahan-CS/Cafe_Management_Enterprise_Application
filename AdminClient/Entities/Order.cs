using System.ComponentModel.DataAnnotations;

namespace AdminClient.Entities
{
	public class Order
	{
		// PK
		public int OrderId { get; set; }

		// FK
		[Required]
		public int UserId { get; set; }

		// TODO: should be enum
		public string Status { get; set; }

		public List<OrderItem> Items { get; set; } = [];
	}
}
