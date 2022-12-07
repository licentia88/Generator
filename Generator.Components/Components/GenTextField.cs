using Microsoft.AspNetCore.Components;
using Generator.Components.Enums;
using Generator.Components.Extensions;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Generator.Shared.Extensions;
using System.ComponentModel;
using Generator.Shared.Models;
using System;
using Generator.Components.Interfaces;
using System.Reflection;
using System.Diagnostics;

namespace Generator.Components.Components;


public partial class GenTextField : MudTextField<object>, IGenCompRenderer
{
    #region NonParams
    private Dictionary<string, object> Parameters { get; set; }

    private Type DataType => Model.GetType();

    public ComponentType ComponentType { get; set; }

    public GenTextField ComponentRef { get; set; }
    #endregion

    #region CascadingParameters
    [CascadingParameter(Name = nameof(ParentComponent))]
    public GenGrid ParentComponent { get; set; }
    #endregion

    #region Parameters
    [Parameter]
    public object Model { get; set; }

    [Parameter]
    [EditorRequired()]
    public string BindingField { get; set; }

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
    #endregion

    public GenTextField()
    {
        Parameters = new();
    }

    
    protected override Task OnInitializedAsync()
    {

        if (!ValueChanged.HasDelegate)
        {
            ComponentRef = this;

            Value = Model.GetPropertyValue(BindingField);
            ValueChanged = EventCallback.Factory.Create<object>(this, x => OnValueChanged(x));
            SetSpecialScenarios();
        }


        ParentComponent?.AddChildComponent(this);
        return base.OnInitializedAsync();
        //return Task.CompletedTask;
    }


    private void SetSpecialScenarios()
    {
        //TODO
        //if (typeof(BindingType) == typeof(DateTime) || typeof(BindingType) == typeof(Nullable<DateTime>) && string.IsNullOrEmpty(Format))
        if (InputType == InputType.Date)
        {
            Converter = DateConverter;
            Format = "yyyy-MM-dd";
        }
        else
            Converter = StringConverter;
    }

    public RenderFragment Render(object model, ComponentBase parent, ComponentType componentType, params (string key, object value)[] AdditionalParameters) => (builder) =>
    {
        if (model is null) return;

        Model = model;

        SetSpecialScenarios();


        if (componentType == ComponentType.Grid)
        {
            builder.OpenComponent(0, typeof(GridLabel));
            builder.AddAttribute(1, nameof(GridLabel.Value), Model.GetPropertyValue(BindingField));
            builder.CloseComponent();
            return;
        }

        var properties = this.GetType().GetProperties()
                         .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(ParameterAttribute))
                                                                 && x.Name != "UserAttributes");

        builder.OpenComponent(0, typeof(GenTextField));

        foreach (var property in properties.Select((val, index) => (val, index)))
        {
            var propName = property.val.Name;

            var value = this.GetPropertyValue(propName);

            if (value is null) continue;

            var index = property.index + 1;

            

            builder.AddAttribute(index, propName, value);

        }

        builder.AddAttribute(201, nameof(GenTextField.Value), Model.GetPropertyValue(BindingField));

        builder.AddAttribute(199, nameof(GenTextField.ValueChanged), EventCallback.Factory.Create<object>(parent, (x) => { OnValueChanged(x); return; }));


        foreach (var additional in AdditionalParameters)
        {
            builder.AddAttribute(201, additional.key, additional.value);
        }

        builder.AddComponentReferenceCapture(300, (value) => this.ComponentRef = (GenTextField)value);

        builder.CloseComponent();

    };


    #region Events and Customs
    private Converter<object> StringConverter = new Converter<object>
    {
        SetFunc = value =>
        {
            return value?.ToString();
        },
        GetFunc = text =>
        {
            return text?.ToString();
        }
    };

    private Converter<object> DateConverter = new Converter<object>
    {
        SetFunc = value =>
        {
           return  value?.ToString();
        },
        GetFunc = text =>
        {
            return Convert.ToDateTime(text);
        }
    };

    public virtual void OnValueChanged(object value)
    {
        ComponentRef?.Model.SetPropertyValue(BindingField, value);
        Model?.SetPropertyValue(BindingField, value);
    }

    #endregion



}





