using Domain.Models.User;
using Infrastructure.Services;

namespace Application.GraphQLTypes;

public class QueryType
{
    public async Task<UserModel> GetByIdAsync([Service] IUserService userService, Guid userId)
    {
        return await userService.GetByIdAsync(userId);
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync([Service] IUserService userService)
    {
        return await userService.GetAllAsync();
    }
}
