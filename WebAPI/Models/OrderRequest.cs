using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    //handles the data the client sends to the API when placing an order.
    public class OrderRequest
    {
        [Key]
        public int OrderId { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
