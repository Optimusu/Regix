using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfacesGen;
using Regix.UnitOfWork.InterfacesGen;

namespace Regix.UnitOfWork.ImplementGen;

public class EtniaRazaUnitOfWork : IEtniaRazaUnitOfWork
{
    private readonly IEtniaRazaService _etniaRaza;

    public EtniaRazaUnitOfWork(IEtniaRazaService etniaRaza)
    {
        _etniaRaza = etniaRaza;
    }

    public async Task<ActionResponse<IEnumerable<EtniaRaza>>> ComboAsync() => await _etniaRaza.ComboAsync();

    public async Task<ActionResponse<IEnumerable<EtniaRaza>>> GetAsync(PaginationDTO pagination) => await _etniaRaza.GetAsync(pagination);

    public async Task<ActionResponse<EtniaRaza>> GetAsync(int id) => await _etniaRaza.GetAsync(id);

    public async Task<ActionResponse<EtniaRaza>> UpdateAsync(EtniaRaza modelo) => await _etniaRaza.UpdateAsync(modelo);

    public async Task<ActionResponse<EtniaRaza>> AddAsync(EtniaRaza modelo) => await _etniaRaza.AddAsync(modelo);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _etniaRaza.DeleteAsync(id);
}