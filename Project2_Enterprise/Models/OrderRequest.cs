using Project2_Enterprise.Controllers;

namespace Project2_Enterprise.Models
{
    public class OrderRequest
    {
            public int OrderId { get; set; } // Unique identifier for the order
            public List<OrderItem> Items { get; set; } // List of order items
      
    }
}
