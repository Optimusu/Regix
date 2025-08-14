using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Regix.AppFront.Helpers;
using Regix.Domain.EntitiesGen;
using Regix.Domain.EntitiesSoft;
using Regix.Domain.Enum;
using Regix.HttpServices;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Regix.AppFront.Pages.EntitiesSoft.PatienPage;

public partial class FormPatient
{
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;

    [Parameter, EditorRequired] public Patient Patient { get; set; } = null!;
    [Parameter, EditorRequired] public EventCallback OnSubmit { get; set; }
    [Parameter, EditorRequired] public EventCallback ReturnAction { get; set; }
    [Parameter, EditorRequired] public EventCallback ExitAction { get; set; }

    private List<DocumentType>? DocumentTypes;
    private List<SexoAsignado>? SexoAsignados;
    private List<IdentidadGenero>? IdentidadGeneros;
    private List<EstadoCivil>? EstadoCivils;
    private List<Idioma>? Idiomas;
    private List<Pharmacy>? Pharmacies;
    private DateTime? DateMin = new DateTime(1900, 1, 1);

    protected override async Task OnInitializedAsync()
    {
        await LoadSexoAsignado();
        await LoadIdentidadGenero();
        await LoadEstadoCivil();
        await LoadTipoDocumento();
        await LoadIdiomas();
        await LoadPharmacy();
    }

    private async Task LoadPharmacy()
    {
        var responseHTTP = await _repository.GetAsync<List<Pharmacy>>($"api/v1/pharmacies/loadCombo");
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHTTP);
        if (errorHandled) return;
        Pharmacies = responseHTTP.Response;
    }

    private void PharmacyChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            Patient.PharmacyId = selectedId;
        }
    }

    private async Task LoadIdiomas()
    {
        var responseHTTP = await _repository.GetAsync<List<Idioma>>($"api/v1/idiomas/loadCombo");
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHTTP);
        if (errorHandled) return;
        Idiomas = responseHTTP.Response;
    }

    private void IdiomasChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            Patient.IdiomaId = selectedId;
        }
    }

    private async Task LoadEstadoCivil()
    {
        var responseHTTP = await _repository.GetAsync<List<EstadoCivil>>($"api/v1/estadosciviles/loadCombo");
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHTTP);
        if (errorHandled) return;
        EstadoCivils = responseHTTP.Response;
    }

    private void EstadoCivilChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            Patient.EstadoCivilId = selectedId;
        }
    }

    private async Task LoadTipoDocumento()
    {
        var responseHTTP = await _repository.GetAsync<List<DocumentType>>($"api/v1/documentTypes/loadCombo");
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHTTP);
        if (errorHandled) return;
        DocumentTypes = responseHTTP.Response;
    }

    private void TipoDocumentoChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            Patient.DocumentTypeId = selectedId;
        }
    }

    private async Task LoadIdentidadGenero()
    {
        var responseHTTP = await _repository.GetAsync<List<IdentidadGenero>>($"api/v1/identidadgeneros/loadCombo");
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHTTP);
        if (errorHandled) return;
        IdentidadGeneros = responseHTTP.Response;
    }

    private void IdentidadGeneroChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            Patient.IdentidadGeneroId = selectedId;
        }
    }

    private async Task LoadSexoAsignado()
    {
        var responseHTTP = await _repository.GetAsync<List<SexoAsignado>>($"api/v1/sexoasignados/loadCombo");
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHTTP);
        if (errorHandled) return;
        SexoAsignados = responseHTTP.Response;
    }

    private void SexoAsignadoChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            Patient.SexoAsignadoId = selectedId;
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