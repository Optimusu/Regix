using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Regix.AppFront.AuthenticationProviders;
using Regix.AppFront.Helpers;
using Regix.Domain.Entities;
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
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;

    private RegisterDTO RegisterDTO = new() { DOB = DateTime.Now };
    private DateTime? DateMin = new DateTime(1900, 1, 1);
    private List<Corporation>? Corporations;
    private string BaseComboCompany = "/api/v1/corporations/loadCombo";
    private bool rememberMe;

    protected override async Task OnInitializedAsync()
    {
        await LoadCorporation();
    }

    private async Task LoadCorporation()
    {
        var responseHttp = await _repository.GetAsync<List<Corporation>>($"{BaseComboCompany}");
        bool errorHandler = await _httpHandler.HandleErrorAsync(responseHttp);
        if (errorHandler)
        {
            _navigation.NavigateTo($"/");
            return;
        }
        Corporations = responseHttp.Response;
    }

    private async Task CorporationChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            if (string.IsNullOrEmpty(RegisterDTO.FirstName) || string.IsNullOrEmpty(RegisterDTO.LastName))
            {
                await _sweetAlert.FireAsync(Messages.ValidationWarningTitle, Messages.ValidationWarningMessage, SweetAlertIcon.Warning);
                return;
            }
            if (RegisterDTO.DOB == DateTime.Now)
            {
                await _sweetAlert.FireAsync(Messages.ValidationWarningTitle, Messages.ValidationWarningMessage, SweetAlertIcon.Warning);
                return;
            }

            if (selectedId == 0)
            {
                return;
            }
            RegisterDTO.CorporationId = selectedId;

            var pLetarLastName = RegisterDTO.LastName[0];
            var bdYear = RegisterDTO.DOB.Year;
            var PossibleUser = $"{RegisterDTO.FirstName}{pLetarLastName}{bdYear}";
            var PossiblePass = $"{RegisterDTO.FirstName}{bdYear}";
            RegisterDTO.UserName = PossibleUser;
            RegisterDTO.NewPassword = PossiblePass;
            RegisterDTO.Confirm = PossiblePass;
        }
    }

    private async Task RegisterAsync()
    {
        //ValidandoFechaNacimiento
        if (RegisterDTO.DOB == DateTime.Now)
        {
            await _sweetAlert.FireAsync(Messages.ValidationWarningTitle, Messages.ValidationWarningMessage, SweetAlertIcon.Warning);
            return;
        }
        if (string.IsNullOrEmpty(RegisterDTO.FirstName) || string.IsNullOrEmpty(RegisterDTO.LastName))
        {
            await _sweetAlert.FireAsync(Messages.ValidationWarningTitle, Messages.ValidationWarningMessage, SweetAlertIcon.Warning);
            return;
        }
        if (string.IsNullOrEmpty(RegisterDTO.NewPassword) || string.IsNullOrEmpty(RegisterDTO.Confirm) || string.IsNullOrEmpty(RegisterDTO.UserName))
        {
            await _sweetAlert.FireAsync(Messages.ValidationWarningTitle, Messages.ValidationWarningMessage, SweetAlertIcon.Warning);
            return;
        }
        var responseHttp = await _repository.PostAsync<RegisterDTO, TokenDTO>("/api/v1/accounts/Register", RegisterDTO);
        if (await _httpHandler.HandleErrorAsync(responseHttp)) return;
        await _loginService.LoginAsync(responseHttp.Response!.Token);
        _sessionService.PhotoUser = responseHttp.Response.PhotoBase64;
        _sessionService.LogoCorp = responseHttp.Response.LogoBase64;

        _navigation.NavigateTo("/register");
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