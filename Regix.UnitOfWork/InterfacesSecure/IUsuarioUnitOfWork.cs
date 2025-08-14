using Regix.Domain.EntitesSoftSec;
using Regix.Domain.Enum;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.UnitOfWork.InterfacesSecure;

public interface IUsuarioUnitOfWork
{
    Task<ActionResponse<IEnumerable<EnumItemModel>>> ComboAsync(string username);

    Task<ActionResponse<IEnumerable<Usuario>>> GetAsync(PaginationDTO pagination, string username);

    Task<ActionResponse<Usuario>> GetAsync(int id);

    Task<ActionResponse<Usuario>> UpdateAsync(Usuario modelo, string UrlFront);

    Task<ActionResponse<Usuario>> AddAsync(Usuario modelo, string urlFront, string username);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}