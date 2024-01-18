using Generator.Components.Components;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
//using Generator.Shared.Extensions;
//using Generator.Shared.Models;

namespace Generator.Components.Args;



public class SearchArgs:EventArgs
{
    public List<IGenControl> Components { get; set; }

    public SearchArgs()
    {

    }

    public SearchArgs(List<IGenControl> components)
    {
        Components = components;
    }

    public KeyValuePair<string, object>[] WhereStatements =>
                                     Components.Where(x => x.BindingField is not null && x is not GenSpacer)
                                     .Select(component => new KeyValuePair<string, object>(component.BindingField, component.GetSearchValue())).ToArray();



    public IGenControl GetComponent(string BindingField) => Components.FirstOrDefault(x => x.BindingField == BindingField);


    public T GetComponentValueAs<T>(string bindingField)
    {
        var component = GetComponent(bindingField);

        var value = component.GetSearchValue();

        if (value is null) return default;

        T result;

        // Check if T supports TryParse
        var tryParseMethod = typeof(T).GetMethod("TryParse", new[] { typeof(string), typeof(T).MakeByRefType() });

        if (tryParseMethod != null)
        {
            // Call TryParse using reflection
            object[] parameters = { value, null };
            var parseResult = (bool)tryParseMethod.Invoke(null, parameters);

            if (parseResult)
            {
                result = (T)parameters[1];
            }
            else
            {
                return default;
                // Handle parsing failure here
                //throw new ArgumentException("Parsing failed");
            }
        }
        else
        {
            return value.CastTo<T>();
            // Handle types that don't support TryParse
            //throw new NotSupportedException($"Type {typeof(T).Name} does not support TryParse.");
        }

        return result;
    }

}



 

