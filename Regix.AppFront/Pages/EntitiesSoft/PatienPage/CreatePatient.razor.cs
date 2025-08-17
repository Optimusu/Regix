using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Regix.AppFront.GenericoModal;
using Regix.AppFront.Helpers;
using Regix.Domain.EntitiesSoft;
using Regix.HttpServices;

namespace Regix.AppFront.Pages.EntitiesSoft.PatienPage;

public partial class CreatePatient
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

    private Patient Patient = new() { Active = true, DOB = DateTime.Now };
    private PatientControl model = new();
    private string BaseUrl = "/api/v1/regpatient";
    private string BaseView = "/regpatient";

    protected override void OnInitialized()
    {
        model = _patientState.Get()!;
        Patient.PatientControlId = model.PatientControlId;
        Patient.FirstName = model.FirstName;
        Patient.LastName = model.LastName;
        Patient.DOB = Convert.ToDateTime(model.DOB);
    }

    private async Task Create()
    {
        var responseHttp = await _repository.PostAsync<Patient, Patient>($"{BaseUrl}", Patient);
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandled) return;
        Patient = responseHttp.Response!;

        await _sweetAlert.FireAsync(Messages.CreateSuccessTitle, Messages.CreateSuccessMessage, SweetAlertIcon.Success);
        _navigationManager.NavigateTo($"/register");
    }

    private void Return()
    {
        _navigationManager.NavigateTo($"/register");
    }
}