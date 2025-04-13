using OrderProcessingAPI.Controllers;

namespace OrderProcessingAPI.Models
{
    public class OrderRequest
    {
            public int OrderId { get; set; }
            public List<OrderItem> Items { get; set; }
    }
}
