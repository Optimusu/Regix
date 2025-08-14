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

public class Patient2Service : IPatient2Service
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITransactionManager _transactionManager;
    private readonly HttpErrorHandler _httpErrorHandler;
    private readonly IStringLocalizer _localizer;
    private readonly IUserHelper _userHelper;
    private readonly IMapperService _mapperService;

    public Patient2Service(DataContext context, IHttpContextAccessor httpContextAccessor,
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

    public async Task<ActionResponse<IEnumerable<Patient2>>> GetAsync(PaginationDTO pagination, string username)
    {
        try
        {
            User user = await _userHelper.GetUserByUserNameAsync(username);
            if (user == null)
            {
                return new ActionResponse<IEnumerable<Patient2>>
                {
                    WasSuccess = false,
                    Message = "Problemas para Conseguir el Usuario"
                };
            }

            var queryable = _context.Patient2s
                .Include(x => x.Patient)
                .Where(x => x.CorporationId == user.CorporationId).AsQueryable();

            await _httpContextAccessor.HttpContext!.InsertParameterPagination(queryable, pagination.RecordsNumber);
            var modelo = await queryable.Paginate(pagination).ToListAsync();

            return new ActionResponse<IEnumerable<Patient2>>
            {
                WasSuccess = true,
                Result = queryable
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<IEnumerable<Patient2>>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Patient2>> GetAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                return new ActionResponse<Patient2>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_InvalidId"]
                };
            }
            var modelo = await _context.Patient2s
                .AsNoTracking()
                .Include(x => x.Patient)
                .FirstOrDefaultAsync(x => x.Patient2Id == id);
            if (modelo == null)
            {
                return new ActionResponse<Patient2>
                {
                    WasSuccess = false,
                    Message = "Problemas para Enconstrar el Registro Indicado"
                };
            }

            return new ActionResponse<Patient2>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<Patient2>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Patient2>> UpdateAsync(Patient2 modelo)
    {
        if (modelo == null || modelo.Patient2Id == Guid.Empty)
        {
            return new ActionResponse<Patient2>
            {
                WasSuccess = false,
                Message = _localizer["Generic_InvalidId"]
            };
        }

        await _transactionManager.BeginTransactionAsync();
        try
        {
            Patient2 NuevoModelo = _mapperService.Map<Patient2, Patient2>(modelo);
            _context.Patient2s.Update(NuevoModelo);

            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<Patient2>
            {
                WasSuccess = true,
                Result = modelo,
                Message = _localizer["Generic_Success"]
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<Patient2>(ex); //Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Patient2>> AddAsync(Patient2 modelo, string username)
    {
        User user = await _userHelper.GetUserByUserNameAsync(username);
        if (user == null)
        {
            return new ActionResponse<Patient2>
            {
                WasSuccess = false,
                Message = _localizer["Generic_AuthIdFail"]
            };
        }

        modelo.CorporationId = Convert.ToInt32(user.CorporationId);

        if (!ValidatorModel.IsValid(modelo, out var errores))
        {
            return new ActionResponse<Patient2>
            {
                WasSuccess = false,
                Message = _localizer["Generic_InvalidModel"] //Clave multilenguaje para modelo nulo
            };
        }

        await _transactionManager.BeginTransactionAsync();
        try
        {
            _context.Patient2s.Add(modelo);

            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<Patient2>
            {
                WasSuccess = true,
                Result = modelo,
                Message = _localizer["Generic_Success"]
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<Patient2>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<bool>> DeleteAsync(Guid id)
    {
        await _transactionManager.BeginTransactionAsync();
        try
        {
            var DataRemove = await _context.Patient2s.FindAsync(id);
            if (DataRemove == null)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_IdNotFound"]
                };
            }

            _context.Patient2s.Remove(DataRemove);

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