using Regix.Domain.EntitiesSoft;

namespace Regix.AppFront.Helpers;

public class PatientControlStateService
{
    private PatientControl? _current;

    public void Set(PatientControl model) => _current = model;
    public PatientControl? Get() => _current;
    public void Clear() => _current = null;

    public bool IsSet => _current != null;
    public Guid? Id => _current?.PatientControlId;
    public string? FullName => _current == null ? null : $"{_current.FirstName} {_current.LastName}";
    public int TotalPatients => _current?.TPatient ?? 0;

}
