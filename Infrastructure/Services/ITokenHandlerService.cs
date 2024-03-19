using Domain.Entities;
using Domain.Models;

namespace Infrastructure.Services;

public interface ITokenHandlerService
{
    public Task<TokenModel> GenerateToken(UserEntity user);

    public Task<IList<string>> GetUserRoles(UserEntity user);

    public string ValidateRefreshToken(string refreshToken);
}
