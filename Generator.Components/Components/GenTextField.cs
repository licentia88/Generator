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
using System;
using Generator.Shared.Models.ComponentModels;

namespace Generator.Components.Components;
public  class GenTextField : MudTextField<object>,  IGenTextField
{
    #region NonParams
    private Dictionary<string, object> Parameters { get; set; }

    public Type DataType { get; set; } 

    public object GetDefaultValue => DataType.GetDefaultValue();

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
        if (Model is not null && ParentComponent is not null  )
        {
            base.BuildRenderTree(__builder);
        }
     }

    

    protected override Task OnInitializedAsync()
    {
        if (InputType == InputType.Date)
        {
            Converter = DateConverter;
            Culture = CultureInfo.GetCultureInfo("en-US");
            Format = "yyyy-MM-dd";
            DataType = typeof(DateTime);
        }
        else
        {
            Converter = StringConverter;
            DataType = typeof(string);
        }

        ParentComponent?.AddChildComponent(this);

        return Task.CompletedTask;
      
    }


  

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
        SetFunc = value => {


            //DateTime.TryParse(value?.ToString(), out var dataToReturn);

            //return dataToReturn.ToString("yyyy-MM-dd");
            return Convert.ToDateTime(value).ToString("yyyy-MM-dd");

        },
        GetFunc = text =>
        {
            DateTime.TryParse(text, out var dataToReturn);
            return dataToReturn;
        }
        
    };


    [EditorBrowsable(EditorBrowsableState.Never)]
    public new string Text => Model.GetPropertyValue(BindingField).ToString();


    public  void OnValueChanged(object value)
    {
         //ParentComponent.SelectedItem
         Model.SetPropertyValue(BindingField, value);
    }


    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => (builder) =>
    {
        Model = model;

        ValueChanged = EventCallback.Factory.Create<object>(this, OnValueChanged);

        var loValue = model.GetPropertyValue(BindingField);

        builder.RenderComponent(new RenderParameters<GenTextField>(this,model,ignoreLabels),(nameof(Value), loValue));

    };

    public RenderFragment RenderAsGridComponent(object model) => (builder) => {
         
        var data = model.GetPropertyValue(BindingField);

        if(data is DateTime dt)
        {
            data = dt.ToString(Format);
        }

        RenderExtensions.RenderGrid(builder, data);

    };



    #endregion



}





