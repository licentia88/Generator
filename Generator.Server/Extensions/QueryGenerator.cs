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
}

