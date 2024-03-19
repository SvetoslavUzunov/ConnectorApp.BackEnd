using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class RoleEntity : IdentityRole<Guid>
{
    public virtual ICollection<UserRoleEntity> UserRoles { get; set; } = new HashSet<UserRoleEntity>();
}
