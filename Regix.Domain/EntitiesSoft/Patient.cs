using Regix.Domain.Entities;
using Regix.Domain.EntitiesGen;
using Regix.Domain.Resources;
using System.ComponentModel.DataAnnotations;

namespace Regix.Domain.EntitiesSoft;

public class Patient
{
    [Key]
    public Guid PatientId { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Frist Name")]
    public string FirstName { get; set; } = null!;

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;

    [MaxLength(101, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Name Prefer")]
    public string? FullName { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Frist Name")]
    public string? Preferido { get; set; }

    [Required]
    [Display(Name = "Sex Birth")]
    public int SexoAsignadoId { get; set; }

    [Required]
    [Range(1, double.MaxValue, ErrorMessageResourceName = nameof(Resource.Validation_Combo), ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Gender Identity ")]
    public int IdentidadGeneroId { get; set; }

    [Required]
    [Display(Name = "DBO")]
    public DateTime DOB { get; set; }

    [Required]
    [Range(1, double.MaxValue, ErrorMessageResourceName = nameof(Resource.Validation_Combo), ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Typo Document")]
    public int DocumentTypeId { get; set; }

    [MaxLength(20, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Document #")]
    public string Nro_Document { get; set; } = null!;

    [MaxLength(256, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [DataType(DataType.MultilineText)]
    [Display(Name = "Address")]
    public string? Address { get; set; }

    [MaxLength(25, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Phone Cell")]
    public string? PhoneCell { get; set; }

    [MaxLength(25, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Phone Home")]
    public string? PhoneHome { get; set; }

    [MaxLength(256, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string UserName { get; set; } = null!;

    [Required]
    [Display(Name = "Lenguage")]
    public int IdiomaId { get; set; }

    [Display(Name = "Interpreter")]
    public bool Interpreter { get; set; }

    [Required]
    [Display(Name = "Marital Status")]
    public int EstadoCivilId { get; set; }

    [MaxLength(25, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Ocupacion")]
    public string? Ocupacion { get; set; }

    [Required]
    [Display(Name = "Pharmacy")]
    public int PharmacyId { get; set; }

    //EmergencyContact
    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Full Name")]
    public string? EmgName { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Relation")]
    public string? EmgRelation { get; set; }

    [MaxLength(25, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Phone")]
    public string? EmgPhone { get; set; }

    [MaxLength(256, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Address")]
    public string? EmgAddress { get; set; }

    //End Emergency Contact

    [Display(Name = nameof(Resource.Active), ResourceType = typeof(Resource))]
    public bool Active { get; set; }

    [Display(Name = nameof(Resource.Active), ResourceType = typeof(Resource))]
    public bool Confirmed { get; set; }

    [Display(Name = nameof(Resource.Active), ResourceType = typeof(Resource))]
    public bool Migrated { get; set; }

    //Relaciones Virtuales

    public int TotalPatien2 => Patient2s == null ? 0 : Patient2s.Count();

    //Relaciones

    public int CorporationId { get; set; }
    public Corporation? Corporation { get; set; }
    public SexoAsignado? SexoAsignado { get; set; }
    public IdentidadGenero? IdentidadGenero { get; set; }
    public DocumentType? DocumentType { get; set; }
    public Idioma? Idioma { get; set; }
    public EstadoCivil? EstadoCivil { get; set; }
    public Pharmacy? Pharmacy { get; set; }

    public ICollection<Patient2>? Patient2s { get; set; }
}