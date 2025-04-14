using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    //handles data the API sends back to the client after processing the order
    public class OrderResponse
    {
        [Key]
        public int OrderId { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }
}
