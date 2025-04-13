using System.Threading.Tasks;
using CustomerClient.ViewModels;

namespace CustomerClient.Services
{
    public interface IOrderService
    {
        Task<OrderViewModel> CreateOrderAsync(OrderViewModel order);
        Task<OrderViewModel> GetOrderByIdAsync(int orderId);
    }
}
