using Microsoft.AspNetCore.Components;
using Generator.Components.Enums;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using System.ComponentModel;
using Microsoft.AspNetCore.Components.Rendering;
using Generator.Components.Extensions;

namespace Generator.Components.Components;

public class GenCheckBox : MudCheckBox<bool>, IGenCheckBox
{
    #region CascadingParameters
    [CascadingParameter(Name = nameof(ParentComponent))]
    public GenGrid ParentComponent { get; set; }
    #endregion

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }

    [Parameter]
    [EditorRequired()]
    public string BindingField { get; set; }

    [Parameter, EditorRequired]
    public string TrueText { get; set; }

    [Parameter,EditorRequired]
    public string FalseText { get; set; }

    public Type DataType { get; set; } = typeof(bool);

    public object GetDefaultValue => DataType.GetDefaultValue();

    [Parameter, AllowNull]
    [Range(1, 12, ErrorMessage = "Column width must be between 1 and 12")]
    public int Width { get; set; }

    [Parameter, AllowNull]
    public int Order { get; set; }

    [Parameter, AllowNull]
    public bool VisibleOnEdit { get; set; } = true;

    [Parameter, AllowNull]
    public bool VisibleOnGrid { get; set; } = true;

    [Parameter, AllowNull]
    public bool EnabledOnEdit { get; set; } = true;

    [Parameter]
    public int xs { get; set; }

    [Parameter]
    public int sm { get; set; }

    [Parameter]
    public int md { get; set; }

    [Parameter]
    public int lg { get; set; }

    [Parameter]
    public int xl { get; set; }

    [Parameter]
    public int xxl { get; set; }

    protected override Task OnInitializedAsync()
    {
        //Converter = BoolConverter;
        ParentComponent?.AddChildComponent(this);

        return Task.CompletedTask;
    }

    protected override void BuildRenderTree(RenderTreeBuilder __builder)
    {
        if (Model is not null && ParentComponent is not null)
        {
            base.BuildRenderTree(__builder);
        }
    }
    protected override Task OnChange(ChangeEventArgs args)
    {
        Model.SetPropertyValue(BindingField, args.Value);
        return base.OnChange(args);
    }

    //public void OnCheckChanged(bool value)
    //{
    //    Model.SetPropertyValue(BindingField, value);
    //}

    public RenderFragment RenderComponent(object model, ComponentType componentType) => (builder) => {

        Model = model;

        if(componentType == ComponentType.Form)
        {
            this.RenderComponent(model, builder, componentType, (nameof(Checked), model.GetPropertyValue(BindingField)));
            return;
        }

        var gridValue = Model.GetPropertyValue(BindingField).CastTo<bool>() ? TrueText : FalseText;
        this.RenderGrid(model, builder, gridValue);

    };

    

}





