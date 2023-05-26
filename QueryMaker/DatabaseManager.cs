using System.Data;
using System.Data.Common;
using QueryMaker.Extensions;

namespace QueryMaker;

public abstract class DatabaseManager
{
    public DbConnection Connection { get; set; }

    /// <summary>
    /// Asynchronously opens the database connection if it's not already open.
    /// </summary>
    internal async Task OpenConnectionAsync()
    {
        if (Connection.State != ConnectionState.Open)
            await Connection.OpenAsync();
    }

    /// <summary>
    /// Opens the database connection if it's not already open.
    /// </summary>
    internal void OpenConnection()
    {
        if (Connection.State != ConnectionState.Open)
            Connection.Open();
    }

    // Other existing methods...

    /// <summary>
    /// Adds parameters to the database command based on the model's field arguments.
    /// </summary>
    internal void AddParameters(DbCommand command, IDictionary<string, object> model, params string[] fieldArgs)
    {
        try
        {
            if (model is null) return;

            var parameters = new DbParameter[fieldArgs.Length];

            for (var i = 0; i < fieldArgs.Length; i++)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = $"@{fieldArgs[i]}";
                parameter.Value = model[fieldArgs[i]] ?? DBNull.Value;

                parameters[i] = parameter;
            }

            if (parameters.Length > 0)
                command.Parameters.AddRange(parameters);
        }
        catch (Exception ex)
        {
            // Handle exception or log the error
        }
    }

    /// <summary>
    /// Adds parameters to the database command based on the provided where statement parameters.
    /// </summary>
    internal void AddWhereStatementParameters(DbCommand command, params (string Key, object Value)[] whereStatementParameters)
    {
        try
        {
            if (whereStatementParameters is null) return;

            var numberOfPropertyValues = whereStatementParameters.Length;

            var parameters = new DbParameter[numberOfPropertyValues];
            var parameterIndex = 0;

            foreach (var statement in whereStatementParameters)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = $"@{statement.Key}";
                parameter.Value = statement.Value ?? DBNull.Value;

                parameters[parameterIndex] = parameter;

                parameterIndex++;
            }

            if (parameters.Length > 0)
                command.Parameters.AddRange(parameters);
        }
        catch (Exception ex)
        {
            // Handle exception or log the error
        }
    }

    /// <summary>
    /// Adds parameters to the database command based on the provided where statement parameters.
    /// </summary>
    internal void AddWhereStatementParameters(DbCommand command, params KeyValuePair<string, object>[] whereStatementParameters)
    {
        try
        {
            if (whereStatementParameters is null) return;

            var numberOfPropertyValues = whereStatementParameters.Length;

            var parameters = new DbParameter[numberOfPropertyValues];
            var parameterIndex = 0;

            foreach (var statement in whereStatementParameters)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = $"@{statement.Key}";
                parameter.Value = statement.Value ?? DBNull.Value;

                parameters[parameterIndex] = parameter;

                parameterIndex++;
            }

            if (parameters.Length > 0)
                command.Parameters.AddRange(parameters);
        }
        catch (Exception ex)
        {
            // Handle exception or log the error
        }
    }

    /// <summary>
    /// Retrieves the result set from the data reader and returns it as a list of dictionaries.
    /// </summary>
    private async Task<List<IDictionary<string, object>>> GetDataTableFromDataReaderAsync(DbDataReader dataReader)
    {
        var resultSet = new List<IDictionary<string, object>>();
        var columns = dataReader.GetColumnSchema().ToList();

        while (await dataReader.ReadAsync())
        {
            var newObj = new Dictionary<string, object>();

            foreach (var column in columns)
                newObj.Add(column.ColumnName, dataReader.GetColumnValue(column.ColumnName));

            resultSet.Add(newObj);
        }

        return resultSet;
    }

    /// <summary>
    /// Retrieves the result set from the data reader and returns it as a list of dictionaries.
    /// </summary>
    private List<IDictionary<string, object>> GetDataTableFromDataReader(DbDataReader dataReader)
    {
        var resultSet = new List<IDictionary<string, object>>();
        var schemaTable = dataReader.GetSchemaTable();

        while (dataReader.Read())
        {
            var newObj = new Dictionary<string, object>();

            if (schemaTable != null)
                foreach (DataColumn column in schemaTable.Columns)
                    newObj.Add(column.ColumnName, dataReader.GetColumnValue(column.ColumnName));

            resultSet.Add(newObj);
        }

        return resultSet;
    }

    /// <summary>
    /// Executes the database command and returns the result set as a list of dictionaries.
    /// </summary>
    public List<IDictionary<string, object>> ExecuteCommand(DbCommand command, CommandBehavior commandBehavior)
    {
        var rdr = command.ExecuteReader(commandBehavior);
        return GetDataTableFromDataReader(rdr);
    }

    /// <summary>
    /// Executes the database command asynchronously and returns the result set as a list of dictionaries.
    /// </summary>
    public async Task<List<IDictionary<string, object>>> ExecuteCommandAsync(DbCommand command, CommandBehavior commandBehavior)
    {
        var rdr = await command.ExecuteReaderAsync(commandBehavior);
        return await GetDataTableFromDataReaderAsync(rdr);
    }
}


