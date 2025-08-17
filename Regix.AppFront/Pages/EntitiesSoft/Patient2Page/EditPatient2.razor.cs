using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Regix.AppFront.GenericoModal;
using Regix.AppFront.Helpers;
using Regix.Domain.EntitiesSoft;
using Regix.HttpServices;

namespace Regix.AppFront.Pages.EntitiesSoft.Patient2Page;

public partial class EditPatient2
{
    //Services

    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;
    [Inject] private PatientControlStateService _patientState { get; set; } = null!;

    //Parameters

    [Parameter] public Guid Id { get; set; } //Patient2Id
    [Parameter] public string? Title { get; set; }

    //Local State

    private Patient2? Patient2;
    private PatientControl model = new();
    private const string BaseUrlPatient = "/api/v1/regpatient";
    private const string BaseUrl = "/api/v1/regpatient2s";
    private const string BaseView = "/register";

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await _repository.GetAsync<Patient2>($"{BaseUrl}/{Id}");
        if (await _responseHandler.HandleErrorAsync(responseHttp)) return;
        Patient2 = responseHttp.Response;
        model = _patientState.Get()!;
        Title = $"{model.FirstName} {model.LastName}";
    }

    private async Task Edit()
    {
        var responseHttp = await _repository.PutAsync($"{BaseUrl}", Patient2);
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandled) return;

        await _sweetAlert.FireAsync(Messages.UpdateSuccessTitle, Messages.UpdateSuccessMessage, SweetAlertIcon.Success);
        _navigationManager.NavigateTo($"{BaseView}");
    }

    private void Return()
    {
        _navigationManager.NavigateTo($"{BaseView}");
    }
}