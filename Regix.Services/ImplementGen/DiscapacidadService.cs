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

public class DiscapacidadService : IDiscapacidadService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITransactionManager _transactionManager;
    private readonly HttpErrorHandler _httpErrorHandler;
    private readonly IStringLocalizer _localizer;
    private readonly IUserHelper _userHelper;

    public DiscapacidadService(DataContext context, IHttpContextAccessor httpContextAccessor,
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

    public async Task<ActionResponse<IEnumerable<Discapacidad>>> ComboAsync()
    {
        try
        {
            List<Discapacidad> ListModel = await _context.Discapacidads.Where(x => x.Active).ToListAsync();
            // Insertar el elemento neutro al inicio
            var defaultItem = new Discapacidad
            {
                DiscapacidadId = 0,
                Name = "[Select Disability]",
                Active = true
            };
            ListModel.Insert(0, defaultItem);

            return new ActionResponse<IEnumerable<Discapacidad>>
            {
                WasSuccess = true,
                Result = ListModel
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<IEnumerable<Discapacidad>>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<IEnumerable<Discapacidad>>> GetAsync(PaginationDTO pagination)
    {
        try
        {
            var queryable = _context.Discapacidads.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                //Busqueda grandes mateniendo los indices de los campos, campo Esta Collation CI para Case Insensitive
                queryable = queryable.Where(u => EF.Functions.Like(u.Name, $"%{pagination.Filter}%"));
            }
            await _httpContextAccessor.HttpContext!.InsertParameterPagination(queryable, pagination.RecordsNumber);
            var modelo = await queryable.OrderBy(x => x.DiscapacidadId).Paginate(pagination).ToListAsync();

            return new ActionResponse<IEnumerable<Discapacidad>>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<IEnumerable<Discapacidad>>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Discapacidad>> GetAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                return new ActionResponse<Discapacidad>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_InvalidId"]
                };
            }
            var modelo = await _context.Discapacidads
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DiscapacidadId == id);
            if (modelo == null)
            {
                return new ActionResponse<Discapacidad>
                {
                    WasSuccess = false,
                    Message = "Problemas para Enconstrar el Registro Indicado"
                };
            }

            return new ActionResponse<Discapacidad>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<Discapacidad>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Discapacidad>> UpdateAsync(Discapacidad modelo)
    {
        if (modelo == null || modelo.DiscapacidadId <= 0)
        {
            return new ActionResponse<Discapacidad>
            {
                WasSuccess = false,
                Message = _localizer["Generic_InvalidId"]
            };
        }

        await _transactionManager.BeginTransactionAsync();
        try
        {
            _context.Discapacidads.Update(modelo);

            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<Discapacidad>
            {
                WasSuccess = true,
                Result = modelo,
                Message = _localizer["Generic_Success"]
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<Discapacidad>(ex); //Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Discapacidad>> AddAsync(Discapacidad modelo)
    {
        if (!ValidatorModel.IsValid(modelo, out var errores))
        {
            return new ActionResponse<Discapacidad>
            {
                WasSuccess = false,
                Message = _localizer["Generic_InvalidModel"] //Clave multilenguaje para modelo nulo
            };
        }

        await _transactionManager.BeginTransactionAsync();
        try
        {
            _context.Discapacidads.Add(modelo);
            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<Discapacidad>
            {
                WasSuccess = true,
                Result = modelo,
                Message = _localizer["Generic_Success"]
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<Discapacidad>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<bool>> DeleteAsync(int id)
    {
        await _transactionManager.BeginTransactionAsync();
        try
        {
            var DataRemove = await _context.Discapacidads.FindAsync(id);
            if (DataRemove == null)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_IdNotFound"]
                };
            }

            _context.Discapacidads.Remove(DataRemove);

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