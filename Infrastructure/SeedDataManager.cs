using Domain.Common.Constants;
using Domain.Common.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure;

public static class SeedDataManager
{
    public static async Task<IHost> SeedDataAsync(this IHost host)
    {
        Guid UserAdminId = Guid.NewGuid();

        using (IServiceScope? scope = host.Services.CreateScope())
        {
            try
            {
                using AppDBContext? data = scope.ServiceProvider.GetRequiredService<AppDBContext>();

                RoleManager<RoleEntity> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();

                UserManager<UserEntity> userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

                await data.Database.MigrateAsync();

                await SeedRolesAsync(roleManager);
                await SeedUsersAsync(userManager, UserAdminId);
                await AddUsersToRolesAsync(userManager, UserAdminId);

                await data.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        return host;
    }

    private static async Task SeedRolesAsync(RoleManager<RoleEntity> roleManager)
    {
        var adminRole = RoleConstants.AdminRole;

        var isAdminRoleExist = await roleManager.FindByNameAsync(adminRole);
        if (isAdminRoleExist == null)
        {
            var role = new RoleEntity()
            {
                Name = adminRole
            };

            var isRoleCreated = await roleManager.CreateAsync(role);

            if (!isRoleCreated.Succeeded)
            {
                ErrorHandler.ExecuteErrorHandler(isRoleCreated);
            }
        }
    }

    private static async Task SeedUsersAsync(UserManager<UserEntity> userManager, Guid userAdminId)
    {
        var userAdminEmail = UserConstants.AdminUserEmail;
        var isAdminUserExist = await userManager.FindByEmailAsync(userAdminEmail);

        if (isAdminUserExist == null)
        {
            var user = new UserEntity()
            {
                Id = userAdminId,
                UserName = UserConstants.AdminUserName,
                Email = userAdminEmail
            };

            var isUserCreated = await userManager.CreateAsync(user, UserConstants.AdminUserPassword);

            if (!isUserCreated.Succeeded)
            {
                ErrorHandler.ExecuteErrorHandler(isUserCreated);
            }
        }
    }

    private static async Task AddUsersToRolesAsync(UserManager<UserEntity> userManager, Guid userAdminId)
    {
        var adminRole = RoleConstants.AdminRole;
        var adminUser = await userManager.FindByIdAsync(userAdminId.ToString());

        if (adminUser != null)
        {
            var isUserIsInRole = await userManager.IsInRoleAsync(adminUser, adminRole);

            if (isUserIsInRole)
            {
                throw new SeedDataException();
            }

            var isUserAddedToRole = await userManager.AddToRoleAsync(adminUser, adminRole);

            if (!isUserAddedToRole.Succeeded)
            {
                ErrorHandler.ExecuteErrorHandler(isUserAddedToRole);
            }
        }
    }
}
