using Regix.Domain.EntitiesSoft;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfaceSoft;
using Regix.UnitOfWork.InterfaceSoft;

namespace Regix.UnitOfWork.ImplementSoft;

public class PatientUnitOfWork : IPatientUnitOfWork
{
    private readonly IPatientService _patientService;

    public PatientUnitOfWork(IPatientService patientService)
    {
        _patientService = patientService;
    }

    public async Task<ActionResponse<IEnumerable<Patient>>> GetAsync(PaginationDTO pagination, string Email) => await _patientService.GetAsync(pagination, Email);

    public async Task<ActionResponse<Patient>> GetAsync(Guid id) => await _patientService.GetAsync(id);

    public async Task<ActionResponse<Patient>> UpdateAsync(Patient modelo) => await _patientService.UpdateAsync(modelo);

    public async Task<ActionResponse<Patient>> AddAsync(Patient modelo, string Email) => await _patientService.AddAsync(modelo, Email);

    public async Task<ActionResponse<bool>> DeleteAsync(Guid id) => await _patientService.DeleteAsync(id);
}