using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Regix.AppFront.Helpers;
using Regix.Domain.EntitiesGen;
using Regix.Domain.EntitiesSoft;
using Regix.HttpServices;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Regix.AppFront.Pages.EntitiesSoft.Patient2Page;

public partial class FormPatient2
{
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;

    [Parameter, EditorRequired] public Patient2 Patient2 { get; set; } = null!;
    [Parameter, EditorRequired] public EventCallback OnSubmit { get; set; }
    [Parameter, EditorRequired] public EventCallback ReturnAction { get; set; }
    [Parameter, EditorRequired] public EventCallback ExitAction { get; set; }

    private DateTime? DateMin = new DateTime(1900, 1, 1);

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