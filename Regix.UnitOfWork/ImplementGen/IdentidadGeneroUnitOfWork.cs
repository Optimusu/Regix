using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfacesGen;
using Regix.UnitOfWork.InterfacesGen;

namespace Regix.UnitOfWork.ImplementGen;

public class IdentidadGeneroUnitOfWork : IIdentidadGeneroUnitOfWork
{
    private readonly IIdentidadGeneroService _identidadGenero;

    public IdentidadGeneroUnitOfWork(IIdentidadGeneroService identidadGenero)
    {
        _identidadGenero = identidadGenero;
    }

    public async Task<ActionResponse<IEnumerable<IdentidadGenero>>> ComboAsync() => await _identidadGenero.ComboAsync();

    public async Task<ActionResponse<IEnumerable<IdentidadGenero>>> GetAsync(PaginationDTO pagination) => await _identidadGenero.GetAsync(pagination);

    public async Task<ActionResponse<IdentidadGenero>> GetAsync(int id) => await _identidadGenero.GetAsync(id);

    public async Task<ActionResponse<IdentidadGenero>> UpdateAsync(IdentidadGenero modelo) => await _identidadGenero.UpdateAsync(modelo);

    public async Task<ActionResponse<IdentidadGenero>> AddAsync(IdentidadGenero modelo) => await _identidadGenero.AddAsync(modelo);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _identidadGenero.DeleteAsync(id);
}