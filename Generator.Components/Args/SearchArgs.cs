using Generator.Components.Components;
using Generator.Components.Interfaces;
//using Generator.Shared.Extensions;
//using Generator.Shared.Models;

namespace Generator.Components.Args;

 

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

    public Dictionary<string, object> WhereStatements =>
                                     Components.Where(x=> x.BindingField is not null && x is not GenSpacer)
                                     .ToDictionary(component => component.BindingField, component => component.GetSearchValue());

}



//public List<WhereStatement> WhereStatements => Components?
//                                                   .Where(x=> x is not GenSpacer)
//                                                    .Select(x => new WhereStatement(x.BindingField, x.Model?.GetPropertyValue(x.BindingField))).ToList();
     

 