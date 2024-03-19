using Domain.Common.Constants;
using Domain.Models.User;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Role;

public class RoleModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(RoleConstants.NameMaxLength, MinimumLength = RoleConstants.NameMinLength)]
    public string Name { get; set; }

    public virtual ICollection<UserModel>? Users { get; set; } = new HashSet<UserModel>();
}
