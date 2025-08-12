using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfacesGen;
using Regix.UnitOfWork.InterfacesGen;

namespace Regix.UnitOfWork.ImplementGen;

public class SexoAsignadoUnitOfWork : ISexoAsignadoUnitOfWork
{
    private readonly ISexoAsignadoService _sexoAsignado;

    public SexoAsignadoUnitOfWork(ISexoAsignadoService sexoAsignado)
    {
        _sexoAsignado = sexoAsignado;
    }

    public async Task<ActionResponse<IEnumerable<SexoAsignado>>> ComboAsync() => await _sexoAsignado.ComboAsync();

    public async Task<ActionResponse<IEnumerable<SexoAsignado>>> GetAsync(PaginationDTO pagination) => await _sexoAsignado.GetAsync(pagination);

    public async Task<ActionResponse<SexoAsignado>> GetAsync(int id) => await _sexoAsignado.GetAsync(id);

    public async Task<ActionResponse<SexoAsignado>> UpdateAsync(SexoAsignado modelo) => await _sexoAsignado.UpdateAsync(modelo);

    public async Task<ActionResponse<SexoAsignado>> AddAsync(SexoAsignado modelo) => await _sexoAsignado.AddAsync(modelo);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _sexoAsignado.DeleteAsync(id);
}