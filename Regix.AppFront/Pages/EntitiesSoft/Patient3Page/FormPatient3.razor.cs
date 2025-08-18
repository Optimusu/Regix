using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Regix.AppFront.Helpers;
using Regix.Domain.EntitiesGen;
using Regix.Domain.EntitiesSoft;
using Regix.HttpServices;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Regix.AppFront.Pages.EntitiesSoft.Patient3Page;

public partial class FormPatient3
{
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;

    [Parameter, EditorRequired] public Patient3 Patient3 { get; set; } = null!;
    [Parameter, EditorRequired] public EventCallback OnSubmit { get; set; }
    [Parameter, EditorRequired] public EventCallback ReturnAction { get; set; }

    private List<EtniaRaza>? EtniaRazas;
    private List<Discapacidad>? Discapacidads;
    private List<Veteran>? Veterans;

    protected override async Task OnInitializedAsync()
    {
        await LoadEtnia();
        await LoadDiscapacidad();
        await LoadVeteran();
    }

    private async Task LoadEtnia()
    {
        var responseHTTP = await _repository.GetAsync<List<EtniaRaza>>($"api/v1/etniarazas/loadCombo");
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHTTP);
        if (errorHandled) return;
        EtniaRazas = responseHTTP.Response;
    }

    private void EtniaChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            Patient3.EtniaRazaId = selectedId;
        }
    }

    private async Task LoadDiscapacidad()
    {
        var responseHTTP = await _repository.GetAsync<List<Discapacidad>>($"api/v1/discapacidades/loadCombo");
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHTTP);
        if (errorHandled) return;
        Discapacidads = responseHTTP.Response;
    }

    private void DiscapacidadChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            Patient3.DiscapacidadId = selectedId;
        }
    }

    private async Task LoadVeteran()
    {
        var responseHTTP = await _repository.GetAsync<List<Veteran>>($"api/v1/veterans/loadCombo");
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHTTP);
        if (errorHandled) return;
        Veterans = responseHTTP.Response;
    }

    private void VeteranChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            Patient3.VeteranId = selectedId;
        }
    }

    private string GetDisplayName<T>(Expression<Func<T>> expression)
    {
        if (expression.Body is MemberExpression memberExpression)
        {
            var property = memberExpression.Member as PropertyInfo;
            if (property != null)
            {
                var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
                if (displayAttribute != null)
                {
                    return displayAttribute.Name!;
                }
            }
        }
        return "Unspecified text";
    }
}