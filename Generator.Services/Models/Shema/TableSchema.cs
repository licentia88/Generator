using System;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Generator.Shared.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Generator.Service.Helpers;

namespace Generator.Service.Models.Shema;

public class TableSchema
{
    DbConnection connection;

    public TableSchema(DbConnection Connection, string tableName)
    {
        connection = Connection;
        TableName = tableName;
        ColumnList = new List<Columns>();
        ForeignKeyList = new List<ForeignKey>();
        UniqueKeyList = new List<UniqueKey>();

        GetDataBaseSchema();

    }

    public string TableName { get; set; }
    public List<Columns> ColumnList { get; set; }
    public PrimaryKey PrimaryKey { get; set; }
    public List<UniqueKey> UniqueKeyList { get; set; }
    public List<ForeignKey> ForeignKeyList { get; set; }


    protected void GetDataBaseSchema()
    {

        
        string[] restrictions = new string[4];
        restrictions[2] = TableName;

        DataTable schemaColumns = connection.GetSchema("Columns", restrictions);

        foreach (DataRow rowColumn in schemaColumns.Rows)
        {
            string ColumnName = rowColumn[3].ToString();
            var dataType = rowColumn[7].ToString().SqlToType();
            this.ColumnList.Add(new Columns() { TableName = TableName, FieldName = ColumnName , DataType = dataType});
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

