using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfacesGen;
using Regix.UnitOfWork.InterfacesGen;

namespace Regix.UnitOfWork.ImplementGen;

public class RegularUnitOfWork : IRegularUnitOfWork
{
    private readonly IRegularService _regularService;

    public RegularUnitOfWork(IRegularService regularService)
    {
        _regularService = regularService;
    }

    public async Task<ActionResponse<IEnumerable<Regular>>> ComboAsync() => await _regularService.ComboAsync();

    public async Task<ActionResponse<IEnumerable<Regular>>> GetAsync(PaginationDTO pagination) => await _regularService.GetAsync(pagination);

    public async Task<ActionResponse<Regular>> GetAsync(int id) => await _regularService.GetAsync(id);

    public async Task<ActionResponse<Regular>> UpdateAsync(Regular modelo) => await _regularService.UpdateAsync(modelo);

    public async Task<ActionResponse<Regular>> AddAsync(Regular modelo) => await _regularService.AddAsync(modelo);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _regularService.DeleteAsync(id);
}