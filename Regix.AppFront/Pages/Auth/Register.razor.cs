using Microsoft.AspNetCore.Components;
using Regix.AppFront.AuthenticationProviders;
using Regix.AppFront.GenericoModal;
using Regix.AppFront.Helpers;
using Regix.DomainLogic.ResponsesSec;
using Regix.HttpServices;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Regix.AppFront.Pages.Auth;

public partial class Register
{
    [Inject] private ISessionService _sessionService { get; set; } = null!;
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigation { get; set; } = null!;
    [Inject] private ILoginService _loginService { get; set; } = null!;
    [Inject] private HttpResponseHandler _httpHandler { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;

    private LoginDTO loginDTO = new();
    private bool rememberMe;

    private async Task LoginAsync()
    {
        var responseHttp = await _repository.PostAsync<LoginDTO, TokenDTO>("/api/v1/accounts/Login", loginDTO);
        if (await _httpHandler.HandleErrorAsync(responseHttp)) return;
        await _loginService.LoginAsync(responseHttp.Response!.Token);
        _sessionService.PhotoUser = responseHttp.Response.PhotoBase64;
        _sessionService.LogoCorp = responseHttp.Response.LogoBase64;
        _sessionService.NameCorp = responseHttp.Response.NameCorp;

        _navigation.NavigateTo("/dashboard");
        _modalService.Close();
    }

    private async Task OpenRecoverPasswordModal()
    {
        await _modalService.ShowAsync<RecoverPassword>();
    }

    private void CloseModal()
    {
        _modalService.Close();
    }

    private string GetDisplayName<T>(Expression<Func<T>> expression)
    {
        if (expression.Body is MemberExpression memberExpression)
        {
            var property = memberExpression.Member as PropertyInfo;
            if (property != null)
            {
                var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
                if (displayAttribute != null)
                {
                    return displayAttribute.Name!;
                }
            }
        }
        return "Unspecified text";
    }
}