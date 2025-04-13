using Project2_Enterprise.Controllers;

namespace Project2_Enterprise.Models
{
    public class OrderRequest
    {
            public int OrderId { get; set; }
            public List<OrderItem> Items { get; set; }
      
    }
}
