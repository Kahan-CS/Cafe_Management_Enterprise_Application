namespace WebAPI.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public decimal Price { get; set; }

        public int OrderRequestId { get; set; }
        public OrderRequest OrderRequest { get; set; }


    }
}
