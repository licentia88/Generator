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

    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public new bool BoolValue => (bool)Model.GetPropertyValue(BindingField);


    protected void OnValueChanged(bool value)
    {

        var index = ParentComponent.DataSource.FindIndexByHash(Model);

        Model.SetPropertyValue(BindingField, value);

        //ParentComponent.DataSource = ParentComponent.DataSource.Replace(index.Value, Model);
        Checked = value;
        
    }

    //public void OnCheckChanged(bool value)
    //{
    //    Model.SetPropertyValue(BindingField, value);
    //}

    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => (builder) =>
    {

        Model = model;


        CheckedChanged = EventCallback.Factory.Create<bool>(this, x => OnValueChanged(x));

        bool val = (bool)model.GetPropertyValue(BindingField);
        //, (nameof(BoolValue), val)
        this.RenderComponent(model, builder,ignoreLabels, (nameof(Checked), val));
    };


    public RenderFragment RenderAsGridComponent(object model) => (builder) =>
    {
        Model = model;
        var val = Model.GetPropertyValue(BindingField) as bool?;

        var gridValue = val  == true ? TrueText : FalseText;

        this.RenderGrid(model, builder, gridValue);
    };
}





