using Regix.Domain.Entities;
using Regix.Domain.EntitiesGen;
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
    [Display(Name = "Last Menstruation")]
    public DateTime? LastMenstruation { get; set; }

    [Required]
    [Range(1, double.MaxValue, ErrorMessageResourceName = nameof(Resource.Validation_Combo), ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Cycle Regularity")]
    public int RegularId { get; set; }

    [Required]
    [Range(1, double.MaxValue, ErrorMessageResourceName = nameof(Resource.Validation_Combo), ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Anticonception")]
    public int AnticonceptionId { get; set; }

    [Range(0, 10, ErrorMessageResourceName = nameof(Resource.Validation_Combo), ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Gravida")]
    public int Gravida { get; set; }  //Gestaciones

    [Display(Name = "Labor Complications")]
    public bool LaborComplications { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Explain")]
    public string WhyComplication { get; set; } = null!;

    [Required]
    [Display(Name = "Last Cytology/Pap")]
    public DateTime? LastCytologyPap { get; set; }

    [Display(Name = "Menopause")]
    public bool Menopause { get; set; }

    //Relaciones

    public int CorporationId { get; set; }
    public Corporation? Corporation { get; set; }
    public PatientControl? PatientControl { get; set; }
    public Regular? Regular { get; set; }
    public Anticonception? Anticonception { get; set; }
}