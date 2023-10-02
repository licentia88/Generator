using Generator.Components.Components;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
//using Generator.Shared.Extensions;
//using Generator.Shared.Models;

namespace Generator.Components.Args;

 public class ComponentArgs<TModel> : EventArgs
{
    public object Model;

    public Dictionary<string,TModel> DictionaryModel { get; set; }

    private bool IsSearchField { get; set; }

    public TModel SourceValue { get; set; }

    public ComponentArgs(object model, TModel sourceValue)
    {
        Model = model;
        SourceValue = sourceValue;
        IsSearchField = false;
    }

    public ComponentArgs(Dictionary<string, TModel> dictionaryModel ,TModel sourceValue)
    {
        DictionaryModel = dictionaryModel;
        SourceValue = sourceValue;
        IsSearchField = true;
    }


    public T GetValueAs<T>(string bindingField)
    {
        object value;

        if (IsSearchField)
            value = DictionaryModel[bindingField];
        else
            value = Model.GetPropertyValue(bindingField);

        if (value is null) return default;

        if (typeof(T).GetInterfaces().Contains(typeof(IConvertible)))
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (InvalidCastException)
            {
                // Handle conversion failure here
                return default;
            }
        }
        else
        {
            // Handle types that don't support conversion from string
            // You can throw an exception or provide a default behavior here
            return default;
        }
    }

    //public T GetValueAs<T>(string bindingField)
    //{
    //    object value;

    //    if (IsSearchField)
    //        value = DictionaryModel[bindingField];
    //    else
    //        value = Model.GetPropertyValue(bindingField);
 
    //    if (value is null) return default;


    //    T result;

    //    // Check if T supports TryParse
    //    var tryParseMethod = typeof(T).GetMethod("TryParse", new[] { typeof(string), typeof(T).MakeByRefType() });

    //    if (tryParseMethod != null)
    //    {
    //        // Call TryParse using reflection
    //        object[] parameters = { value, null };
    //        var parseResult = (bool)tryParseMethod.Invoke(null, parameters);

    //        if (parseResult)
    //        {
    //            result = (T)parameters[1];
    //        }
    //        else
    //        {
    //            return default;
    //            // Handle parsing failure here
    //            //throw new ArgumentException("Parsing failed");
    //        }
    //    }
    //    else
    //    {
    //        return value.CastTo<T>();
    //        // Handle types that don't support TryParse
    //        //throw new NotSupportedException($"Type {typeof(T).Name} does not support TryParse.");
    //    }

    //    return result;
    //}

}

public class GenArgs<TModel> : EventArgs  
{
    public TModel Model { get; set; }

    public TModel OldModel { get; set; }

    public int Index { get; set; }

    public GenArgs(TModel model,TModel oldModel, int index) 
    {
        Model = model;

        OldModel = oldModel;

        Index = index;
    }


}



public class SearchArgs:EventArgs
{
    public List<IGenComponent> Components { get; set; }

    public SearchArgs()
    {

    }

    public SearchArgs(List<IGenComponent> components)
    {
        Components = components;
    }

    public KeyValuePair<string, object>[] WhereStatements =>
                                     Components.Where(x => x.BindingField is not null && x is not GenSpacer)
                                     .Select(component => new KeyValuePair<string, object>(component.BindingField, component.GetSearchValue())).ToArray();



    public IGenComponent GetComponent(string BindingField) => Components.FirstOrDefault(x => x.BindingField == BindingField);


    public T GetComponentValueAs<T>(string bindingField)
    {
        var component = GetComponent(bindingField);

        var value = component.GetSearchValue();

        if (value is null) return default;

        T result;

        // Check if T supports TryParse
        var tryParseMethod = typeof(T).GetMethod("TryParse", new[] { typeof(T), typeof(T).MakeByRefType() });

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





//public List<WhereStatement> WhereStatements => Components?
//                                                   .Where(x=> x is not GenSpacer)
//                                                    .Select(x => new WhereStatement(x.BindingField, x.Model?.GetPropertyValue(x.BindingField))).ToList();


