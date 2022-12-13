using Cysharp.Text;
using Generator.Server.Helpers;
using Generator.Shared.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Generator.Server.Extensions;

public class QueryGenerator
{
    /// <summary>
    /// IdentityField dolu ise AutoIncrement dir
    /// </summary>
    /// <param name="model"></param>
    /// <param name="IdentityField"></param>
    internal static (string query, List<string> fields) InsertStatement(IDictionary<string, object> model, string TableName, bool isAutoIncrement, string IdentityField)
    {
        var fields = model.Where(x => x.Key is not null && (isAutoIncrement ? !x.Key.Equals(IdentityField):true)).Select(x => x.Key).ToList();
        var fieldsString = string.Join(", ", fields);

        var values = model.Where(x => x.Key is not null && (isAutoIncrement ? !x.Key.Equals(IdentityField) : true)).Select(x => $"@{x.Key} ").ToList();
        var valueString = string.Join(", ", values);

        var query = $" INSERT INTO {TableName} ({fieldsString}) VALUES ({valueString}) ";

        var WhereStatement = isAutoIncrement ? $" WHERE {IdentityField} = SCOPE_IDENTITY() ": $" WHERE {IdentityField} = @{IdentityField} ";

        var selectQuery = $" SELECT {IdentityField}, {fieldsString} FROM {TableName}  {WhereStatement}  ";

        return ($"{query} \n {selectQuery}", fields);
    }

    internal static (string query, List<string> fields) UpdateStatement(IDictionary<string, object> model, string TableName, string IdentityField)
    {
        var fields = model.Where(x => x.Key is not null && !x.Key.Equals(IdentityField)).Select(x => x.Key).ToList();

        var fieldsString = string.Join(", ", fields);

        var setFields = model.Where(x => x.Key is not null && !x.Key.Equals(IdentityField)).Select(x => $" {x.Key} = @{x.Key} ").ToList();

        var setString = "SET " + string.Join(", ", setFields);

        var whereStatememt = $"WHERE {IdentityField} = '{model[IdentityField]}' ";

        var query = $"UPDATE {TableName} {setString} {whereStatememt}";

        var selectQuery = $" SELECT {IdentityField}, {fieldsString} FROM {TableName}  {whereStatememt} ";

        return ($"{query} \n {selectQuery}", model.Where(x => !x.Key.Equals(IdentityField)).Select(x => x.Key).ToList());
    }

    internal static string DeleteStatement(IDictionary<string, object> model, string TableName, string IdentityField)
    {
        var whereStatememt = $"WHERE {IdentityField} = @{IdentityField}";

        var query = $"DELETE FROM {TableName} {whereStatememt}";

        return query;
    }

    internal static string CreateParametricQuery(string TableName, IDictionary<string, object> objectModel = null, params (string Key, LogicalOperator LogicalOperator, object[] Value)[] parameters)
    {
        using (var sb = ZString.CreateStringBuilder())
        {
            using (var WhereSb = ZString.CreateStringBuilder())
            {
                var fieldsList = objectModel?.Select((x) => x.Key).ToList();
                var fieldsString = fieldsList is null ? " * " : ZString.Join(", ", fieldsList);

                if (parameters.Any())
                {
                    var loParams = parameters.Select(x => $"{x.LogicalOperator.CreateStatement(x.Key, x.Value)}  AND").ToList();

                    var paramsString = ZString.Join("", loParams).TrimEnd('A', 'N', 'D');
                    WhereSb.AppendFormat("WHERE {0}", paramsString);
                }

                sb.AppendFormat("SELECT {0} FROM {1} {2}", fieldsString, TableName, WhereSb);

                var Query = sb.ToString();

                return Query;
            }
        }
    }
}

