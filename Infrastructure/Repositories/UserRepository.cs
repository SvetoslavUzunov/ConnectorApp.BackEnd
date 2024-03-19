using Domain.Entities;

namespace Infrastructure.Repositories;

public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
    public UserRepository(AppDBContext data) : base(data) { }
}
