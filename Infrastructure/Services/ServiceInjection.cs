using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services;

public static class ServiceInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        RepositoryInjection.AddRepositories(services);

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenHandlerService, TokenHandlerService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}
