using CustomerClient.ViewModels;

namespace CustomerClient.Services
{
    public interface IUserService
    {
        Task<bool> LoginAsync(LoginViewModel loginModel);
        Task<bool> RegisterAsync(RegisterViewModel registerModel);
        Task LogoutAsync();
    }
}
