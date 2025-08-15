using Regix.Domain.EntitiesSoft;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfaceSoft;
using Regix.UnitOfWork.InterfaceSoft;

namespace Regix.UnitOfWork.ImplementSoft;

public class PatientControlUnitOfWork : IPatientControlUnitOfWork
{
    private readonly IPatientControlService _patientService;

    public PatientControlUnitOfWork(IPatientControlService patientService)
    {
        _patientService = patientService;
    }

    public async Task<ActionResponse<PatientControl>> ControlDataAsync(PatientControlDTO modelo, string username) => await _patientService.ControlDataAsync(modelo, username);

    public async Task<ActionResponse<IEnumerable<PatientControl>>> GetAsync(PaginationDTO pagination, string Email) => await _patientService.GetAsync(pagination, Email);

    public async Task<ActionResponse<PatientControl>> GetAsync(Guid id) => await _patientService.GetAsync(id);

    public async Task<ActionResponse<PatientControl>> UpdateAsync(PatientControl modelo) => await _patientService.UpdateAsync(modelo);

    public async Task<ActionResponse<PatientControl>> AddAsync(PatientControl modelo, string Email) => await _patientService.AddAsync(modelo, Email);

    public async Task<ActionResponse<bool>> DeleteAsync(Guid id) => await _patientService.DeleteAsync(id);
}