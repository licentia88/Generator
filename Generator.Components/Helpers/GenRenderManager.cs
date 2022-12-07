using System;
using Generator.Components.Components;
using Generator.Components.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace Generator.Components.Helpers
{
	public class GenRenderManager<T> where T: MudComponentBase
	{
 

        //public RenderFragment Render(object model, MudComponentBase parent, ComponentType componentType, params (string key, object value)[] AdditionalParameters) => (builder) =>
        //{
        //    if (model is null) return;

             

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

        //    builder.AddAttribute(201, nameof(GenTextField.Value), Model.GetPropertyValue(BindingField));

        //    builder.AddAttribute(199, nameof(GenTextField.ValueChanged), EventCallback.Factory.Create<object>(parent, (x) => { OnValueChanged(x); return; }));


        //    foreach (var additional in AdditionalParameters)
        //    {
        //        builder.AddAttribute(201, additional.key, additional.value);
        //    }

        //    builder.AddComponentReferenceCapture(300, (value) => this.ComponentRef = (GenTextField)value);

        //    builder.CloseComponent();

        //};

    }
}

