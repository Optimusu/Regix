using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfacesGen;
using Regix.UnitOfWork.InterfacesGen;

namespace Regix.UnitOfWork.ImplementGen;

public class VeteranUnitOfWork : IVeteranUnitOfWork
{
    private readonly IVeteranService _veteranService;

    public VeteranUnitOfWork(IVeteranService veteranService)
    {
        _veteranService = veteranService;
    }

    public async Task<ActionResponse<IEnumerable<Veteran>>> ComboAsync() => await _veteranService.ComboAsync();

    public async Task<ActionResponse<IEnumerable<Veteran>>> GetAsync(PaginationDTO pagination) => await _veteranService.GetAsync(pagination);

    public async Task<ActionResponse<Veteran>> GetAsync(int id) => await _veteranService.GetAsync(id);

    public async Task<ActionResponse<Veteran>> UpdateAsync(Veteran modelo) => await _veteranService.UpdateAsync(modelo);

    public async Task<ActionResponse<Veteran>> AddAsync(Veteran modelo) => await _veteranService.AddAsync(modelo);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _veteranService.DeleteAsync(id);
}