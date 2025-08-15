using Regix.DomainLogic.ResponsesSec;
using Regix.DomainLogic.TrialResponse;

namespace Regix.UnitOfWork.InterfacesSecure;

public interface IAccountUnitOfWork
{
    Task<ActionResponse<TokenDTO>> LoginAsync(LoginDTO modelo);

    Task<ActionResponse<TokenDTO>> RegisterAsync(RegisterDTO modelo);

    Task<ActionResponse<bool>> RecoverPasswordAsync(EmailDTO modelo, string frontUrl);

    Task<ActionResponse<bool>> ResetPasswordAsync(ResetPasswordDTO modelo);

    Task<ActionResponse<bool>> ChangePasswordAsync(ChangePasswordDTO modelo, string UserName);

    Task<ActionResponse<bool>> ConfirmEmailAsync(string userId, string token);
}