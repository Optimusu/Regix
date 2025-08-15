using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Regix.AppFront.GenericoModal;
using Regix.AppFront.Helpers;
using Regix.Domain.EntitiesSoft;
using Regix.HttpServices;

namespace Regix.AppFront.Pages.EntitiesSoft.PatienPage;

public partial class EditPatient
{
    //Services

    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;

    //Parameters

    [Parameter] public Guid Id { get; set; }
    [Parameter] public string? Title { get; set; }

    //Local State

    private Patient? Patient;
    private bool IsLoading = false;
    private const string BaseUrl = "/api/v1/regpatient";
    private const string BaseView = "/regpatient";

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await _repository.GetAsync<Patient>($"{BaseUrl}/{Id}");
        if (await _responseHandler.HandleErrorAsync(responseHttp)) return;
        Patient = responseHttp.Response;
    }

    private async Task Edit()
    {
        IsLoading = true;

        var responseHttp = await _repository.PutAsync($"{BaseUrl}", Patient);
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandled)
        {
            IsLoading = false;
            return;
        }

        //await _sweetAlert.FireAsync(Messages.UpdateSuccessTitle, Messages.UpdateSuccessMessage, SweetAlertIcon.Success);
        //if (Patient!.TotalPatien2 == 0)
        //{
        //    _navigationManager.NavigateTo($"/regpatient2s/create/{Patient.PatientId}");
        //}
        //else
        //{
        //    _navigationManager.NavigateTo($"/regpatient2s/edit/{Patient.PatientId}");
        //}
        IsLoading = false;
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