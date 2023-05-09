using Generator.Components.Components;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
//using Generator.Shared.Extensions;
//using Generator.Shared.Models;

namespace Generator.Components.Args;

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

    public Dictionary<string, object> WhereStatements => Components.Where(x=> x.BindingField is not null).ToDictionary(component => component.BindingField,
                                                                          component => component.Model.GetPropertyValue(component.BindingField));

}



//public List<WhereStatement> WhereStatements => Components?
//                                                   .Where(x=> x is not GenSpacer)
//                                                    .Select(x => new WhereStatement(x.BindingField, x.Model?.GetPropertyValue(x.BindingField))).ToList();
     

 