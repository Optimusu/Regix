using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfacesGen;
using Regix.UnitOfWork.InterfacesGen;

namespace Regix.UnitOfWork.ImplementGen;

public class AnticonceptionUnitOfWork : IAnticonceptionUnitOfWork
{
    private readonly IAnticonceptionService _anticonception;

    public AnticonceptionUnitOfWork(IAnticonceptionService anticonception)
    {
        _anticonception = anticonception;
    }

    public async Task<ActionResponse<IEnumerable<Anticonception>>> ComboAsync() => await _anticonception.ComboAsync();

    public async Task<ActionResponse<IEnumerable<Anticonception>>> GetAsync(PaginationDTO pagination) => await _anticonception.GetAsync(pagination);

    public async Task<ActionResponse<Anticonception>> GetAsync(int id) => await _anticonception.GetAsync(id);

    public async Task<ActionResponse<Anticonception>> UpdateAsync(Anticonception modelo) => await _anticonception.UpdateAsync(modelo);

    public async Task<ActionResponse<Anticonception>> AddAsync(Anticonception modelo) => await _anticonception.AddAsync(modelo);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _anticonception.DeleteAsync(id);
}