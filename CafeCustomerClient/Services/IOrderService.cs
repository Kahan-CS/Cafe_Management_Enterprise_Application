using System.Threading.Tasks;
using CafeCustomerClient.ViewModels;

namespace CafeCustomerClient.Services
{
    public interface IOrderService
    {
        Task<OrderViewModel> CreateOrderAsync(OrderViewModel order);
        Task<OrderViewModel> GetOrderByIdAsync(int orderId);
    }
}
