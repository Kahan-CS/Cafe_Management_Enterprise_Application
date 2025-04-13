using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerClient.ViewModels;


namespace CustomerClient.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingViewModel>> GetBookingsAsync();
        Task<BookingViewModel> CreateBookingAsync(BookingViewModel booking);
    }
}
