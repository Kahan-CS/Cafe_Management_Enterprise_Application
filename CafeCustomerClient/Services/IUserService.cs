using CafeCustomerClient.ViewModels;

namespace CafeCustomerClient.Services
{
    public interface IUserService
    {
        Task<bool> LoginAsync(LoginViewModel loginModel);
        Task<bool> RegisterAsync(RegisterViewModel registerModel);
        Task LogoutAsync();
    }
}
