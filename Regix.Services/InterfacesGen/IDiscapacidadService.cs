using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.Services.InterfacesGen;

public interface IDiscapacidadService
{
    Task<ActionResponse<IEnumerable<Discapacidad>>> ComboAsync();

    Task<ActionResponse<IEnumerable<Discapacidad>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Discapacidad>> GetAsync(int id);

    Task<ActionResponse<Discapacidad>> UpdateAsync(Discapacidad modelo);

    Task<ActionResponse<Discapacidad>> AddAsync(Discapacidad modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}