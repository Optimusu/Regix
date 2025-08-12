using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfacesGen;
using Regix.UnitOfWork.InterfacesGen;

namespace Regix.UnitOfWork.ImplementGen;

public class EstadoCivilUnitOfWork : IEstadoCivilUnitOfWork
{
    private readonly IEstadoCivilService _estadoCivilService;

    public EstadoCivilUnitOfWork(IEstadoCivilService estadoCivilService)
    {
        _estadoCivilService = estadoCivilService;
    }

    public async Task<ActionResponse<IEnumerable<EstadoCivil>>> ComboAsync() => await _estadoCivilService.ComboAsync();

    public async Task<ActionResponse<IEnumerable<EstadoCivil>>> GetAsync(PaginationDTO pagination) => await _estadoCivilService.GetAsync(pagination);

    public async Task<ActionResponse<EstadoCivil>> GetAsync(int id) => await _estadoCivilService.GetAsync(id);

    public async Task<ActionResponse<EstadoCivil>> UpdateAsync(EstadoCivil modelo) => await _estadoCivilService.UpdateAsync(modelo);

    public async Task<ActionResponse<EstadoCivil>> AddAsync(EstadoCivil modelo) => await _estadoCivilService.AddAsync(modelo);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _estadoCivilService.DeleteAsync(id);
}