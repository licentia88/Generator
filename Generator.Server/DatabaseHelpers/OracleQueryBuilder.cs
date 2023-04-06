namespace Generator.Server.DatabaseResolvers
{
    public class OracleQueryBuilder : DatabaseManager, IQueryStringBuilder
    {
        public string CreateDeleteStatement(string TableName, string PrimaryKey)
        {
            throw new NotImplementedException();
        }

        public string CreateInsertStatement(string TableName, IDictionary<string, object> Model, string PrimaryKey, bool IsAutoIncrement)
        {
            throw new NotImplementedException();
        }

        public string CreateMethodPropertyMetaDataStatement(string MethodName)
        {
            throw new NotImplementedException();
        }

        public string CreateStoredProcedureFieldMetaDataStatement(string StoredProcedure)
        {
            throw new NotImplementedException();
        }

        public string CreateUpdateStatement(string TableName, IDictionary<string, object> Model, string PrimaryKey)
        {
            throw new NotImplementedException();
        }

        public string IsAutoIncrementStatement(string TableName)
        {
            throw new NotImplementedException();
        }
    }


}
