using QueryMaker.Interfaces;

namespace QueryMaker.MSSql;

public class SqlQueryBuilder : DatabaseManager, IQueryStringBuilder
{
    public string CreateDeleteStatement(string TableName, string PrimaryKey)
    {
        var whereStatememt = $"WHERE [{PrimaryKey}] = @{PrimaryKey}";

        var query = $"DELETE FROM [{TableName}] {whereStatememt}";

        return query;
    }

    public string CreateInsertStatement(string TableName, IDictionary<string, object> Model, string PrimaryKey, bool IsAutoIncrement)
    {
        var fields = Model.Where(x => x.Key is not null).Select(x => x.Key).ToList();

        var fieldsString = string.Join(", ", fields.Select(x => $"[{x}]"));

        var valueString = string.Join(", ", fields.Select(x => $"@{x}"));

        var query = $"SET NOCOUNT ON;\n\n INSERT INTO [{TableName}] ({fieldsString}) VALUES ({valueString}) ";

        var whereClause = IsAutoIncrement ? $" [{PrimaryKey}] = SCOPE_IDENTITY() " : $" [{PrimaryKey}] = @{PrimaryKey} ";

        var selectQuery = $" \n SELECT [{PrimaryKey}], {fieldsString} FROM {TableName} WHERE @@ROWCOUNT = 1 AND {whereClause}  ";

        return query + selectQuery;
    }

    public string CreateMethodPropertyMetaDataStatement(string MethodName)
    {
        return $"SELECT P.NAME AS PARAMETER_NAME FROM SYS.PARAMETERS P INNER JOIN SYS.OBJECTS O ON O.OBJECT_ID = P.OBJECT_ID WHERE O.TYPE IN ('FN', 'IF', 'TF', 'P') AND O.NAME = @{nameof(MethodName)} ";
    }

    public string CreateStoredProcedureFieldMetaDataStatement(string StoredProcedure)
    {
        return $"SELECT NAME, SYSTEM_TYPE_NAME FROM SYS.DM_EXEC_DESCRIBE_FIRST_RESULT_SET('EXEC {StoredProcedure}', NULL, 0)";
    }

    public string CreateUpdateStatement(string TableName, IDictionary<string, object> Model, string PrimaryKey)
    {
        var fields = Model.Where(x => x.Key is not null && !x.Key.Equals(PrimaryKey)).Select(x => x.Key).ToList();

        var fieldsString = string.Join(", ", fields.Select(x => $"[{x}]"));

        var setFields = fields.Select(x => $" [{x}] = @{x} ").ToList();

        var setString = "SET " + string.Join(", ", setFields);

        var whereStatememt = $"WHERE [{PrimaryKey}] = @{PrimaryKey} ";

        var query = $"UPDATE [{TableName}] \n {setString} \n {whereStatememt} ; \n";

        var selectQuery = $" SELECT {PrimaryKey}, {fieldsString} FROM {TableName} \n {whereStatememt} ; \n";

        return query + selectQuery;
    }


    public string IsAutoIncrementStatement(string tableName)
    {
        return $"SELECT C.NAME PrimaryKeyName, C.IS_IDENTITY  FROM SYS.COLUMNS C JOIN SYS.INDEX_COLUMNS IC ON C.OBJECT_ID = IC.OBJECT_ID AND C.COLUMN_ID = IC.COLUMN_ID JOIN SYS.INDEXES I ON IC.OBJECT_ID = I.OBJECT_ID AND IC.INDEX_ID = I.INDEX_ID WHERE I.IS_PRIMARY_KEY = 1 AND I.OBJECT_ID = OBJECT_ID(@{nameof(tableName)})\n";
    }
}


