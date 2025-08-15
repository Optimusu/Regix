using Regix.Domain.Resources;
using System.ComponentModel.DataAnnotations;

namespace Regix.DomainLogic.ResponsesSec;

public class RegisterDTO
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "First Name")]
    public string FirstName
    {
        get => _firstName;
        set => _firstName = CapitalizeFirstLetter(value);
    }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Last Name")]
    public string LastName
    {
        get => _lastName;
        set => _lastName = CapitalizeFirstLetter(value);
    }

    [Required]
    [Display(Name = "Birth")]
    public DateTime DOB { get; set; }

    [StringLength(24, MinimumLength = 6, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resource))]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessageResourceName = "Validation_UserNameFormat", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "User ID")]
    public string UserName { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessageResourceName = nameof(Resource.Validation_Required), ErrorMessageResourceType = typeof(Resource))]
    [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = nameof(Resource.Validation_BetweenLength), ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "New Password")]
    public string NewPassword { get; set; } = null!;

    [Compare("NewPassword", ErrorMessageResourceName = nameof(Resource.Validation_PasswordMismatch), ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = nameof(Resource.Validation_Required), ErrorMessageResourceType = typeof(Resource))]
    [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = nameof(Resource.Validation_BetweenLength), ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Confirm Password")]
    public string Confirm { get; set; } = null!;

    public int CorporationId { get; set; }

    // 🔧 Helper privado para capitalizar
    private static string CapitalizeFirstLetter(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        return char.ToUpper(input[0]) + input.Substring(1);
    }
}