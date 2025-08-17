using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.UnitOfWork.InterfacesGen;

public interface IEtniaRazaUnitOfWork
{
    Task<ActionResponse<IEnumerable<EtniaRaza>>> ComboAsync();

    Task<ActionResponse<IEnumerable<EtniaRaza>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<EtniaRaza>> GetAsync(int id);

    Task<ActionResponse<EtniaRaza>> UpdateAsync(EtniaRaza modelo);

    Task<ActionResponse<EtniaRaza>> AddAsync(EtniaRaza modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}