using Regix.Domain.Entities;
using Regix.Domain.Resources;
using System.ComponentModel.DataAnnotations;

namespace Regix.Domain.EntitiesSoft;

public class PatientControl
{
    [Key]
    public Guid PatientControlId { get; set; }

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Frist Name")]
    public string FirstName { get; set; } = null!;

    [MaxLength(50, ErrorMessageResourceName = "Validation_MaxLength", ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resource))]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;

    [Required]
    [Display(Name = "DBO")]
    public DateTime DOB { get; set; }

    [Display(Name = "User ID")]
    public string UserName { get; set; } = null!;

    public int TPatient => Patients == null ? 0 : Patients.Count();
    public int TPatient2 => Patient2s == null ? 0 : Patient2s.Count();
    public int TGinecologico => Ginecologicos == null ? 0 : Ginecologicos.Count();

    //Relaciones

    public int CorporationId { get; set; }
    public Corporation? Corporation { get; set; }

    //Relaciones Virtuales

    public ICollection<Patient>? Patients { get; set; }
    public ICollection<Patient2>? Patient2s { get; set; }
    public ICollection<Ginecologico>? Ginecologicos { get; set; }
}