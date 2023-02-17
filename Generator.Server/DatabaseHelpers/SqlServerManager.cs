using System.Data;
using System.Text;
using Cysharp.Text;

namespace Generator.Server.DatabaseResolvers
{
    public abstract class ServerManagerBase
    {
        internal virtual string CreateInsertStatement(Dictionary<string, object> model, string tableName, string primaryKey, bool isAutoIncrement)
        {
            throw new NotImplementedException();
        }

       

    }


    public class SqlServerManager : ServerManagerBase, IDatabaseManager
    {
        private readonly IDbConnection _connection;

        public SqlServerManager(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task ExecuteQueryAsync(string query)
        {
            // Use the SqlConnection instance to execute the query
            await Task.Delay(0);
        }

        

        internal override string CreateInsertStatement(Dictionary<string, object> model, string tableName, string primaryKey, bool isAutoIncrement)
        {
            using (var sbFields = ZString.CreateStringBuilder())
            using (var sbValues = ZString.CreateStringBuilder())
            {
                if (isAutoIncrement)
                    model.Remove(primaryKey);

                foreach (var item in model)
                {
                    sbFields.Append(item.Key + ", ");
                    sbValues.Append("@" + item.Key + ", ");
                }
 
                var fieldsString = sbFields.ToString();
                var valueString = sbValues.ToString();
                var query = $" INSERT INTO {tableName} ({fieldsString}) VALUES ({valueString}) ";
                var whereStatement = isAutoIncrement ? $" WHERE {primaryKey} = SCOPE_IDENTITY() " : $" WHERE {primaryKey} = @{primaryKey} ";
                var selectQuery = $" SELECT {primaryKey}, {fieldsString} FROM {tableName} {whereStatement} ";

                return query + selectQuery;
            }
        }
    }

    
}
