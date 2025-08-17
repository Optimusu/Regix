using Regix.Domain.EntitiesSoft;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.Services.InterfaceSoft;

public interface IGinecologoService
{
    Task<ActionResponse<IEnumerable<Ginecologico>>> GetAsync(PaginationDTO pagination, string username);

    Task<ActionResponse<Ginecologico>> GetAsync(Guid id);

    Task<ActionResponse<Ginecologico>> UpdateAsync(Ginecologico modelo);

    Task<ActionResponse<Ginecologico>> AddAsync(Ginecologico modelo, string username);

    Task<ActionResponse<bool>> DeleteAsync(Guid id);
}