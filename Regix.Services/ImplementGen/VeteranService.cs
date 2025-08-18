using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Regix.AppInfra;
using Regix.AppInfra.ErrorHandling;
using Regix.AppInfra.Extensions;
using Regix.AppInfra.Transactions;
using Regix.AppInfra.UserHelper;
using Regix.AppInfra.Validations;
using Regix.Domain.EntitiesGen;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfacesGen;

namespace Regix.Services.ImplementGen;

public class VeteranService : IVeteranService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITransactionManager _transactionManager;
    private readonly HttpErrorHandler _httpErrorHandler;
    private readonly IStringLocalizer _localizer;
    private readonly IUserHelper _userHelper;

    public VeteranService(DataContext context, IHttpContextAccessor httpContextAccessor,
        ITransactionManager transactionManager, HttpErrorHandler httpErrorHandler,
        IStringLocalizer localizer, IUserHelper userHelper)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _transactionManager = transactionManager;
        _httpErrorHandler = httpErrorHandler;
        _localizer = localizer;
        _userHelper = userHelper;
    }

    public async Task<ActionResponse<IEnumerable<Veteran>>> ComboAsync()
    {
        try
        {
            List<Veteran> ListModel = await _context.Veterans.Where(x => x.Active).ToListAsync();
            // Insertar el elemento neutro al inicio
            var defaultItem = new Veteran
            {
                VeteranId = 0,
                Name = "[Select Veteran]",
                Active = true
            };
            ListModel.Insert(0, defaultItem);

            return new ActionResponse<IEnumerable<Veteran>>
            {
                WasSuccess = true,
                Result = ListModel
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<IEnumerable<Veteran>>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<IEnumerable<Veteran>>> GetAsync(PaginationDTO pagination)
    {
        try
        {
            var queryable = _context.Veterans.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                //Busqueda grandes mateniendo los indices de los campos, campo Esta Collation CI para Case Insensitive
                queryable = queryable.Where(u => EF.Functions.Like(u.Name, $"%{pagination.Filter}%"));
            }
            await _httpContextAccessor.HttpContext!.InsertParameterPagination(queryable, pagination.RecordsNumber);
            var modelo = await queryable.OrderBy(x => x.VeteranId).Paginate(pagination).ToListAsync();

            return new ActionResponse<IEnumerable<Veteran>>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<IEnumerable<Veteran>>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Veteran>> GetAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                return new ActionResponse<Veteran>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_InvalidId"]
                };
            }
            var modelo = await _context.Veterans
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.VeteranId == id);
            if (modelo == null)
            {
                return new ActionResponse<Veteran>
                {
                    WasSuccess = false,
                    Message = "Problemas para Enconstrar el Registro Indicado"
                };
            }

            return new ActionResponse<Veteran>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<Veteran>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Veteran>> UpdateAsync(Veteran modelo)
    {
        if (modelo == null || modelo.VeteranId <= 0)
        {
            return new ActionResponse<Veteran>
            {
                WasSuccess = false,
                Message = _localizer["Generic_InvalidId"]
            };
        }

        await _transactionManager.BeginTransactionAsync();
        try
        {
            _context.Veterans.Update(modelo);

            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<Veteran>
            {
                WasSuccess = true,
                Result = modelo,
                Message = _localizer["Generic_Success"]
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<Veteran>(ex); //Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Veteran>> AddAsync(Veteran modelo)
    {
        if (!ValidatorModel.IsValid(modelo, out var errores))
        {
            return new ActionResponse<Veteran>
            {
                WasSuccess = false,
                Message = _localizer["Generic_InvalidModel"] //Clave multilenguaje para modelo nulo
            };
        }

        await _transactionManager.BeginTransactionAsync();
        try
        {
            _context.Veterans.Add(modelo);
            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<Veteran>
            {
                WasSuccess = true,
                Result = modelo,
                Message = _localizer["Generic_Success"]
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<Veteran>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<bool>> DeleteAsync(int id)
    {
        await _transactionManager.BeginTransactionAsync();
        try
        {
            var DataRemove = await _context.Veterans.FindAsync(id);
            if (DataRemove == null)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_IdNotFound"]
                };
            }

            _context.Veterans.Remove(DataRemove);

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