using Generator.Components.Components;
using Generator.Components.Enums;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Generator.Components.Extensions
{
	public static class RenderExtensions
	{
        public static void Render<T>(this T component, object objectModel,RenderTreeBuilder builder, ComponentType componentType, params (string key, object value)[]  AdditionalParameters) where T:IGenComponent
        {
            if (objectModel is null || component.ParentComponent is null) return;

            //Model = model;

            //SetSpecialScenarios();


            if (componentType == ComponentType.Grid)
            {
                builder.OpenComponent(0, typeof(GridLabel));
                builder.AddAttribute(1, nameof(GridLabel.Value), objectModel.GetPropertyValue(component.BindingField));
                builder.CloseComponent();
                return;
            }

            var properties = component.GetType().GetProperties()
                             .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(ParameterAttribute))
                                                                     && x.Name != "UserAttributes");

            builder.OpenComponent(0, typeof(GenTextField));

            foreach (var property in properties.Select((val, index) => (val, index)))
            {
                var propName = property.val.Name;

                var value = component.GetPropertyValue(propName);

                if (value is null) continue;

                var index = property.index + 1;

                builder.AddAttribute(index, propName, value);

            }

            builder.AddAttribute(201, nameof(GenTextField.Model), objectModel);
            builder.AddAttribute(201, nameof(GenTextField.Value), objectModel.GetPropertyValue(component.BindingField));

            if (typeof(T).HasMethod("OnValueChanged"))
            {
            

            }
            //builder.AddAttribute(199, nameof(GenTextField.ValueChanged), EventCallback.Factory.Create<object>(component.ParentComponent, (x) => { ((dynamic)component).OnValueChanged(x); return; }));



            foreach (var additional in AdditionalParameters)
            {
                builder.AddAttribute(201, additional.key, additional.value);
            }

            //builder.AddComponentReferenceCapture(300, (value) => this.ComponentRef = (GenTextField)value);

            //builder.AddComponentReferenceCapture(300, (value) => this.ComponentRef = (GenTextField)value);

            builder.CloseComponent();

        }

    }
 
}

