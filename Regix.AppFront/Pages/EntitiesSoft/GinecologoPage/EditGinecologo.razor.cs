using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Regix.AppFront.Helpers;
using Regix.Domain.EntitiesSoft;
using Regix.HttpServices;

namespace Regix.AppFront.Pages.EntitiesSoft.GinecologoPage;

public partial class EditGinecologo
{
    //Services

    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;
    [Inject] private PatientControlStateService _patientState { get; set; } = null!;

    //Parameters

    [Parameter] public Guid Id { get; set; } //GinecologicoId
    [Parameter] public string? Title { get; set; }

    //Local State

    private Ginecologico? Ginecologico;
    private PatientControl model = new();
    private const string BaseUrl = "/api/v1/ginecologicos";
    private const string BaseView = "/register";

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await _repository.GetAsync<Ginecologico>($"{BaseUrl}/{Id}");
        if (await _responseHandler.HandleErrorAsync(responseHttp)) return;
        Ginecologico = responseHttp.Response;
        model = _patientState.Get()!;
        Title = $"{model.FirstName} {model.LastName}";
    }

    private async Task Edit()
    {
        var responseHttp = await _repository.PutAsync($"{BaseUrl}", Ginecologico);
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