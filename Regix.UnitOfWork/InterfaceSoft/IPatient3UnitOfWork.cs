using Regix.Domain.EntitiesSoft;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.UnitOfWork.InterfaceSoft;

public interface IPatient3UnitOfWork
{
    Task<ActionResponse<IEnumerable<Patient3>>> GetAsync(PaginationDTO pagination, string username);

    Task<ActionResponse<Patient3>> GetAsync(Guid id);

    Task<ActionResponse<Patient3>> UpdateAsync(Patient3 modelo);

    Task<ActionResponse<Patient3>> AddAsync(Patient3 modelo, string username);

    Task<ActionResponse<bool>> DeleteAsync(Guid id);
}