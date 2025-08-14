using Regix.Domain.Entities;
using Regix.Domain.Resources;
using System.ComponentModel.DataAnnotations;

namespace Regix.Domain.EntitiesSoft;

public class Patient2
{
    [Key]
    public Guid Patient2Id { get; set; }

    [Required]
    [Display(Name = nameof(Resource.Patient), ResourceType = typeof(Resource))]
    public Guid PatientId { get; set; }

    [MaxLength(256, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [DataType(DataType.MultilineText)]
    [Display(Name = "Reason Visit")]
    public string? ConsultationReason { get; set; }

    [Required]
    [Display(Name = nameof(Resource.DateStart), ResourceType = typeof(Resource))]
    public DateTime DateStart { get; set; }

    [MaxLength(256, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [DataType(DataType.MultilineText)]
    [Display(Name = "Symptom Location")]
    public string? SymptomLocation { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Pain Quality")]
    public string PainQuality { get; set; } = null!;


    [Range(0, 10, ErrorMessageResourceName = nameof(Resource.Validation_Combo), ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Severity 0-10")]
    public int PainSeverity { get; set; }

    [MaxLength(100, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Duration/Pattern")]
    public string DurationPattern { get; set; } = null!;

    [MaxLength(100, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Modifying Factors")]
    public string ModifyingFactors { get; set; } = null!;

    [MaxLength(256, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [DataType(DataType.MultilineText)]
    [Display(Name = "Related Symptoms")]
    public string? RelatedSymptoms { get; set; }

    [Display(Name = "Prior Episodes")]
    public bool PriorEpisodes { get; set; }

    //Antecedentes
    [Display(Name = "Hypertension")]
    public bool Hypertension { get; set; }

    [Display(Name = "Diabetes")]
    public bool Diabetes { get; set; }

    [Display(Name = "Hyperlipidemia")]
    public bool Hyperlipidemia { get; set; }

    [Display(Name = "Asthma/COPD")]
    public bool Asthma { get; set; }

    [Display(Name = "Cancer")]
    public bool Cancer { get; set; }

    [Display(Name = "Heart Disease")]
    public bool Heartdisease { get; set; }

    [Display(Name = "Kidney Disease")]
    public bool Kidneydisease { get; set; }

    [Display(Name = "Liver Disease")]
    public bool LiverDisease { get; set; }

    [Display(Name = "Thyroid Disorder")]
    public bool ThyroidDisorder { get; set; }

    [Display(Name = "Autoimmune disease")]
    public bool AutoimmuneDisease { get; set; }

    [Display(Name = "Coagulation Disorder")]
    public bool CoagulationDisorder { get; set; }

    [Display(Name = "Infectious Diseases")]
    public bool InfectiousDiseases { get; set; }

    [MaxLength(100, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Type Cancer")]
    public string TypeCancer { get; set; } = null!;

    [MaxLength(100, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Other")]
    public string OtherPersonalMedical { get; set; } = null!;

    [Display(Name = "Tobacco")]
    public bool Tobacco { get; set; }

    [Display(Name = "Alcohol")]
    public bool Alcohol { get; set; }

    [Display(Name = "Recreational Drugs")]
    public bool RecreationalDrugs { get; set; }

    [Display(Name = "Caffeine")]
    public bool Caffeine { get; set; }

    [Display(Name = "Physical Activity")]
    public bool PhysicalActivity { get; set; }

    [Display(Name = "Dietary Pattern")]
    public bool DietaryPattern { get; set; }

    [Display(Name = "Sleep")]
    public bool Sleep { get; set; }

    [Display(Name = "Occupational Hazards")]
    public bool OccupationalHazards { get; set; }

    [Display(Name = "Housing Situation")]
    public bool HousingSituation { get; set; }

    //Relaciones

    public int CorporationId { get; set; }
    public Corporation? Corporation { get; set; }

    public Patient? Patient { get; set; }
}