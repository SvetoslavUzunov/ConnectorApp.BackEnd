using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static Domain.Common.Constants.UserConstants;

namespace Domain.Entities;

public class UserEntity : IdentityUser<Guid>
{
    [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
    public string? FirstName { get; set; }

    [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
    public string? LastName { get; set; }

    [EmailAddress]
    public override string Email { get; set; }

    public bool IsActive { get; set; } = true;

    public virtual ICollection<UserRoleEntity> UserRoles { get; set; } = new HashSet<UserRoleEntity>();
}
