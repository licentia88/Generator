using System.Data;
using System.Data.Common;
using Generator.Server.Helpers;

namespace Generator.Server.Models.Shema;

public class ShemaGenerator
{
    internal ICollection<TableSchema> ShemaList { get; set; } = new HashSet<TableSchema>();

    public ShemaGenerator(DbConnection Connection, params string[] tableNames)
    {

        ShemaList = tableNames.Select(tableName => new TableSchema(Connection, tableName)).ToList();
    }

    public void FindRelation()
    {

    }
}
internal class TableSchema 
{
    DbConnection connection;

    public string TableName { get; set; }
    public List<Columns> ColumnList { get; set; } = new List<Columns>();
    public PrimaryKey PrimaryKey { get; set; } = new PrimaryKey();
    public List<UniqueKey> UniqueKeyList { get; set; } = new List<UniqueKey>();
    public List<ForeignKey> ForeignKeyList { get; set; } = new List<ForeignKey>();

    public TableSchema(DbConnection Connection, string tableName)
    {
        connection = Connection;
        TableName = tableName;

        GetDataBaseSchema();
    }
 
     
 
    protected void GetDataBaseSchema()
    {
        string[] restrictions = new string[4];
        restrictions[2] = TableName;

        DataTable schemaColumns = connection.GetSchema("Columns", restrictions);

        foreach (DataRow rowColumn in schemaColumns.Rows)
        {
            string ColumnName = rowColumn[3].ToString();
            var dataType = rowColumn[7].ToString().SqlToType();
            this.ColumnList.Add(new Columns() { TableName = TableName, FieldName = ColumnName, DataType = dataType });
        }

        DataTable schemaPrimaryKey = connection.GetSchema("IndexColumns", restrictions);

        foreach (System.Data.DataRow rowPrimaryKey in schemaPrimaryKey.Rows)
        {
            string indexName = rowPrimaryKey[2].ToString();

            if (indexName.IndexOf("PK_") != -1)
            {
                PrimaryKey = new PrimaryKey
                {
                    TableName = TableName,
                    FieldName = rowPrimaryKey[6].ToString(),
                    PrimaryKeyName = indexName
                };

            }

            if (indexName.IndexOf("UQ_") != -1)
            {
                this.UniqueKeyList.Add(new UniqueKey()
                {
                    TableName = TableName,
                    FieldName = rowPrimaryKey[6].ToString(),
                    UniqueKeyName = indexName
                });
            }

        }



        DataTable schemaForeignKeys = connection.GetSchema("ForeignKeys", restrictions);


        foreach (System.Data.DataRow rowFK in schemaForeignKeys.Rows)
        {

            this.ForeignKeyList.Add(new ForeignKey()
            {
                ForeignName = rowFK[2].ToString(),
                TableName = TableName,
                // FieldName = rowFK[6].ToString() //There is no information
            });
        }
       

    }

}

