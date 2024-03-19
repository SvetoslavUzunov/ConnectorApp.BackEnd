using Domain.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.User;

public class UserModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(UserConstants.UserNameMaxLength, MinimumLength = UserConstants.UserNameMinLength)]
    public string? UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public bool IsActive { get; set; } = true;
}
