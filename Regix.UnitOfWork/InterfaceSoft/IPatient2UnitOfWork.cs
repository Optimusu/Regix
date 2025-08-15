using Regix.Domain.EntitiesSoft;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.UnitOfWork.InterfaceSoft;

public interface IPatient2UnitOfWork
{
    Task<ActionResponse<IEnumerable<Patient2>>> GetAsync(PaginationDTO pagination, string username);

    Task<ActionResponse<Patient2>> GetAsync(Guid id);

    Task<ActionResponse<Patient2>> UpdateAsync(Patient2 modelo);

    Task<ActionResponse<Patient2>> AddAsync(Patient2 modelo, string username);

    Task<ActionResponse<bool>> DeleteAsync(Guid id);
}