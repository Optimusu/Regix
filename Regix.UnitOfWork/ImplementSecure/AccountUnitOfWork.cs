using Regix.DomainLogic.ResponsesSec;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfacesSecure;
using Regix.UnitOfWork.InterfacesSecure;

namespace Regix.UnitOfWork.ImplementSecure;

public class AccountUnitOfWork : IAccountUnitOfWork
{
    private readonly IAccountService _accountService;

    public AccountUnitOfWork(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<ActionResponse<TokenDTO>> LoginAsync(LoginDTO modelo) => await _accountService.LoginAsync(modelo);

    public async Task<ActionResponse<TokenDTO>> RegisterAsync(RegisterDTO modelo) => await _accountService.RegisterAsync(modelo);

    public async Task<ActionResponse<bool>> RecoverPasswordAsync(EmailDTO modelo, string frontUrl) => await _accountService.RecoverPasswordAsync(modelo, frontUrl);

    public async Task<ActionResponse<bool>> ResetPasswordAsync(ResetPasswordDTO modelo) => await _accountService.ResetPasswordAsync(modelo);

    public async Task<ActionResponse<bool>> ChangePasswordAsync(ChangePasswordDTO modelo, string UserName) => await _accountService.ChangePasswordAsync(modelo, UserName);

    public async Task<ActionResponse<bool>> ConfirmEmailAsync(string userId, string token) => await _accountService.ConfirmEmailAsync(userId, token);
}