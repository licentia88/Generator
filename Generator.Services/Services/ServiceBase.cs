using Generator.Service.Extensions;
using Generator.Service.Helpers;
using Generator.Service.Models.Shema;
using Generator.Services;
using Generator.Shared.Models;
using GenFu;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Generator.Service.Services;

public class ServiceBase
{
    protected TestContext Db { get; set; }

    public DbConnection Connection => Db.Database.GetDbConnection();

 

    public ServiceBase(IServiceProvider provider)
    {
        Db = provider.GetService<TestContext>();
    }



    internal async ValueTask<List<object>> CreateFakeData(string tableName,int number)
    {
        using var command = Connection.CreateCommand();

        if (command.Connection.State != ConnectionState.Open)
            await command.Connection.OpenAsync();

        var shema = new TableSchema(Connection, tableName);

        var dataModel = new Dictionary<string, Type>();

        shema.ColumnList.ForEach(x=> dataModel.Add(x.FieldName, x.DataType));

        var classGenerator = new ClassGenerator(tableName);
        var fakeDataType = classGenerator.GenerateClass(dataModel);

        var dataName = fakeDataType.GetType().Name;


        var fakeList = A.ListOf(fakeDataType.GetType(),number);

        // var savePath = fakeList.CreateCsv();

        //var Q = $@"BULK INSERT {tableName}
        //           FROM '{savePath}'
        //           WITH ( FORMAT = 'CSV');
        //         ";

         //await Connection.ExecuteNonQuery(Q);

        return fakeList;
    }


    
}
