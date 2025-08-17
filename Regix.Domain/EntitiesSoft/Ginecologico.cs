using Regix.Domain.Resources;
using System.ComponentModel.DataAnnotations;

namespace Regix.Domain.EntitiesSoft;

public class Ginecologico
{
    [Key]
    public Guid GinecologicoId { get; set; }

    [Required]
    [Display(Name = nameof(Resource.Patient), ResourceType = typeof(Resource))]
    public Guid PatientControlId { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Menarquia")]
    public string Menarquia { get; set; } = null!;

    [Required]
    [Display(Name = "Las Menstruation")]
    public DateTime LastMenstruation { get; set; }
}