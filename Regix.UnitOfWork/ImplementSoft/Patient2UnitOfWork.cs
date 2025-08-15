using Regix.Domain.EntitiesSoft;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfaceSoft;
using Regix.UnitOfWork.InterfaceSoft;

namespace Regix.UnitOfWork.ImplementSoft;

public class Patient2UnitOfWork : IPatient2UnitOfWork
{
    private readonly IPatient2Service _patientService;

    public Patient2UnitOfWork(IPatient2Service patientService)
    {
        _patientService = patientService;
    }

    public async Task<ActionResponse<IEnumerable<Patient2>>> GetAsync(PaginationDTO pagination, string username) => await _patientService.GetAsync(pagination, username);

    public async Task<ActionResponse<Patient2>> GetAsync(Guid id) => await _patientService.GetAsync(id);

    public async Task<ActionResponse<Patient2>> UpdateAsync(Patient2 modelo) => await _patientService.UpdateAsync(modelo);

    public async Task<ActionResponse<Patient2>> AddAsync(Patient2 modelo, string username) => await _patientService.AddAsync(modelo, username);

    public async Task<ActionResponse<bool>> DeleteAsync(Guid id) => await _patientService.DeleteAsync(id);
}