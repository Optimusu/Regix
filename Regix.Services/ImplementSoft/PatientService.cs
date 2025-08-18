using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Regix.AppInfra;
using Regix.AppInfra.ErrorHandling;
using Regix.AppInfra.Extensions;
using Regix.AppInfra.Mappings;
using Regix.AppInfra.Transactions;
using Regix.AppInfra.UserHelper;
using Regix.AppInfra.Validations;
using Regix.Domain.Entities;
using Regix.Domain.EntitiesSoft;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfaceSoft;

namespace Regix.Services.ImplementSoft;

public class PatientService : IPatientService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITransactionManager _transactionManager;
    private readonly HttpErrorHandler _httpErrorHandler;
    private readonly IStringLocalizer _localizer;
    private readonly IUserHelper _userHelper;
    private readonly IMapperService _mapperService;

    public PatientService(DataContext context, IHttpContextAccessor httpContextAccessor,
        ITransactionManager transactionManager, HttpErrorHandler httpErrorHandler,
        IStringLocalizer localizer, IUserHelper userHelper, IMapperService mapperService)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _transactionManager = transactionManager;
        _httpErrorHandler = httpErrorHandler;
        _localizer = localizer;
        _userHelper = userHelper;
        _mapperService = mapperService;
    }

    public async Task<ActionResponse<IEnumerable<Patient>>> GetAsync(PaginationDTO pagination, string username)
    {
        try
        {
            User user = await _userHelper.GetUserByUserNameAsync(username);
            if (user == null)
            {
                return new ActionResponse<IEnumerable<Patient>>
                {
                    WasSuccess = false,
                    Message = "Problemas para Conseguir el Usuario"
                };
            }

            var queryable = _context.Patients
                 .Include(x => x.PatientControl)
                .Where(x => x.CorporationId == user.CorporationId && x.PatientControlId == pagination.GuidId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                //Busqueda grandes mateniendo los indices de los campos, campo Esta Collation CI para Case Insensitive
                queryable = queryable.Where(u => EF.Functions.Like(u.FullName, $"%{pagination.Filter}%"));
            }
            await _httpContextAccessor.HttpContext!.InsertParameterPagination(queryable, pagination.RecordsNumber);
            var modelo = await queryable.OrderBy(x => x.FullName).Paginate(pagination).ToListAsync();

            return new ActionResponse<IEnumerable<Patient>>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<IEnumerable<Patient>>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Patient>> GetAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                return new ActionResponse<Patient>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_InvalidId"]
                };
            }
            var modelo = await _context.Patients
                .AsNoTracking()
                .Include(x => x.PatientControl)
                .FirstOrDefaultAsync(x => x.PatientId == id);
            if (modelo == null)
            {
                return new ActionResponse<Patient>
                {
                    WasSuccess = false,
                    Message = "Problemas para Enconstrar el Registro Indicado"
                };
            }

            return new ActionResponse<Patient>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<Patient>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Patient>> UpdateAsync(Patient modelo)
    {
        if (modelo == null || modelo.PatientId == Guid.Empty)
        {
            return new ActionResponse<Patient>
            {
                WasSuccess = false,
                Message = _localizer["Generic_InvalidId"]
            };
        }

        await _transactionManager.BeginTransactionAsync();
        try
        {
            modelo.FullName = $"{modelo.FirstName} {modelo!.LastName}";
            Patient NuevoModelo = _mapperService.Map<Patient, Patient>(modelo);
            _context.Patients.Update(NuevoModelo);

            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<Patient>
            {
                WasSuccess = true,
                Result = modelo,
                Message = _localizer["Generic_Success"]
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<Patient>(ex); //Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Patient>> AddAsync(Patient modelo, string username)
    {
        User user = await _userHelper.GetUserByUserNameAsync(username);
        if (user == null)
        {
            return new ActionResponse<Patient>
            {
                WasSuccess = false,
                Message = _localizer["Generic_AuthIdFail"]
            };
        }

        modelo.CorporationId = Convert.ToInt32(user.CorporationId);
        modelo.FullName = $"{modelo.FirstName} {modelo!.LastName}";
        Patient NuevoModelo = _mapperService.Map<Patient, Patient>(modelo);

        if (!ValidatorModel.IsValid(modelo, out var errores))
        {
            return new ActionResponse<Patient>
            {
                WasSuccess = false,
                Result = modelo,
                Message = _localizer["Generic_InvalidModel"] //Clave multilenguaje para modelo nulo
            };
        }

        await _transactionManager.BeginTransactionAsync();
        try
        {
            _context.Patients.Add(NuevoModelo);

            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<Patient>
            {
                WasSuccess = true,
                Result = NuevoModelo,
                Message = _localizer["Generic_Success"]
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<Patient>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<bool>> DeleteAsync(Guid id)
    {
        await _transactionManager.BeginTransactionAsync();
        try
        {
            var DataRemove = await _context.Patients.FindAsync(id);
            if (DataRemove == null)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_IdNotFound"]
                };
            }

            _context.Patients.Remove(DataRemove);

            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<bool>
            {
                WasSuccess = true,
                Result = true,
                Message = _localizer["Generic_Success"]
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<bool>(ex); // ✅ Manejo de errores automático
        }
    }
}