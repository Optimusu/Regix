using Regix.Domain.Entities;
using Regix.Domain.EntitiesGen;
using Regix.Domain.Resources;
using System.ComponentModel.DataAnnotations;

namespace Regix.Domain.EntitiesSoft;

public class Patient3
{
    [Key]
    public Guid Patient3Id { get; set; }

    [Required]
    [Display(Name = nameof(Resource.Patient), ResourceType = typeof(Resource))]
    public Guid PatientControlId { get; set; }

    //Para Detalla la Salud mental
    [Display(Name = "Mental Health")]
    public bool MentalHealth { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Mental Details")]
    public string? MentalDetail { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Current Therapy")]
    public string? CurrentTherapy { get; set; }

    [Display(Name = "Suicidal Ideation")]
    public bool SuicidalIdeation { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Ideation Details")]
    public string? IdeationDetails { get; set; }

    [Display(Name = "PHQ-9")]
    public bool Phq9 { get; set; }

    [Display(Name = "GAD-7")]
    public bool Gad7 { get; set; }

    [Display(Name = "C-SSRS")]
    public bool Cssrs { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Test Details")]
    public string? TestDetails { get; set; }

    //Estado Funcionaly Calidad de Vida

    [Display(Name = "Mobility Aid")]
    public bool MobilityAid { get; set; }

    [Display(Name = "Daily  Aid")]
    public bool DailyAid { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Mobility Details")]
    public string? MobilityDetails { get; set; }

    [Range(0, 10, ErrorMessageResourceName = nameof(Resource.Validation_Combo), ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Paint 0-10")]
    public int Pain { get; set; }

    //Etnia y Raza

    [Required]
    [Range(1, double.MaxValue, ErrorMessageResourceName = nameof(Resource.Validation_Combo), ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Ethnic/Race")]
    public int EtniaRazaId { get; set; }

    [Required]
    [Range(1, double.MaxValue, ErrorMessageResourceName = nameof(Resource.Validation_Combo), ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Disability")]
    public int DiscapacidadId { get; set; }

    [Required]
    [Range(1, double.MaxValue, ErrorMessageResourceName = nameof(Resource.Validation_Combo), ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Veteran")]
    public int VeteranId { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Recent Travel")]
    public string? RecentTravel { get; set; }

    [MaxLength(100, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Exposure Contagious Disease")]
    public string? ExposureContagious { get; set; }

    //Relaciones

    public int CorporationId { get; set; }
    public Corporation? Corporation { get; set; }

    public PatientControl? PatientControl { get; set; }

    public EtniaRaza? EtniaRaza { get; set; }

    public Discapacidad? Discapacidad { get; set; }

    public Veteran? Veteran { get; set; }
}