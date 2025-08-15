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
    [Inject] private ModalService _modalService { get; set; } = null!;

    //Parameters

    [Parameter] public Guid Id { get; set; } //PatientId
    [Parameter] public string? Title { get; set; }

    //Local State

    private Patient? Patient;
    private Patient2? Patient2;
    private const string BaseUrlPatient = "/api/v1/regpatient";
    private const string BaseUrl = "/api/v1/regpatient2s";
    private const string BaseView = "/regpatient/edit";

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await _repository.GetAsync<Patient>($"{BaseUrlPatient}/{Id}");
        if (await _responseHandler.HandleErrorAsync(responseHttp)) return;
        Patient = responseHttp.Response;
    }

    private async Task Edit()
    {
        var responseHttp = await _repository.PutAsync($"{BaseUrl}", Patient2);
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandled) return;

        //await _sweetAlert.FireAsync(Messages.UpdateSuccessTitle, Messages.UpdateSuccessMessage, SweetAlertIcon.Success);
        //if (Patient!.TotalPatien2 == 0)
        //{
        //    //_navigationManager.NavigateTo($"/regpatient");
        //}
        //else
        //{
        //    //_navigationManager.NavigateTo($"/regpatient");
        //}
    }

    private void Return()
    {
        _navigationManager.NavigateTo($"{BaseView}/{Patient!.PatientId}");
    }

    private void ExitAction()
    {
        _navigationManager.NavigateTo($"/regpatient");
    }
}