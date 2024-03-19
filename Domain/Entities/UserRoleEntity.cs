namespace Domain.Entities;

public class UserRoleEntity
{
    public Guid UserId { get; set; } = Guid.NewGuid();

    public virtual UserEntity User { get; set; }

    public Guid RoleId { get; set; } = Guid.NewGuid();

    public virtual RoleEntity Role { get; set; }
}
