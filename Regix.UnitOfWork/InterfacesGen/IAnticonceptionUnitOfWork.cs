using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.UnitOfWork.InterfacesGen;

public interface IAnticonceptionUnitOfWork
{
    Task<ActionResponse<IEnumerable<Anticonception>>> ComboAsync();

    Task<ActionResponse<IEnumerable<Anticonception>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Anticonception>> GetAsync(int id);

    Task<ActionResponse<Anticonception>> UpdateAsync(Anticonception modelo);

    Task<ActionResponse<Anticonception>> AddAsync(Anticonception modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}