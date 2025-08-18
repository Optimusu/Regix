using Regix.Domain.EntitiesSoft;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfaceSoft;
using Regix.UnitOfWork.InterfaceSoft;

namespace Regix.UnitOfWork.ImplementSoft;

public class Patient3UnitOfWork : IPatient3UnitOfWork
{
    private readonly IPatient3Service _patientService;

    public Patient3UnitOfWork(IPatient3Service patientService)
    {
        _patientService = patientService;
    }

    public async Task<ActionResponse<IEnumerable<Patient3>>> GetAsync(PaginationDTO pagination, string username) => await _patientService.GetAsync(pagination, username);

    public async Task<ActionResponse<Patient3>> GetAsync(Guid id) => await _patientService.GetAsync(id);

    public async Task<ActionResponse<Patient3>> UpdateAsync(Patient3 modelo) => await _patientService.UpdateAsync(modelo);

    public async Task<ActionResponse<Patient3>> AddAsync(Patient3 modelo, string username) => await _patientService.AddAsync(modelo, username);

    public async Task<ActionResponse<bool>> DeleteAsync(Guid id) => await _patientService.DeleteAsync(id);
}