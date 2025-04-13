using Microsoft.EntityFrameworkCore;

namespace OrderProcessingAPI.Models
{
    public class CafeDbContext:DbContext
    {
        public CafeDbContext(DbContextOptions<CafeDbContext> options)
           : base(options) { }

        public DbSet<OrderRequest> OrderRequests { get; set; }
        public DbSet<OrderResponse> OrderResponses { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
