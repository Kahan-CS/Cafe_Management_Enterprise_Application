namespace Project2_Enterprise.Models
{
    public class OrderResponse
    {
        public int OrderId { get; set; } 
        public List<OrderItem> Items { get; set; } 
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; } 
    }
}

