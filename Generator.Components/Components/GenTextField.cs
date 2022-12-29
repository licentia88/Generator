using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Generator.Components.Validators;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Generator.Components.Components;

public class GenTextField : MudTextField<object>, IGenTextField
{
    ObjectValidator<GenTextField> ObjectValidator = new ObjectValidator<GenTextField>();

    public Type DataType { get; set; }

    public object GetDefaultValue => DataType.GetDefaultValue();

    [CascadingParameter(Name = nameof(ParentComponent))]
    public GenGrid ParentComponent { get; set; }

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }

    [Parameter]
    [EditorRequired]
    public string BindingField { get; set; }

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

    //Gridin basinda cagirdigimiz icin buraya duser, tabi model null oldugu icin renderlemiyoruz
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Model is not null )
            base.BuildRenderTree(builder);

    }
  
    protected override  Task OnInitializedAsync()
    {
       
        if (InputType == InputType.Date)
        {
            Converter = _dateConverter;
            Culture = CultureInfo.GetCultureInfo("en-US");
            Format = "yyyy-MM-dd";
            DataType = typeof(DateTime);
        }
        else
        {
            Converter = _stringConverter;
            DataType = typeof(string);
        }

        ParentComponent?.AddChildComponent(this);

        return Task.CompletedTask;
    }

    private Converter<object> _stringConverter = new()
    {
        SetFunc = value => value?.ToString(),
        GetFunc = text => text?.ToString()
    };

    private Converter<object> _dateConverter = new()
    {
        SetFunc = value => Convert.ToDateTime(value).ToString("yyyy-MM-dd"),
        GetFunc = text =>
        {
            DateTime.TryParse(text, out var dataToReturn);
            return dataToReturn;
        }
    };

    [EditorBrowsable(EditorBrowsableState.Never)]
    public new string Text => Model.GetPropertyValue(BindingField).ToString();

    public void OnValueChanged(object value)
    {
        Model.SetPropertyValue(BindingField, value);

        ObjectValidator.Validate(this);
    }

    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => (builder) =>
    {
        Model = model;

        ValueChanged = EventCallback.Factory.Create<object>(this, OnValueChanged);

        var loValue = model.GetPropertyValue(BindingField);

        builder.RenderComponent(this,ignoreLabels, (nameof(Value), loValue));
    };

    public RenderFragment RenderAsGridComponent(object model) => (builder) =>
    {
        var data = model.GetPropertyValue(BindingField);

        if (data is DateTime dt)
        {
            data = dt.ToString(Format);
        }

        RenderExtensions.RenderGrid(builder, data);
    };
}






