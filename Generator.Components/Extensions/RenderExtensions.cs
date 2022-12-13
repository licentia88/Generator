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

       
        public static void RenderGrid<T>(this T component, object objectModel, RenderTreeBuilder builder ,object value) where T : IGenComponent
        {
            builder.OpenComponent(0, typeof(GridLabel));
            builder.AddAttribute(1, nameof(GridLabel.Value), value);
            builder.CloseComponent();
        }

        public static void RenderComponent<T>(this T component, object objectModel,RenderTreeBuilder builder, ComponentType componentType, params (string key, object value)[]  AdditionalParameters) where T:IGenComponent
        {
            if (objectModel is null || component.ParentComponent is null) return;
 
            var properties = component.GetType().GetProperties()
                             .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(ParameterAttribute))
                                                                     && x.Name != "UserAttributes");

            builder.OpenComponent(0, typeof(T));

            foreach (var property in properties.Select((val, index) => (val, index)))
            {
                var propName = property.val.Name;

                var value = component.GetPropertyValue(propName);

                if (value is null) continue;

                var index = property.index + 1;

                builder.AddAttribute(index, propName, value);

            }

            builder.AddAttribute(201, nameof(IGenComponent.Model), objectModel);

 

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

