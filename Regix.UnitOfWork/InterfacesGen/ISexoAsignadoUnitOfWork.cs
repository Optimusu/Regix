using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.UnitOfWork.InterfacesGen;

public interface ISexoAsignadoUnitOfWork
{
    Task<ActionResponse<IEnumerable<SexoAsignado>>> ComboAsync();

    Task<ActionResponse<IEnumerable<SexoAsignado>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<SexoAsignado>> GetAsync(int id);

    Task<ActionResponse<SexoAsignado>> UpdateAsync(SexoAsignado modelo);

    Task<ActionResponse<SexoAsignado>> AddAsync(SexoAsignado modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}