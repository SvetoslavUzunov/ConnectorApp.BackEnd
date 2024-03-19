using Infrastructure.Repositories;

namespace Infrastructure.Patterns;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDBContext data;

    public UnitOfWork(AppDBContext data, IUserRepository users)
    {
        this.data = data;
        Users = users;
    }

    public IUserRepository Users { get; }

    public async Task<int> CompleteAsync()
       => await data.SaveChangesAsync();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            data.Dispose();
        }
    }
}
