using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfacesGen;
using Regix.UnitOfWork.InterfacesGen;

namespace Regix.UnitOfWork.ImplementGen;

public class IdiomaUnitOfWork : IIdiomaUnitOfWork
{
    private readonly IIdiomaService _idiomaService;

    public IdiomaUnitOfWork(IIdiomaService idiomaService)
    {
        _idiomaService = idiomaService;
    }

    public async Task<ActionResponse<IEnumerable<Idioma>>> ComboAsync() => await _idiomaService.ComboAsync();

    public async Task<ActionResponse<IEnumerable<Idioma>>> GetAsync(PaginationDTO pagination) => await _idiomaService.GetAsync(pagination);

    public async Task<ActionResponse<Idioma>> GetAsync(int id) => await _idiomaService.GetAsync(id);

    public async Task<ActionResponse<Idioma>> UpdateAsync(Idioma modelo) => await _idiomaService.UpdateAsync(modelo);

    public async Task<ActionResponse<Idioma>> AddAsync(Idioma modelo) => await _idiomaService.AddAsync(modelo);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _idiomaService.DeleteAsync(id);
}