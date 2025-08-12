using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Regix.AppFront.GenericoModal;
using Regix.AppFront.Helpers;
using Regix.Domain.EntitiesGen;
using Regix.HttpServices;

namespace Regix.AppFront.Pages.EntitiesGen.IdentidadGeneroPage;

public partial class CreateIndentidadGenero
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

    private IdentidadGenero IdentidadGenero = new() { Active = true };
    private string BaseUrl = "/api/v1/identidadgeneros";
    private string BaseView = "/identidadgeneros";

    private async Task Create()
    {
        var responseHttp = await _repository.PostAsync($"{BaseUrl}", IdentidadGenero);
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandled) return;

        await _sweetAlert.FireAsync(Messages.CreateSuccessTitle, Messages.CreateSuccessMessage, SweetAlertIcon.Success);
        _modalService.Close();
        _navigationManager.NavigateTo("/");
        _navigationManager.NavigateTo(BaseView);
    }

    private void Return()
    {
        _modalService.Close();
        _navigationManager.NavigateTo("/");
        _navigationManager.NavigateTo($"{BaseView}");
    }
}