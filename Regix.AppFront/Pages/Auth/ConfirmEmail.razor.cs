using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Regix.AppFront.GenericoModal;
using Regix.AppFront.Helpers;
using Regix.HttpServices;

namespace Regix.AppFront.Pages.Auth;

public partial class ConfirmEmail
{
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigation { get; set; } = null!;
    [Inject] private HttpResponseHandler _httpHandler { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;

    [Parameter, SupplyParameterFromQuery] public string userid { get; set; } = string.Empty;
    [Parameter, SupplyParameterFromQuery] public string token { get; set; } = string.Empty;

    private async Task ConfirmAccountAsync()
    {
        var responseHttp = await _repository.GetAsync($"/api/v1/accounts/ConfirmEmail/?userId={userid}&token={token}");
        if (await _httpHandler.HandleErrorAsync(responseHttp))
        {
            _navigation.NavigateTo("/");
            return;
        }
        await _sweetAlert.FireAsync("Email Confirmed", "Your account has been successfully activated.", SweetAlertIcon.Success);
        _navigation.NavigateTo("/");
        await _modalService.ShowAsync<Login>();
    }
}