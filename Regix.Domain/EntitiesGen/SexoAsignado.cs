using Regix.Domain.Resources;
using System.ComponentModel.DataAnnotations;

namespace Regix.Domain.EntitiesGen;

public class SexoAsignado
{
    [Key]
    public int SexoAsignadoId { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.BirthSex), ResourceType = typeof(Resource))]
    public string Name { get; set; } = null!;

    [Display(Name = nameof(Resource.Active), ResourceType = typeof(Resource))]
    public bool Active { get; set; }
}