using System.ComponentModel.DataAnnotations;
using Regix.Domain.Resources;

namespace Regix.DomainLogic.ResponsesSec;

public class LoginDTO
{
    [EmailAddress(ErrorMessageResourceName = nameof(Resource.Validation_InvalidEmail), ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = nameof(Resource.Validation_Required), ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Email), ResourceType = typeof(Resource))]
    public string Email { get; set; } = null!;

    [MinLength(6, ErrorMessageResourceName = nameof(Resource.Validation_MinLength), ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = nameof(Resource.Validation_Required), ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.User), ResourceType = typeof(Resource))]
    public string Password { get; set; } = null!;
}