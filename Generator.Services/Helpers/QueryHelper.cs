using System;
using Newtonsoft.Json.Linq;

namespace Generator.Service.Helpers;

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
