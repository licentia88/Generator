using System.Data.Common;

namespace Generator.Server.DatabaseResolvers
{
    public interface IQueryStringBuilder
    {
        public string CreateInsertStatement(string TableName, IDictionary<string, object> Model, string PrimaryKey, bool IsAutoIncrement);

        public string CreateUpdateStatement(string TableName, IDictionary<string, object> Model, string PrimaryKey);

        public string CreateDeleteStatement(string TableName, string PrimaryKey);

        public string IsAutoIncrementStatement(string TableName);

        public string CreateStoredProcedureFieldMetaDataStatement(string StoredProcedure);

        public string CreateMethodPropertyMetaDataStatement(string MethodName);
    }

    
}
