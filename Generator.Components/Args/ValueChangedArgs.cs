namespace Generator.Components.Args;

public class ValueChangedArgs<TValueType> : EventArgs
{
    public ValueChangedArgs( object model,TValueType oldValue, TValueType newValue, bool isSearchField)
    {
        OldValue = oldValue;
        Model = model;
        NewValue = newValue;
        IsSearchField = isSearchField;
    }

    public object Model { get; set; }

    public TValueType NewValue { get; set; }

    public TValueType OldValue { get; set; }

    public bool IsSearchField { get; set; }
    
}