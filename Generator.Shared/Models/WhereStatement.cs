using System;
namespace Generator.Shared.Models;

public class WhereStatement
{
    public WhereStatement()
    {

    }

    public WhereStatement(string propertyName, object propertyValue)
    {
        PropertyName = propertyName;
        AddPropertyValue(propertyValue);
    }

    public WhereStatement(string propertyName, object[] propertyValues)
    {
        PropertyName = propertyName;
        PropertyValues = propertyValues.ToList();
    }

    public string PropertyName { get; set; }

    public IList<object> PropertyValues { get; set; } = new List<object>();

    private void AddPropertyValue(object value)
    {
        PropertyValues.Add(value);
    }

    public void RemovePropertyValue(object value)
    {
        PropertyValues.Remove(value);
    }

    public WhereStatement[] CreateWhereStatementCollection(IDictionary<string, object> Model)
    {
        return WhereStatement.CreateStatementCollection(Model);
    }

    public static WhereStatement[] CreateStatementCollection(IDictionary<string,object> Model)
    {
       return  Model.Select((KeyValuePair<string, object> arg) => new WhereStatement(arg.Key, arg.Value)).ToArray();
    }

}

