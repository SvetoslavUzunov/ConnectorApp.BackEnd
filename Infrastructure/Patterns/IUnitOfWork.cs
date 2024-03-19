using Infrastructure.Repositories;

namespace Infrastructure.Patterns;

public interface IUnitOfWork : IDisposable
{
    public IUserRepository Users { get; }

    public Task<int> CompleteAsync();
}
