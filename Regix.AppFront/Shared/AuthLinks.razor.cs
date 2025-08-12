using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Regix.AppFront.AuthenticationProviders;
using Regix.AppFront.GenericoModal;
using Regix.AppFront.Helpers;
using Regix.AppFront.Pages.Auth;

namespace Regix.AppFront.Shared
{
    public partial class AuthLinks
    {
        [Inject] private ISessionService _sessionService { get; set; } = null!;
        [Inject] private NavigationManager _navigation { get; set; } = null!;
        [Inject] private ILoginService _loginService { get; set; } = null!;
        [Inject] private ModalService _modalService { get; set; } = null!;
        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;

        private bool mostrarModalLogout = false;
        private string? photoUser;
        private string? LogoCorp;
        private string? NameCorp;

        protected override async Task OnParametersSetAsync()
        {
            var authenticationState = await AuthenticationStateTask;
            var claims = authenticationState.User.Claims.ToList();
            photoUser = _sessionService.PhotoUser;
            LogoCorp = _sessionService.LogoCorp;
            NameCorp = _sessionService.NameCorp;
        }

        private async Task ShowModalLogIn()
        {
            await _modalService.ShowAsync<Login>();
        }

        private async Task ShowModalLogOut()
        {
            await _modalService.ShowAsync<LogoutModal>();
        }

        private async Task ShowModalRecoverPassword()
        {
            await _modalService.ShowAsync<RecoverPassword>();
        }

        private async Task ShowModalCambiarClave()
        {
            await _modalService.ShowAsync<ChangePassword>();
        }
    }
}