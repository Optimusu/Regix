using Regix.Domain.EntitiesSoft;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.Services.InterfaceSoft;

public interface IPatient2Service
{
    Task<ActionResponse<IEnumerable<Patient2>>> GetAsync(PaginationDTO pagination, string Email);

    Task<ActionResponse<Patient2>> GetAsync(Guid id);

    Task<ActionResponse<Patient2>> UpdateAsync(Patient2 modelo);

    Task<ActionResponse<Patient2>> AddAsync(Patient2 modelo, string Email);

    Task<ActionResponse<bool>> DeleteAsync(Guid id);
}