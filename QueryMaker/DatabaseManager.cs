using System.Data;
using System.Data.Common;
using System.Text;
using QueryMaker.Extensions;

namespace QueryMaker;

public abstract class DatabaseManager
    {
        public DbConnection Connection { get; set; }

        internal async Task OpenConnectionAsync()
        {
            if (Connection.State != ConnectionState.Open)
                await Connection.OpenAsync();
        }

        internal void OpenConnection()
        {
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
        }

        internal void AddParameters(DbCommand command, IDictionary<string, object> model, params string[] fieldArgs)
        {
            try
            {
                if (model is null) return;

                DbParameter[] parameters = new DbParameter[fieldArgs.Length];

                for (int i = 0; i < fieldArgs?.Length; i++)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = $"@{fieldArgs[i]}";
                    parameter.Value = model[fieldArgs[i]] ?? DBNull.Value;

                    parameters[i] = parameter;

                }

                if (parameters is not null && parameters.Count() > 0)
                    command.Parameters.AddRange(parameters);
            }
            catch (Exception ex)
            {

            }

        }

        internal void AddWhereStatementParameters(DbCommand command, params (string Key, object Value)[] WhereStatementParameters)
        {
            try
            {
                if (WhereStatementParameters is null) return;

                var numberOfPropertyValues = WhereStatementParameters.Count();

                DbParameter[] parameters = new DbParameter[numberOfPropertyValues];

                
                var parameterIndex = 0;

                var groups = WhereStatementParameters.Where(x => x.Key is not null).GroupBy(p => p.Key);


            foreach (var whereStatement in groups)
                {
                    var parameterName = whereStatement.Key;

                   

                    if (whereStatement.Count() == 1)
                    {
                        var parameter = command.CreateParameter();

                        parameter.ParameterName = $"@{parameterName}";

                        parameter.Value = whereStatement.Select(x=> x.Value).FirstOrDefault() ?? DBNull.Value;

                        parameters[parameterIndex] = parameter;

                        parameterIndex++;

                    }

                    if (whereStatement.Count() > 1)
                    {
                        //Birden fazla property olmasi In Veya Not In Clause dur

                        var queryReplacementString = new StringBuilder();
                        var paramArray = whereStatement.Select(x => x).ToArray();

                        for (int j = 0; j < whereStatement.Count(); j++)
                        {
                            var parameter = command.CreateParameter();

                            var paramName = $"@{parameterName}{j}";

                            parameter.ParameterName = paramName;
                            parameter.Value = paramArray[j];
                            queryReplacementString.Append($"{paramName}, ");

                            parameters[parameterIndex] = parameter;

                            parameterIndex++;

                        }
                        queryReplacementString = queryReplacementString.Remove(queryReplacementString.Length - 2, 2);

                        command.CommandText = command.CommandText.Replace($"@{parameterName}", queryReplacementString.ToString());

                    }
                }

                if (parameters is not null && parameters.Count() > 0)
                    command.Parameters.AddRange(parameters);
            }
            catch (Exception ex)
            {

            }

        }


        private async Task<List<IDictionary<string, object>>> GetDataTableFromDataReaderAsync(DbDataReader dataReader)
        {
            var resultSet = new List<IDictionary<string, object>>();

            var columns = dataReader.GetColumnSchema().ToList();

            while (await dataReader.ReadAsync())
            {
                var newObj = new Dictionary<string, object>();// new ExpandoObject() as IDictionary<string, object>;

                foreach (var column in columns)
                    newObj.Add(column.ColumnName, dataReader.GetColumnValue(column.ColumnName));

                resultSet.Add(newObj);
            }

            return resultSet;

        }

        private List<IDictionary<string, object>> GetDataTableFromDataReader(DbDataReader dataReader)
        {
            var resultSet = new List<IDictionary<string, object>>();

            var schemaTable = dataReader.GetSchemaTable();

            while (dataReader.Read())
            {
                //Type.GetType(dataRow["DataType"]
                var newObj = new Dictionary<string, object>();// new ExpandoObject() as IDictionary<string, object>;

                foreach (DataColumn column in schemaTable.Columns)
                    newObj.Add(column.ColumnName, dataReader.GetColumnValue(column.ColumnName));

                resultSet.Add(newObj);
            }

            return resultSet;
        }

        public List<IDictionary<string, object>> ExecuteCommand(DbCommand command, CommandBehavior commandBehavior)
        {
            DbDataReader rdr = command.ExecuteReader();
            return GetDataTableFromDataReader(rdr);
        }

        public async Task<List<IDictionary<string, object>>> ExecuteCommandAsync(DbCommand command, CommandBehavior commandBehavior)
        {
            DbDataReader rdr = await command.ExecuteReaderAsync(commandBehavior);
            return await GetDataTableFromDataReaderAsync(rdr);
        }

    }


