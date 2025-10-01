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
                                     .Select(component => new KeyValuePair<string, object>(component.BindingField, component.GetValue())).ToArray();


    private Dictionary<string, object> WhereStatementDictionary => WhereStatements.ToDictionary(kv => kv.Key, kv => kv.Value);
    
     

    public T GetComponentValueAs<T>(string bindingField) where T :IParsable<T>
    {
        if (!WhereStatementDictionary.TryGetValue(bindingField, out object value)) 
            return default;
        
        T.TryParse(value?.ToString(), null, out var val);
        
        return val;
    }

}



 

