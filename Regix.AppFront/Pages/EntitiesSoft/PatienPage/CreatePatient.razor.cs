using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
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
    [Inject] private ModalService _modalService { get; set; } = null!;

    //Parameters

    [Parameter] public string? Title { get; set; }

    //Local State

    private Patient Patient = new() { Active = true, DOB = DateTime.Now };
    private string BaseUrl = "/api/v1/regpatient";
    private string BaseView = "/regpatient";

    private async Task Create()
    {
        var responseHttp = await _repository.PostAsync<Patient, Patient>($"{BaseUrl}", Patient);
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandled) return;
        Patient = responseHttp.Response!;

        //await _sweetAlert.FireAsync(Messages.CreateSuccessTitle, Messages.CreateSuccessMessage, SweetAlertIcon.Success);
        _modalService.Close();
        _navigationManager.NavigateTo($"/regpatient2s/create/{Patient.PatientId}");
    }

    private void Return()
    {
        _modalService.Close();
        _navigationManager.NavigateTo("/");
        _navigationManager.NavigateTo($"{BaseView}");
    }

    private void ExitAction()
    {
        _navigationManager.NavigateTo($"{BaseView}");
    }
}