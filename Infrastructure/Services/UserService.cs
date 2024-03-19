using Domain.Common.Exceptions;
using Domain.Entities;
using Domain.Models.User;
using Infrastructure.Patterns;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<UserModel> GetByIdAsync(Guid id)
    {
        var user = await userRepository.GetByIdAsync(id);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        return new UserModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        var users = await userRepository.GetAllAsync();

        if (!users.Any())
        {
            throw new EmptyCollectionException();
        }

        return users.Where(user => user.IsActive)
           .Select(u => new UserModel
           {
               Id = u.Id,
               UserName = u.UserName,
               Email = u.Email,
               IsActive = u.IsActive
           })
           .ToList();
    }

    public async Task<UserModel> CreateAsync(UserModel userModel)
    {
        var user = await userRepository.GetByIdAsync(userModel.Id);

        if (user != null)
        {
            throw new UserAlreadyExistException();
        }

        user = new UserEntity
        {
            UserName = userModel.UserName,
            Email = userModel.Email
        };

        await userRepository.CreateAsync(user);
        await unitOfWork.CompleteAsync();

        return userModel;
    }

    public async Task<UserModel> EditAsync(UserModel userModel)
    {
        var user = await userRepository.GetByIdAsync(userModel.Id);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        user.Id = userModel.Id;
        user.UserName = userModel.UserName;
        user.Email = userModel.Email;
        user.IsActive = userModel.IsActive;

        userRepository.Edit(user);
        await unitOfWork.CompleteAsync();

        return userModel;
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var user = await userRepository.GetByIdAsync(id);

        if (user == null || !user.IsActive)
        {
            throw new UserNotFoundException();
        }

        user.IsActive = false;
        await unitOfWork.CompleteAsync();
    }
}
