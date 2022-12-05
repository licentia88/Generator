using System.Data;
using System.Data.Common;
using Generator.Server.Extensions;
using Generator.Server.Helpers;
using Generator.Server.Models.Shema;
using Generator.Shared.Models;
using Generator.Shared.Services;
using Generator.Shared.TEST_WILL_DELETE_LATER;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc;
using ProtoBuf.Meta;

namespace Generator.Server.Services;

 
public abstract class ServiceBase<TContext> : IServiceBase where TContext : DbContext
{
    public Lazy<List<GRIDS_M>> Components { get; set; }

    protected TContext Db { get; set; }

    protected DbConnection GeneratorConnection => Db?.Database.GetDbConnection();

    public IServiceProvider Provider { get; set; }



    public ServiceBase(IServiceProvider provider)
    {
        Db = provider.GetService<TContext>();

        Provider = provider;
    }

 
    


    protected async ValueTask<List<object>> CreateFakeDataAsync(string tableName, int number)
    {
        await using var command = GeneratorConnection.CreateCommand();
        if (command.Connection!.State != ConnectionState.Open)
            await command.Connection.OpenAsync();

        var shema = new ShemaGenerator(GeneratorConnection, tableName).ShemaList.First();


        var dataModel = new Dictionary<string, Type>();

        shema.ColumnList.ForEach(x => dataModel.Add(x.FieldName, x.DataType));

        var classGenerator = new ClassGenerator(tableName);
        var fakeDataType = classGenerator.GenerateClass(dataModel);

        var dataName = fakeDataType.GetType().Name;


        var fakeList = A.ListOf(fakeDataType.GetType(), number);

        // var savePath = fakeList.CreateCsv();

        //var Q = $@"BULK INSERT {tableName}
        //           FROM '{savePath}'
        //           WITH ( FORMAT = 'CSV');
        //         ";

        //await Connection.ExecuteNonQuery(Q);

        return fakeList;
    }


    public ValueTask<RESPONSE_RESULT> QueryAsync(RESPONSE_REQUEST request, CallContext context = default)
    {
        return Delegates.ExecuteAsync(async () =>
        {
            var result = await GeneratorConnection.QueryAsync($"SELECT * FROM {nameof(TEST_TABLE)}");

            return result;
        });
    }

    public ValueTask<RESPONSE_RESULT> QueryRelationalAsync(RESPONSE_REQUEST request, CallContext context = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<RESPONSE_RESULT> ExecuteNonQuery(CallContext context = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<RESPONSE_RESULT> QueryScalar(CallContext context = default)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<RESPONSE_RESULT> QueryStream(CallContext context = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<RESPONSE_RESULT> AddAsync(RESPONSE_REQUEST request, CallContext context = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<RESPONSE_RESULT> Updatesync(RESPONSE_REQUEST request, CallContext context = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<RESPONSE_RESULT> DeleteAsync(RESPONSE_REQUEST request, CallContext context = default)
    {
        throw new NotImplementedException();
    }


}
