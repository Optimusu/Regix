using Regix.Domain.EntitiesSoft;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.UnitOfWork.InterfaceSoft;

public interface IPatientUnitOfWork
{
    Task<ActionResponse<IEnumerable<Patient>>> GetAsync(PaginationDTO pagination, string username);

    Task<ActionResponse<Patient>> GetAsync(Guid id);

    Task<ActionResponse<Patient>> UpdateAsync(Patient modelo);

    Task<ActionResponse<Patient>> AddAsync(Patient modelo, string username);

    Task<ActionResponse<bool>> DeleteAsync(Guid id);
}