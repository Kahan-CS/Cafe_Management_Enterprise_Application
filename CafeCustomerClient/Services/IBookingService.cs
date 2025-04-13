using System.Collections.Generic;
using System.Threading.Tasks;
using CafeCustomerClient.ViewModels;


namespace CafeCustomerClient.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingViewModel>> GetBookingsAsync();
        Task<BookingViewModel> CreateBookingAsync(BookingViewModel booking);
    }
}
