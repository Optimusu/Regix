using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.Services.InterfacesGen;

public interface IIdentidadGeneroService
{
    Task<ActionResponse<IEnumerable<IdentidadGenero>>> ComboAsync();

    Task<ActionResponse<IEnumerable<IdentidadGenero>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<IdentidadGenero>> GetAsync(int id);

    Task<ActionResponse<IdentidadGenero>> UpdateAsync(IdentidadGenero modelo);

    Task<ActionResponse<IdentidadGenero>> AddAsync(IdentidadGenero modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}