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
    [Display(Name = nameof(Resource.FirstName), ResourceType = typeof(Resource))]
    public string FirstName { get; set; } = null!;

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.LastName), ResourceType = typeof(Resource))]
    public string LastName { get; set; } = null!;

    [MaxLength(101, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.FullName), ResourceType = typeof(Resource))]
    public string? FullName { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Prefername), ResourceType = typeof(Resource))]
    public string? Preferido { get; set; }

    [Required]
    [Display(Name = nameof(Resource.TypeDocument), ResourceType = typeof(Resource))]
    public int SexoAsignadoId { get; set; }

    [Required]
    [Display(Name = nameof(Resource.TypeDocument), ResourceType = typeof(Resource))]
    public int IdentidadGeneroId { get; set; }

    [Display(Name = nameof(Resource.DOB), ResourceType = typeof(Resource))]
    public DateTime? DOB { get; set; }

    [Required]
    [Display(Name = nameof(Resource.TypeDocument), ResourceType = typeof(Resource))]
    public int DocumentTypeId { get; set; }

    [MaxLength(20, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Document), ResourceType = typeof(Resource))]
    public string Nro_Document { get; set; } = null!;

    [MaxLength(256, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [DataType(DataType.MultilineText)]
    [Display(Name = nameof(Resource.Address), ResourceType = typeof(Resource))]
    public string? Address { get; set; }

    [MaxLength(25, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Phone), ResourceType = typeof(Resource))]
    public string? PhoneCell { get; set; }

    [MaxLength(25, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Phone), ResourceType = typeof(Resource))]
    public string? PhoneHome { get; set; }

    [MaxLength(256, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [DataType(DataType.EmailAddress)]
    [Display(Name = nameof(Resource.Email), ResourceType = typeof(Resource))]
    public string UserName { get; set; } = null!;

    [Required]
    [Display(Name = nameof(Resource.Idioma), ResourceType = typeof(Resource))]
    public int IdiomaId { get; set; }

    [Display(Name = nameof(Resource.Interpreter), ResourceType = typeof(Resource))]
    public bool Interpreter { get; set; }

    [Required]
    [Display(Name = nameof(Resource.MaritalStatus), ResourceType = typeof(Resource))]
    public int EstadoCivilId { get; set; }

    [MaxLength(25, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = nameof(Resource.Occupation), ResourceType = typeof(Resource))]
    public string? Ocupacion { get; set; }

    [Display(Name = nameof(Resource.Active), ResourceType = typeof(Resource))]
    public bool Active { get; set; }

    [Display(Name = nameof(Resource.Active), ResourceType = typeof(Resource))]
    public bool Confirmed { get; set; }

    [Display(Name = nameof(Resource.Active), ResourceType = typeof(Resource))]
    public bool Migrated { get; set; }

    //Relaciones

    public int CorporationId { get; set; }

    public Corporation? Corporation { get; set; }
    public SexoAsignado? SexoAsignado { get; set; }
    public IdentidadGenero? IdentidadGenero { get; set; }
    public DocumentType? DocumentType { get; set; }
    public Idioma? Idioma { get; set; }
    public EstadoCivil? EstadoCivil { get; set; }
}