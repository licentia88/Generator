using Generator.Components.Components;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Generator.Components.Extensions
{
    public class RenderParameters<TGenComponent> where TGenComponent : IGenComponent
    {
        public RenderParameters(TGenComponent component, object objectModel, bool ignoreLabels)
        {
            Component = component;
            ObjectModel = objectModel;
            IgnoreLabels = ignoreLabels;
        }

        public TGenComponent Component { get; }
        public object ObjectModel { get; }
        public bool IgnoreLabels { get; }
    }

    public static class RenderExtensions
	{

       
        public static void RenderGrid(RenderTreeBuilder builder ,object value)
        {
            builder.OpenComponent(0, typeof(GridLabel));
            builder.AddAttribute(1, nameof(GridLabel.Value), value);
          
            builder.CloseComponent();
        }

        public static void RenderComponent<T>(this RenderTreeBuilder builder, RenderParameters<T> renderParameters, params (string Key, object Value)[] AdditionalParams) where T:IGenComponent
        {
            if (renderParameters.ObjectModel is null || renderParameters.Component.ParentComponent is null) return;
 
            var properties = renderParameters.Component.GetType().GetProperties()
                             .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(ParameterAttribute))
                                                                     && x.Name != "UserAttributes");

            if (renderParameters.IgnoreLabels)
            {
                properties = properties.Where(x => !x.Name.Equals(nameof(IGenComponent.Label)));
            }

            var i = 1;
            builder.OpenComponent(0, typeof(T));

            foreach (var property in properties)
            {
                var propName = property.Name;

                var value = renderParameters.Component.GetPropertyValue(propName);

                if (value is null) continue;

                var index = i;

                builder.AddAttribute(index, propName, value);

                i++;
            }

            builder.AddAttribute(i++, nameof(IGenComponent.Model), renderParameters.ObjectModel);

 


            foreach (var additional in AdditionalParams)
            {
                builder.AddAttribute(i, additional.Key, additional.Value);
                i++;
            }

            //builder.AddComponentReferenceCapture(300, (value) => this.ComponentRef = (GenTextField)value);

            //builder.AddComponentReferenceCapture(300, (value) => this.ComponentRef = (GenTextField)value);

            builder.CloseComponent();

        }
         
}
 
}

