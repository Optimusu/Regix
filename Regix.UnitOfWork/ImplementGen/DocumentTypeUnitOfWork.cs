using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfacesGen;
using Regix.UnitOfWork.InterfacesGen;

namespace Regix.UnitOfWork.ImplementGen;

public class DocumentTypeUnitOfWork : IDocumentTypeUnitOfWork
{
    private readonly IDocumentTypeService _documentTypeService;

    public DocumentTypeUnitOfWork(IDocumentTypeService documentTypeService)
    {
        _documentTypeService = documentTypeService;
    }

    public async Task<ActionResponse<IEnumerable<DocumentType>>> ComboAsync() => await _documentTypeService.ComboAsync();

    public async Task<ActionResponse<IEnumerable<DocumentType>>> GetAsync(PaginationDTO pagination) => await _documentTypeService.GetAsync(pagination);

    public async Task<ActionResponse<DocumentType>> GetAsync(int id) => await _documentTypeService.GetAsync(id);

    public async Task<ActionResponse<DocumentType>> UpdateAsync(DocumentType modelo) => await _documentTypeService.UpdateAsync(modelo);

    public async Task<ActionResponse<DocumentType>> AddAsync(DocumentType modelo) => await _documentTypeService.AddAsync(modelo);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _documentTypeService.DeleteAsync(id);
}