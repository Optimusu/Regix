using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.UnitOfWork.InterfacesGen;

public interface IPharmacyUnitOfWork
{
    Task<ActionResponse<IEnumerable<Pharmacy>>> ComboAsync();

    Task<ActionResponse<IEnumerable<Pharmacy>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Pharmacy>> GetAsync(int id);

    Task<ActionResponse<Pharmacy>> UpdateAsync(Pharmacy modelo);

    Task<ActionResponse<Pharmacy>> AddAsync(Pharmacy modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}