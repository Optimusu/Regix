using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.Services.InterfacesGen;

public interface IAnticonceptionService
{
    Task<ActionResponse<IEnumerable<Anticonception>>> ComboAsync();

    Task<ActionResponse<IEnumerable<Anticonception>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Anticonception>> GetAsync(int id);

    Task<ActionResponse<Anticonception>> UpdateAsync(Anticonception modelo);

    Task<ActionResponse<Anticonception>> AddAsync(Anticonception modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}