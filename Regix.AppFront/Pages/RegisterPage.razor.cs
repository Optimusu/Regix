using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Regix.AppFront.GenericoModal;
using Regix.AppFront.Helpers;
using Regix.Domain.EntitiesSoft;
using Regix.HttpServices;
using System.Buffers.Text;
using System.Security.Claims;

namespace Regix.AppFront.Pages;

public partial class RegisterPage
{
    [Inject] private ISessionService _sessionService { get; set; } = null!;
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigation { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;
    [CascadingParameter] private Task<AuthenticationState> authenticationState { get; set; } = null!;

    private PatientControlDTO PatientControlDTO = new();
    private PatientControl PatientControl = new();
    private string BaseUrl = "/api/v1/patientcontrols";
    private bool CompletePatient = true;

    protected override async Task OnInitializedAsync()
    {
        var authState = await authenticationState;
        var userClaims = authState.User;

        string userId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        PatientControlDTO.UserName = userClaims.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
        PatientControlDTO.FirstName = userClaims.FindFirst("FirstName")?.Value ?? string.Empty;
        PatientControlDTO.LastName = userClaims.FindFirst("LastName")?.Value ?? string.Empty;
        PatientControlDTO.CorporationId = Convert.ToInt32(userClaims.FindFirst("CorporateId")?.Value ?? string.Empty);

        var responseHttp = await _repository.PostAsync<PatientControlDTO, PatientControl>($"{BaseUrl}/PatientControlData", PatientControlDTO);
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandled) return;
        PatientControl = responseHttp.Response!;
    }

    private void ClickPatient()
    {
        _navigation.NavigateTo("/regpatient/create");
    }
}