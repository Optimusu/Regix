using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Regix.AppFront.Helpers;
using Regix.Domain.EntitiesSoft;
using Regix.HttpServices;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace Regix.AppFront.Pages;

public partial class RegisterPage
{
    [Inject] private ISessionService _sessionService { get; set; } = null!;
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigation { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;
    [Inject] private PatientControlStateService _patientState { get; set; } = null!;

    [CascadingParameter] private Task<AuthenticationState> authenticationState { get; set; } = null!;

    private PatientControlDTO PatientControlDTO = new();
    private PatientControl PatientControl = new();
    private string BaseUrl = "/api/v1/patientcontrols";
    private bool ViewGinecologico = false;
    private bool CompletePatient = false;
    private bool CompletePatient2 = false;
    private bool CompleteGinecologico3 = false;

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
        _patientState.Set(PatientControl);
        ViewGinecologico = PatientControl.Patients!.FirstOrDefault()!.SexoAsignado!.Name == "Female" ? true : false;
        CompletePatient = PatientControl.TPatient == 0 ? false : true;
        CompletePatient2 = PatientControl.TPatient2 == 0 ? false : true;
    }

    private void ClickPatient()
    {
        //Llamamos a Register y enviamos el PatientControlId
        if (PatientControl.TPatient == 0)
        {
            _navigation.NavigateTo($"/regpatient/create");
        }
        else
        {
            _navigation.NavigateTo($"/regpatient/edit/{PatientControl.Patients!.FirstOrDefault()!.PatientId}");
        }
    }

    private void ClickPatient2()
    {
        //Llamamos a Register y enviamos el PatientControlId
        if (PatientControl.TPatient2 == 0)
        {
            _navigation.NavigateTo($"/regpatient2s/create");
        }
        else
        {
            _navigation.NavigateTo($"/regpatient2s/edit/{PatientControl.Patient2s!.FirstOrDefault()!.Patient2Id}");
        }
    }
}