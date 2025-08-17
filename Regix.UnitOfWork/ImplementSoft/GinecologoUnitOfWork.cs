using Regix.Domain.EntitiesSoft;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfaceSoft;
using Regix.UnitOfWork.InterfaceSoft;

namespace Regix.UnitOfWork.ImplementSoft;

public class GinecologoUnitOfWork : IGinecologoUnitOfWork
{
    private readonly IGinecologoService _ginecologo;

    public GinecologoUnitOfWork(IGinecologoService ginecologo)
    {
        _ginecologo = ginecologo;
    }

    public async Task<ActionResponse<IEnumerable<Ginecologico>>> GetAsync(PaginationDTO pagination, string username) => await _ginecologo.GetAsync(pagination, username);

    public async Task<ActionResponse<Ginecologico>> GetAsync(Guid id) => await _ginecologo.GetAsync(id);

    public async Task<ActionResponse<Ginecologico>> UpdateAsync(Ginecologico modelo) => await _ginecologo.UpdateAsync(modelo);

    public async Task<ActionResponse<Ginecologico>> AddAsync(Ginecologico modelo, string username) => await _ginecologo.AddAsync(modelo, username);

    public async Task<ActionResponse<bool>> DeleteAsync(Guid id) => await _ginecologo.DeleteAsync(id);
}