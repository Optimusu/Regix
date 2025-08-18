using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfacesGen;
using Regix.UnitOfWork.InterfacesGen;

namespace Regix.UnitOfWork.ImplementGen;

public class DiscapacidadUnitOfWork : IDiscapacidadUnitOfWork
{
    private readonly IDiscapacidadService _discapacidadService;

    public DiscapacidadUnitOfWork(IDiscapacidadService discapacidadService)
    {
        _discapacidadService = discapacidadService;
    }

    public async Task<ActionResponse<IEnumerable<Discapacidad>>> ComboAsync() => await _discapacidadService.ComboAsync();

    public async Task<ActionResponse<IEnumerable<Discapacidad>>> GetAsync(PaginationDTO pagination) => await _discapacidadService.GetAsync(pagination);

    public async Task<ActionResponse<Discapacidad>> GetAsync(int id) => await _discapacidadService.GetAsync(id);

    public async Task<ActionResponse<Discapacidad>> UpdateAsync(Discapacidad modelo) => await _discapacidadService.UpdateAsync(modelo);

    public async Task<ActionResponse<Discapacidad>> AddAsync(Discapacidad modelo) => await _discapacidadService.AddAsync(modelo);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _discapacidadService.DeleteAsync(id);
}