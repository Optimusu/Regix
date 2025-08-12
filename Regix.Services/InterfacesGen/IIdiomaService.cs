using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.Services.InterfacesGen;

public interface IIdiomaService
{
    Task<ActionResponse<IEnumerable<Idioma>>> ComboAsync();

    Task<ActionResponse<IEnumerable<Idioma>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Idioma>> GetAsync(int id);

    Task<ActionResponse<Idioma>> UpdateAsync(Idioma modelo);

    Task<ActionResponse<Idioma>> AddAsync(Idioma modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}