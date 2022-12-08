using Microsoft.AspNetCore.Components;
using Generator.Components.Enums;
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
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor.Extensions;
using System.Globalization;
using Generator.Components.Extensions;

namespace Generator.Components.Components;
public  class GenTextField : MudTextField<object>,  IGenTextField
{
    #region NonParams
    private Dictionary<string, object> Parameters { get; set; }

    private Type DataType => Model.GetType();

    public ComponentType ComponentType { get; set; }

    //public GenTextField ComponentRef { get; set; }
    #endregion

    #region CascadingParameters
    [CascadingParameter(Name = nameof(ParentComponent))]
    public GenGrid ParentComponent { get; set; }
    #endregion

    #region Parameters
    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
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

    //Gridin basinda cagirdigimiz icin buraya duser, tabi model null oldugu icin renderlemiyoruz
    protected override void BuildRenderTree(RenderTreeBuilder __builder)
    {
        if (Model is not null && ParentComponent is not null && Value is not null)
        {

                base.BuildRenderTree(__builder);
        }
            
    }

  

    protected override Task OnInitializedAsync()
    {
        //if (!ValueChanged.HasDelegate)
        //{
        //    //ComponentRef = this;

        //    Value = Model.GetPropertyValue(BindingField);
        //    ValueChanged = EventCallback.Factory.Create<object>(this, x => OnValueChanged(x));
        //    //SetSpecialScenarios();
        //}


        ParentComponent?.AddChildComponent(this);

        return Task.CompletedTask;
        //return base.OnInitializedAsync();
        //return Task.CompletedTask;
    }


    //private void SetSpecialScenarios()
    //{
    //    //TODO
    //    //if (typeof(BindingType) == typeof(DateTime) || typeof(BindingType) == typeof(Nullable<DateTime>) && string.IsNullOrEmpty(Format))
    //    if (InputType == InputType.Date)
    //    {
    //        Converter = DateConverter;
    //        Culture = CultureInfo.GetCultureInfo("en-US");
    //        Format = "yyyy-MM-dd";
    //    }
    //    else
    //        Converter = StringConverter;
    //}
     

    //public void Render(object objectModel,  ComponentType componentType, params (string key, object value)[] AdditionalParameters) => (builder) =>
    //{
    //    if (objectModel is null || ParentComponent is null) return;

    //    //Model = model;

    //    SetSpecialScenarios();


    //    if (componentType == ComponentType.Grid)
    //    {
    //        builder.OpenComponent(0, typeof(GridLabel));
    //        builder.AddAttribute(1, nameof(GridLabel.Value), Model.GetPropertyValue(BindingField));
    //        builder.CloseComponent();
    //        return;
    //    }

    //    var properties = this.GetType().GetProperties()
    //                     .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(ParameterAttribute))
    //                                                             && x.Name != "UserAttributes");

    //    builder.OpenComponent(0, typeof(GenTextField));

    //    foreach (var property in properties.Select((val, index) => (val, index)))
    //    {
    //        var propName = property.val.Name;

    //        var value = this.GetPropertyValue(propName);

    //        if (value is null) continue;

    //        var index = property.index + 1;            

    //        builder.AddAttribute(index, propName, value);

    //    }

    //    builder.AddAttribute(201, nameof(GenTextField.Model), objectModel);
    //    builder.AddAttribute(201, nameof(GenTextField.Value), objectModel.GetPropertyValue(BindingField));

    //    builder.AddAttribute(199, nameof(GenTextField.ValueChanged), EventCallback.Factory.Create<object>(ParentComponent, (x) => { OnValueChanged(x); return; }));


    //    foreach (var additional in AdditionalParameters)
    //    {
    //        builder.AddAttribute(201, additional.key, additional.value);
    //    }

    //    //builder.AddComponentReferenceCapture(300, (value) => this.ComponentRef = (GenTextField)value);

    //    builder.CloseComponent();

    //};


    #region Events 
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
        SetFunc = value =>  Convert.ToDateTime(value).ToString("yyyy-MM-dd"),
        GetFunc = text => Convert.ToDateTime(text)
        
    };


    [EditorBrowsable(EditorBrowsableState.Never)]
    public new string Text => Model.GetPropertyValue(BindingField).ToString();

    public  void OnValueChanged(object value)
    {
        
        Model.SetPropertyValue(BindingField, value);

        //ComponentRef?.Model.SetPropertyValue(BindingField, value);
        //Model?.SetPropertyValue(BindingField, value);

        //Value = Model.GetPropertyValue(BindingField);
        //ComponentRef.Value = ComponentRef.Model.GetPropertyValue(BindingField);

        //Console.WriteLine(Model.ToString());
    }

    public RenderFragment RenderComponent(object model, ComponentType componentType) => (builder) => {

        if (InputType == InputType.Date)
        {
            Converter = DateConverter;
            Culture = CultureInfo.GetCultureInfo("en-US");
            Format = "yyyy-MM-dd";
        }
        else
            Converter = StringConverter;


        Model = model;
        
        ValueChanged = EventCallback.Factory.Create<object>(this, x => OnValueChanged(x));

        this.Render(model,builder, componentType);
    };
     
   

        #endregion



    }





