using System.Data;
using System.Configuration;
using Generator.Server.Models.Shema;
using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Dynamic;
using Generator.Server.Extensions;
using SharedPropertyExt = Generator.Shared.Extensions.PropertyExtensions;
using Generator.Shared.Extensions;
using ObjectExtensions = Generator.Server.Extensions.ObjectExtensions;
using System.Collections.Generic;
using System.Data.Common;

namespace Generator.Server.DatabaseResolvers
{
    //Ortak Methodlar burada olacak
    public abstract class DatabaseManager 
    {
        public DbConnection Connection { get; set; }

        //public ConnectionStringSettings ConnectionSettings { get; set; }

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
        private async Task<List<IDictionary<string, object>>> GetDataTableFromDataReaderAsync(DbDataReader dataReader)
        {
            var resultSet = new List<IDictionary<string, object>>();
 
            var columns = dataReader.GetColumnSchema().ToList();

            while (await dataReader.ReadAsync())
            {

                var newObj = new ExpandoObject() as IDictionary<string, object>;

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
                var newObj = new ExpandoObject() as IDictionary<string, object>;

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


}
