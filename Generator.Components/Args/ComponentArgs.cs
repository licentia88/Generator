using Generator.Components.Extensions;
//using Generator.Shared.Extensions;
//using Generator.Shared.Models;

namespace Generator.Components.Args;

public class ComponentArgs<TModel> : EventArgs
{
    public object Model;

    //public Dictionary<string,TModel> DictionaryModel { get; set; }

    private bool IsSearchField { get; set; }

    public TModel SourceValue { get; set; }

    public ComponentArgs(object model, TModel sourceValue, bool isSearchField )
    {
        Model = model;
        SourceValue = sourceValue;
        IsSearchField = isSearchField;
    }

    //public ComponentArgs(Dictionary<string, TModel> dictionaryModel ,TModel sourceValue)
    //{
    //    DictionaryModel = dictionaryModel;
    //    SourceValue = sourceValue;
    //    IsSearchField = true;
    //}

  

    // public T GetValueAs<T>(string bindingField)
    // {
    //     object value;
    //
    //     //if (IsSearchField)
    //     //    value = DictionaryModel[bindingField];
    //     //else
    //     value = Model.GetPropertyValue(bindingField);
    //
    //     if (value is null) return default;
    //
    //     if (typeof(T).GetInterfaces().Contains(typeof(IConvertible)))
    //     {
    //         try
    //         {
    //             return (T)Convert.ChangeType(value, typeof(T));
    //         }
    //         catch (InvalidCastException)
    //         {
    //             // Handle conversion failure here
    //             return default;
    //         }
    //     }
    //     else
    //     {
    //         // Handle types that don't support conversion from string
    //         // You can throw an exception or provide a default behavior here
    //         return default;
    //     }
    // }

     
}





//public List<WhereStatement> WhereStatements => Components?
//                                                   .Where(x=> x is not GenSpacer)
//                                                    .Select(x => new WhereStatement(x.BindingField, x.Model?.GetPropertyValue(x.BindingField))).ToList();


