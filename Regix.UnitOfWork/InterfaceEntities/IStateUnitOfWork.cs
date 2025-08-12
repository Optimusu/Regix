using Regix.Domain.Entities;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.ResponsesSec;
using Regix.DomainLogic.TrialResponse;

namespace Regix.UnitOfWork.InterfaceEntities;

public interface IStateUnitOfWork
{
    Task<ActionResponse<IEnumerable<State>>> ComboAsync(ClaimsDTOs claimsDTO);

    Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<State>> GetAsync(int id);

    Task<ActionResponse<State>> UpdateAsync(State modelo);

    Task<ActionResponse<State>> AddAsync(State modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}