using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Regix.AppFront.GenericoModal;
using Regix.AppFront.Helpers;
using Regix.Domain.EntitiesSoft;
using Regix.HttpServices;

namespace Regix.AppFront.Pages.EntitiesSoft.Patient2Page;

public partial class CreatePatient2
{
    //Services

    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;
    [Inject] private PatientControlStateService _patientState { get; set; } = null!;

    //Parameters

    [Parameter] public Guid Id { get; set; } //PatientId
    [Parameter] public string? Title { get; set; }

    //Local State

    private Patient Patient = new();
    private Patient2 Patient2 = new() { DateStart = DateTime.Now };
    private PatientControl model = new();
    private const string BaseUrlPatient = "/api/v1/regpatient";
    private string BaseUrl = "/api/v1/regpatient2s";
    private string BaseView = "/regpatient/edit";

    protected override void OnInitialized()
    {
        model = _patientState.Get()!;
        Patient2.PatientControlId = model.PatientControlId;
        Title = $"{model.FirstName} {model.LastName}";
    }

    private async Task Create()
    {
        var responseHttp = await _repository.PostAsync($"{BaseUrl}", Patient2);
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandled) return;

        await _sweetAlert.FireAsync(Messages.CreateSuccessTitle, Messages.CreateSuccessMessage, SweetAlertIcon.Success);
        _navigationManager.NavigateTo($"/register");
    }

    private void Return()
    {
        _navigationManager.NavigateTo($"/register");
    }
}