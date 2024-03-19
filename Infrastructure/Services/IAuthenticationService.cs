using Domain.Models;
using Domain.Models.User;

namespace Infrastructure.Services;

public interface IAuthenticationService
{
    public Task<UserLoginModel> RegisterAsync(UserRegistrationModel userModel);

    public Task<TokenModel> LoginAsync(UserLoginModel userModel);

    public string Logout(string refreshToken);

    public Task<TokenModel> RefreshTokenAsync(string refreshToken);
}
