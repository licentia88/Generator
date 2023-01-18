using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using System.ComponentModel;
using Microsoft.AspNetCore.Components.Rendering;
using Generator.Components.Extensions;
using Generator.Components.Validators;

namespace Generator.Components.Components;

public class GenCheckBox : MudCheckBox<bool>, IGenCheckBox
{
    #region CascadingParameters

    [CascadingParameter(Name = nameof(ParentComponent))]
    public INonGenGrid ParentComponent { get; set; }

    #endregion CascadingParameters

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }

    [Parameter]
    [EditorRequired()]
    public string BindingField { get; set; }

    [Parameter, EditorRequired]
    public string TrueText { get; set; }

    [Parameter, EditorRequired]
    public string FalseText { get; set; }

    public Type DataType { get; set; } = typeof(bool);

    public object GetDefaultValue => DataType.GetDefaultValue();

    [Parameter]
    [Range(1, 12, ErrorMessage = "Column width must be between 1 and 12")]
    public int Width { get; set; }

    [Parameter]
    public int Order { get; set; }

    [Parameter]
    public bool VisibleOnEdit { get; set; } = true;

    [Parameter]
    public bool VisibleOnGrid { get; set; } = true;

    [Parameter]
    public bool EnabledOnEdit { get; set; } = true;

    [Parameter]
    public int xs { get; set; } = 12;

    [Parameter]
    public int sm { get; set; } = 12;

    [Parameter]
    public int md { get; set; } = 12;

    [Parameter]
    public int lg { get; set; } = 12;

    [Parameter]
    public int xl { get; set; } = 12;

    [Parameter]
    public int xxl { get; set; } = 12;

    //bool HasErrors { get; set; } 

    protected override Task OnInitializedAsync()
    {
        ParentComponent?.AddChildComponent(this);

        return Task.CompletedTask;
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Model is not null )
        {
            base.BuildRenderTree(builder);
        }
    }

    private void OnValueChanged(bool value)
    {


        Model.SetPropertyValue(BindingField, value);

        Checked = value;
    }

    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => (builder) =>
    {
        Model = model;

        CheckedChanged = EventCallback.Factory.Create<bool>(this, OnValueChanged);

        var val = (bool)model.GetPropertyValue(BindingField);


        builder.RenderComponent(this, ignoreLabels, (nameof(Checked), val));
    };

    public RenderFragment RenderAsGridComponent(object model) => (builder) =>
    {
        var val = model.GetPropertyValue(BindingField) as bool?;

        var gridValue = val == true ? TrueText : FalseText;

        RenderExtensions.RenderGrid(builder, gridValue);
    };

    public Task ValidateObject()
    {
        return Task.CompletedTask;
    }
}






