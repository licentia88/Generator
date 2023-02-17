using System.Data;
using System.Text;
using Cysharp.Text;

namespace Generator.Server.DatabaseResolvers
{
    public class OracleManager : ServerManagerBase, IDatabaseManager
    {
        private readonly IDbConnection _connection;

        public OracleManager(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task ExecuteQueryAsync(string query)
        {
            // Use the OracleConnection instance to execute the query
            await Task.Delay(0);
        }

        internal override string CreateInsertStatement(Dictionary<string, object> model, string tableName, string primaryKey, bool isAutoIncrement)
        {

            using (var sbFields = ZString.CreateStringBuilder())
            using (var sbValues = ZString.CreateStringBuilder())
            {
                foreach (var item in model)
                {
                    sbFields.Append(item.Key + ", ");
                    sbValues.Append("'" + item.Value + "', ");
                }

                var fieldsString = sbFields.ToString().TrimEnd(',', ' ');
                var valueString = sbValues.ToString().TrimEnd(',', ' ');

                var query = $" INSERT INTO {tableName} ({fieldsString}) ";

                if (isAutoIncrement)
                {
                    query += $"VALUES ({primaryKey}.NEXTVAL, {valueString})";
                }
                else
                {
                    query += $"VALUES ({valueString})";
                }

                var selectQuery = $" SELECT {primaryKey}, {fieldsString} FROM {tableName} WHERE {primaryKey} = (SELECT MAX({primaryKey}) FROM {tableName}) ";

                return query + selectQuery;
            }
        }
    }

    
}
