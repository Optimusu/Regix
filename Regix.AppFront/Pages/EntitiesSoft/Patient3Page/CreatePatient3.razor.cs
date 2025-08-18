using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Regix.AppFront.Helpers;
using Regix.Domain.EntitiesSoft;
using Regix.HttpServices;

namespace Regix.AppFront.Pages.EntitiesSoft.Patient3Page;

public partial class CreatePatient3
{
    //Services

    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;
    [Inject] private PatientControlStateService _patientState { get; set; } = null!;

    //Parameters
    [Parameter] public string? Title { get; set; }

    //Local State

    private Patient3 Patient3 = new();
    private PatientControl model = new();
    private string BaseUrl = "/api/v1/regpatient3s";
    private string BaseView = "/register";

    protected override void OnInitialized()
    {
        model = _patientState.Get()!;
        Patient3.PatientControlId = model.PatientControlId;
        Title = $"{model.FirstName} {model.LastName}";
    }

    private async Task Create()
    {
        var responseHttp = await _repository.PostAsync($"{BaseUrl}", Patient3);
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandled) return;

        await _sweetAlert.FireAsync(Messages.CreateSuccessTitle, Messages.CreateSuccessMessage, SweetAlertIcon.Success);
        _navigationManager.NavigateTo($"{BaseView}");
    }

    private void Return()
    {
        _navigationManager.NavigateTo($"{BaseView}");
    }
}