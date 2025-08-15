using Regix.Domain.EntitiesSoft;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;

namespace Regix.UnitOfWork.InterfaceSoft;

public interface IPatientControlUnitOfWork
{
    Task<ActionResponse<PatientControl>> ControlDataAsync(PatientControlDTO modelo, string username);

    Task<ActionResponse<IEnumerable<PatientControl>>> GetAsync(PaginationDTO pagination, string username);

    Task<ActionResponse<PatientControl>> GetAsync(Guid id);

    Task<ActionResponse<PatientControl>> UpdateAsync(PatientControl modelo);

    Task<ActionResponse<PatientControl>> AddAsync(PatientControl modelo, string username);

    Task<ActionResponse<bool>> DeleteAsync(Guid id);
}