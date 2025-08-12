using System.ComponentModel.DataAnnotations;
using Regix.Domain.Enum;
using Regix.Domain.Resources;

namespace Regix.Domain.Entities;

public class UserRoleDetails
{
    [Key]
    public int UserRoleDetailsId { get; set; }

    [Display(Name = nameof(Resource.RoleUser), ResourceType = typeof(Resource))]
    public UserType? UserType { get; set; }

    [Display(Name = nameof(Resource.User), ResourceType = typeof(Resource))]
    public string? UserId { get; set; }

    //Relacion
    public User? User { get; set; }
}