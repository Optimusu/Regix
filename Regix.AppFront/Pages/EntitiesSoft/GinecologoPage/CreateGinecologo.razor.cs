using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Regix.AppFront.Helpers;
using Regix.Domain.EntitiesSoft;
using Regix.HttpServices;

namespace Regix.AppFront.Pages.EntitiesSoft.GinecologoPage;

public partial class CreateGinecologo
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

    private Ginecologico Ginecologico = new() { LastMenstruation = DateTime.Now, LastCytologyPap = DateTime.Now };
    private PatientControl model = new();
    private string BaseUrl = "/api/v1/ginecologicos";
    private string BaseView = "/register";

    protected override void OnInitialized()
    {
        model = _patientState.Get()!;
        Ginecologico.PatientControlId = model.PatientControlId;
        Title = $"{model.FirstName} {model.LastName}";
    }

    private async Task Create()
    {
        var responseHttp = await _repository.PostAsync($"{BaseUrl}", Ginecologico);
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