using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfacesGen;
using Regix.UnitOfWork.InterfacesGen;

namespace Regix.UnitOfWork.ImplementGen;

public class PharmacyUnitOfWork : IPharmacyUnitOfWork
{
    private readonly IPharmacyService _pharmacyService;

    public PharmacyUnitOfWork(IPharmacyService pharmacyService)
    {
        _pharmacyService = pharmacyService;
    }

    public async Task<ActionResponse<IEnumerable<Pharmacy>>> ComboAsync() => await _pharmacyService.ComboAsync();

    public async Task<ActionResponse<IEnumerable<Pharmacy>>> GetAsync(PaginationDTO pagination) => await _pharmacyService.GetAsync(pagination);

    public async Task<ActionResponse<Pharmacy>> GetAsync(int id) => await _pharmacyService.GetAsync(id);

    public async Task<ActionResponse<Pharmacy>> UpdateAsync(Pharmacy modelo) => await _pharmacyService.UpdateAsync(modelo);

    public async Task<ActionResponse<Pharmacy>> AddAsync(Pharmacy modelo) => await _pharmacyService.AddAsync(modelo);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _pharmacyService.DeleteAsync(id);
}