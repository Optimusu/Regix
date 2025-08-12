using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.UnitOfWork.InterfacesGen;

public interface IDocumentTypeUnitOfWork
{
    Task<ActionResponse<IEnumerable<DocumentType>>> ComboAsync();

    Task<ActionResponse<IEnumerable<DocumentType>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<DocumentType>> GetAsync(int id);

    Task<ActionResponse<DocumentType>> UpdateAsync(DocumentType modelo);

    Task<ActionResponse<DocumentType>> AddAsync(DocumentType modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}