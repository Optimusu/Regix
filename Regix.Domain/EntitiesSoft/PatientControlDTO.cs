namespace Regix.Domain.EntitiesSoft;

public class PatientControlDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public int CorporationId { get; set; }
}