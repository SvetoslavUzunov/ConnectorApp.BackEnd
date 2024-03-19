using Domain.Entities;
using Infrastructure.EntitiesConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDBContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    public override DbSet<UserEntity> Users { get; set; }

    public override DbSet<RoleEntity> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserRoleConfiguration());

        base.OnModelCreating(builder);
    }
}
