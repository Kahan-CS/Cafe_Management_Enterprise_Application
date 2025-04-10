namespace Project2_Enterprise.Models
{
    public class OrderResponse
    {
        public int OrderId { get; set; } // Unique identifier for the order
        public List<OrderItem> Items { get; set; } // List of order items
        public decimal Subtotal { get; set; } // Total before tax
        public decimal Total { get; set; } // Total including tax
    }
}
}
