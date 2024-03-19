using Domain.Models.User;
using Infrastructure.Services;

namespace Application.GraphQLTypes;

public class MutationType
{
    public async Task<UserModel> CreateUserAsync([Service] IUserService userService, UserModel userModel)
    {
        return await userService.CreateAsync(userModel);
    }

    public async Task<UserModel> EditUserAsync([Service] IUserService userService, UserModel userModel)
    {
        return await userService.EditAsync(userModel);
    }

    public async Task DeleteUserAsync([Service] IUserService userService, Guid userId)
    {
        await userService.DeleteByIdAsync(userId);
    }
}
