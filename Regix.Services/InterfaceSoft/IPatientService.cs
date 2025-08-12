using Regix.Domain.EntitiesSoft;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.Services.InterfaceSoft;

public interface IPatientService
{
    Task<ActionResponse<IEnumerable<Patient>>> GetAsync(PaginationDTO pagination, string Email);

    Task<ActionResponse<Patient>> GetAsync(Guid id);

    Task<ActionResponse<Patient>> UpdateAsync(Patient modelo);

    Task<ActionResponse<Patient>> AddAsync(Patient modelo, string Email);

    Task<ActionResponse<bool>> DeleteAsync(Guid id);
}