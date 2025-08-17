using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.Services.InterfacesGen;

public interface IRegularService
{
    Task<ActionResponse<IEnumerable<Regular>>> ComboAsync();

    Task<ActionResponse<IEnumerable<Regular>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Regular>> GetAsync(int id);

    Task<ActionResponse<Regular>> UpdateAsync(Regular modelo);

    Task<ActionResponse<Regular>> AddAsync(Regular modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}