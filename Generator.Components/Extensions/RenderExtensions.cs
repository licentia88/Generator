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

        public static void RenderComponent<T>(this T component, object objectModel,RenderTreeBuilder builder,bool IgnoreLabels, params (string key, object value)[]  AdditionalParameters) where T:IGenComponent
        {
            if (objectModel is null || component.ParentComponent is null) return;
 
            var properties = component.GetType().GetProperties()
                             .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(ParameterAttribute))
                                                                     && x.Name != "UserAttributes");

            if (IgnoreLabels)
            {
                properties = properties.Where(x => !x.Name.Equals(nameof(IGenComponent.Label)));
            }

            var i = 1;
            builder.OpenComponent(0, typeof(T));

            foreach (var property in properties)
            {
                var propName = property.Name;

                var value = component.GetPropertyValue(propName);

                if (value is null) continue;

                var index = i;

                builder.AddAttribute(index, propName, value);

                i++;
            }

            builder.AddAttribute(i++, nameof(IGenComponent.Model), objectModel);

 


            foreach (var additional in AdditionalParameters)
            {
                builder.AddAttribute(i, additional.key, additional.value);
                i++;
            }

            //builder.AddComponentReferenceCapture(300, (value) => this.ComponentRef = (GenTextField)value);

            //builder.AddComponentReferenceCapture(300, (value) => this.ComponentRef = (GenTextField)value);

            builder.CloseComponent();

        }
         
}
 
}

