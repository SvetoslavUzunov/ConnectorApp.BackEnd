using Infrastructure.Patterns;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Repositories;

public static class RepositoryInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<AppDBContext>(options => options
                .UseSqlServer(new DBConnectionString().GetDbConnectionString));

        return services;
    }
}
