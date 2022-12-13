using System.Data.Common;
using Cysharp.Text;

namespace Generator.Server.Helpers;

public class QueryHelper
{
    public string Select(string TableName)
    {
        return Select(TableName, "*");
    }

    public string Select(string TableName, string fields)
    {
        return $"SELECT {fields} FROM {TableName}";
    }

    //public string Insert(string TableName
}
