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

namespace Generator.Components.Components;

 
public partial class GenTextField: MudTextField<object>, IGenCompRenderer
{
    

    private Dictionary<string, object> Parameters { get; set; }

    private Type DataType => Context.GetType();

    public ComponentType ComponentType { get; set; }

    [Parameter]
    public object Context { get; set; }

    [Parameter]
    public string BindingField { get; set; }
    //[CascadingParameter(Name = nameof(ParentComponent))]
    //public GenGrid ParentComponent { get; set; }


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

    public GenTextField()
    {
        Parameters = new();
    }


    //private void SetParameterValues()
    //{
    //    if (Value is DateTime || Value is Nullable<DateTime>)
    //    {
    //        Format = "yyyy-MM-dd";
    //        Converter = DateConverter;
    //    }
    //    else
    //        Converter = StringConverter;
    //}

    


    public GenTextField ComponentRef { get; set; }

    public RenderFragment Render(object Model, ComponentBase parent, ComponentType componentType, params (string key, object value)[] AdditionalParameters) => (builder) =>
    {

        if (Model is null) return;


        Context = Model;

        if(componentType == ComponentType.Grid)
        {
            builder.OpenComponent(0, typeof(GridLabel));
            builder.AddAttribute(1, nameof(GridLabel.Value), Context.GetPropertyValue(BindingField));
            builder.CloseComponent();
            return;
        }

        var properties = this.GetType().GetProperties()
                         .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(ParameterAttribute))
                                                                 && x.Name != "UserAttributes");

        builder.OpenComponent(0, typeof(GenTextField));

        foreach (var property in properties.Select((val, index) => (val, index)))
        {
            var index = property.index + 1;

            var propName = property.val.Name;

            var value = this.GetPropertyValue(propName);

            builder.AddAttribute(index, propName, value);

        }

        builder.AddAttribute(199, nameof(GenTextField.ValueChanged), EventCallback.Factory.Create<object>(parent, (x) =>
        {
            ComponentRef.Context.SetPropertyValue(BindingField, x);
            //ComponentRef.Context.SetPropertyValue(BindingField, x);
            return;
        }));
        builder.AddAttribute(200, nameof(GenTextField.Context), Model);
        builder.AddAttribute(201, nameof(GenTextField.Value), Context.GetPropertyValue(BindingField));
        builder.AddComponentReferenceCapture(999, (value) => {
            #nullable restore
            this.ComponentRef = (GenTextField)value;
        #line default
        #line hidden
        #nullable disable
        });

        foreach (var additional in AdditionalParameters)
        {
            builder.AddAttribute(201, additional.key, additional.value);
           
        }
 
        builder.CloseComponent();

    };

     
    //public void ValueChangedEvent(object obj)
    //{
    //    ComponentRef.Context.SetPropertyValue(BindingField, obj);
    //}
    
    Converter<object> StringConverter = new Converter<object>
    {
        SetFunc = value => value.ToString(),
        GetFunc = text => text.ToString(),
    };

    Converter<object> DateConverter = new Converter<object>
    {
        SetFunc = value => value.ToString(),
        GetFunc = text => Convert.ToDateTime(text),
    };

     
}
 
   

 

