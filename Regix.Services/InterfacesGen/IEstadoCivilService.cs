using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.Services.InterfacesGen;

public interface IEstadoCivilService
{
    Task<ActionResponse<IEnumerable<EstadoCivil>>> ComboAsync();

    Task<ActionResponse<IEnumerable<EstadoCivil>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<EstadoCivil>> GetAsync(int id);

    Task<ActionResponse<EstadoCivil>> UpdateAsync(EstadoCivil modelo);

    Task<ActionResponse<EstadoCivil>> AddAsync(EstadoCivil modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}