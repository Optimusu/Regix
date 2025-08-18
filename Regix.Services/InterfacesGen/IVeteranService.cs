using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.Services.InterfacesGen;

public interface IVeteranService
{
    Task<ActionResponse<IEnumerable<Veteran>>> ComboAsync();

    Task<ActionResponse<IEnumerable<Veteran>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Veteran>> GetAsync(int id);

    Task<ActionResponse<Veteran>> UpdateAsync(Veteran modelo);

    Task<ActionResponse<Veteran>> AddAsync(Veteran modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}