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

public class PatientControlService : IPatientControlService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITransactionManager _transactionManager;
    private readonly HttpErrorHandler _httpErrorHandler;
    private readonly IStringLocalizer _localizer;
    private readonly IUserHelper _userHelper;
    private readonly IMapperService _mapperService;

    public PatientControlService(DataContext context, IHttpContextAccessor httpContextAccessor,
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

    public async Task<ActionResponse<PatientControl>> ControlDataAsync(PatientControlDTO modelo, string username)
    {
        User user = await _userHelper.GetUserByUserNameAsync(username);
        if (user == null)
        {
            return new ActionResponse<PatientControl>
            {
                WasSuccess = false,
                Message = "Problemas para Conseguir el Usuario"
            }
            ;
        }
        if (user.UserName != modelo.UserName)
        {
            return new ActionResponse<PatientControl>
            {
                WasSuccess = false,
                Message = "Problemas para Enconstrar el Registro Indicado"
            };
        }
        if (user.CorporationId != modelo.CorporationId)
        {
            return new ActionResponse<PatientControl>
            {
                WasSuccess = false,
                Message = "Problemas para Enconstrar el Registro Indicado"
            };
        }

        PatientControl? patientControl = await _context.PatientControls
            .Include(x => x.Patients!).ThenInclude(x => x.SexoAsignado)
            .Include(x => x.Patient2s)
            .FirstOrDefaultAsync(x => x.CorporationId == user.CorporationId && x.UserName == user.UserName && x.FirstName == modelo.FirstName);
        return new ActionResponse<PatientControl>
        {
            WasSuccess = true,
            Result = patientControl
        };
    }

    public async Task<ActionResponse<IEnumerable<PatientControl>>> GetAsync(PaginationDTO pagination, string username)
    {
        try
        {
            User user = await _userHelper.GetUserByUserNameAsync(username);
            if (user == null)
            {
                return new ActionResponse<IEnumerable<PatientControl>>
                {
                    WasSuccess = false,
                    Message = "Problemas para Conseguir el Usuario"
                };
            }

            var queryable = _context.PatientControls
                 .Include(x => x.Patients).Include(x => x.Patient2s)
                .Where(x => x.CorporationId == user.CorporationId && x.PatientControlId == pagination.GuidId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                //Busqueda grandes mateniendo los indices de los campos, campo Esta Collation CI para Case Insensitive
                queryable = queryable.Where(u => EF.Functions.Like(u.FirstName, $"%{pagination.Filter}%"));
            }
            await _httpContextAccessor.HttpContext!.InsertParameterPagination(queryable, pagination.RecordsNumber);
            var modelo = await queryable.OrderBy(x => x.FirstName).Paginate(pagination).ToListAsync();

            return new ActionResponse<IEnumerable<PatientControl>>
            {
                WasSuccess = true,
                Result = queryable
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<IEnumerable<PatientControl>>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<PatientControl>> GetAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                return new ActionResponse<PatientControl>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_InvalidId"]
                };
            }
            var modelo = await _context.PatientControls
                .AsNoTracking()
                .Include(x => x.Patients).Include(x => x.Patient2s)
                .FirstOrDefaultAsync(x => x.PatientControlId == id);
            if (modelo == null)
            {
                return new ActionResponse<PatientControl>
                {
                    WasSuccess = false,
                    Message = "Problemas para Enconstrar el Registro Indicado"
                };
            }

            return new ActionResponse<PatientControl>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<PatientControl>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<PatientControl>> UpdateAsync(PatientControl modelo)
    {
        if (modelo == null || modelo.PatientControlId == Guid.Empty)
        {
            return new ActionResponse<PatientControl>
            {
                WasSuccess = false,
                Message = _localizer["Generic_InvalidId"]
            };
        }

        await _transactionManager.BeginTransactionAsync();
        try
        {
            PatientControl NuevoModelo = _mapperService.Map<PatientControl, PatientControl>(modelo);
            _context.PatientControls.Update(NuevoModelo);

            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<PatientControl>
            {
                WasSuccess = true,
                Result = modelo,
                Message = _localizer["Generic_Success"]
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<PatientControl>(ex); //Manejo de errores automático
        }
    }

    public async Task<ActionResponse<PatientControl>> AddAsync(PatientControl modelo, string username)
    {
        User user = await _userHelper.GetUserByUserNameAsync(username);
        if (user == null)
        {
            return new ActionResponse<PatientControl>
            {
                WasSuccess = false,
                Message = _localizer["Generic_AuthIdFail"]
            };
        }

        modelo.CorporationId = Convert.ToInt32(user.CorporationId);
        PatientControl NuevoModelo = _mapperService.Map<PatientControl, PatientControl>(modelo);

        if (!ValidatorModel.IsValid(modelo, out var errores))
        {
            return new ActionResponse<PatientControl>
            {
                WasSuccess = false,
                Result = modelo,
                Message = _localizer["Generic_InvalidModel"] //Clave multilenguaje para modelo nulo
            };
        }

        await _transactionManager.BeginTransactionAsync();
        try
        {
            _context.PatientControls.Add(NuevoModelo);

            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<PatientControl>
            {
                WasSuccess = true,
                Result = NuevoModelo,
                Message = _localizer["Generic_Success"]
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<PatientControl>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<bool>> DeleteAsync(Guid id)
    {
        await _transactionManager.BeginTransactionAsync();
        try
        {
            var DataRemove = await _context.PatientControls.FindAsync(id);
            if (DataRemove == null)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_IdNotFound"]
                };
            }

            _context.PatientControls.Remove(DataRemove);

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