using Regix.Domain.EntitiesSoft;
using Regix.Domain.Resources;
using System.ComponentModel.DataAnnotations;

namespace Regix.Domain.EntitiesGen;

public class Discapacidad
{
    [Key]
    public int DiscapacidadId { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Discapacidad")]
    public string Name { get; set; } = null!;

    [Display(Name = nameof(Resource.Active), ResourceType = typeof(Resource))]
    public bool Active { get; set; }

    public ICollection<Patient3>? Patient3s { get; set; }
}