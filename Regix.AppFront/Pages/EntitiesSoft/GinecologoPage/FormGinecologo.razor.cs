using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Regix.AppFront.Helpers;
using Regix.Domain.EntitiesGen;
using Regix.Domain.EntitiesSoft;
using Regix.HttpServices;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Regix.AppFront.Pages.EntitiesSoft.GinecologoPage;

public partial class FormGinecologo
{
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;

    [Parameter, EditorRequired] public Ginecologico Ginecologico { get; set; } = null!;
    [Parameter, EditorRequired] public EventCallback OnSubmit { get; set; }
    [Parameter, EditorRequired] public EventCallback ReturnAction { get; set; }

    private DateTime? DateMin = new DateTime(2000, 1, 1);
    private List<Regular>? Regulars;
    private List<Anticonception>? Anticonceptions;

    protected override async Task OnInitializedAsync()
    {
        await LoadRegular();
        await LoadAnticonceptivo();
    }

    private async Task LoadRegular()
    {
        var responseHTTP = await _repository.GetAsync<List<Regular>>($"api/v1/regulars/loadCombo");
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHTTP);
        if (errorHandled) return;
        Regulars = responseHTTP.Response;
    }

    private void RegularChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            Ginecologico.RegularId = selectedId;
        }
    }

    private async Task LoadAnticonceptivo()
    {
        var responseHTTP = await _repository.GetAsync<List<Anticonception>>($"api/v1/anticonceptions/loadCombo");
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHTTP);
        if (errorHandled) return;
        Anticonceptions = responseHTTP.Response;
    }

    private void AnticonceptivoChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            Ginecologico.AnticonceptionId = selectedId;
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