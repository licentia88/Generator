//using Generator.Shared.Extensions;
//using Generator.Shared.Models;

namespace Generator.Components.Args;

public class GenArgs<TModel> : EventArgs //  where TModel:class
{
    public TModel CurrentValue { get; set; }

    public TModel OldValue { get; set; }

    public int Index { get; set; }

    public GenArgs()
    {

    }
    public GenArgs(TModel currentValue, TModel oldValue)
    {
        CurrentValue = currentValue;

        OldValue = oldValue;
    }

    public GenArgs(TModel currentValue,TModel oldValue, int index) 
    {
        CurrentValue = currentValue;

        OldValue = oldValue;

        Index = index;
    }



}





//public List<WhereStatement> WhereStatements => Components?
//                                                   .Where(x=> x is not GenSpacer)
//                                                    .Select(x => new WhereStatement(x.BindingField, x.Model?.GetPropertyValue(x.BindingField))).ToList();


