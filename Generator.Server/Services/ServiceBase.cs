using System.Data;
using System.Data.Common;
using Generator.Server.Helpers;
using Generator.Server.Models.Shema;
using Generator.Shared.Models;
using Generator.Shared.Services;
using Generator.Shared.TEST_WILL_DELETE_LATER;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc;
using QueryMaker;
using QueryMaker.MSSql;

namespace Generator.Server.Services;


public abstract class ServiceBase<TContext> : IServiceBase where TContext : DbContext
{
    protected readonly IDictionary<string, Func<SqlQueryFactory>> ConnectionFactory;


    protected TContext Db { get; set; }

    protected DbConnection GeneratorConnection => Db?.Database.GetDbConnection();

    public IServiceProvider Provider { get; set; }

    public SqlQueryFactory SqlQueryFactory(string connectionName) => ConnectionFactory[connectionName]?.Invoke();

    public IConfigurationBuilder ConfigurationBuilder { get; set; }

    public ServiceBase(IServiceProvider provider)
    {
        Provider = provider;
        Db = provider.GetService<TContext>();
        ConnectionFactory = provider.GetService<IDictionary<string, Func<SqlQueryFactory>>>();
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


        var fakeList = GenFu.GenFu.ListOf(fakeDataType.GetType(), number);

        // var savePath = fakeList.CreateCsv();

        //var Q = $@"BULK INSERT {tableName}
        //           FROM '{savePath}'
        //           WITH ( FORMAT = 'CSV');
        //         ";

        //await Connection.ExecuteNonQuery(Q);

        return fakeList;
    }


    public Task<RESPONSE_RESULT> QueryAsync(RESPONSE_REQUEST request, CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var result = new SqlServerManager(GeneratorConnection).QueryAsync($"SELECT * FROM {nameof(TEST_TABLE)}");

            return new GenObject();
            //return result;
        });
    }

    public Task<RESPONSE_RESULT> QueryRelationalAsync(RESPONSE_REQUEST request, CallContext context = default)
    {
        throw new NotImplementedException();
    }

    public Task<RESPONSE_RESULT> ExecuteNonQuery(CallContext context = default)
    {
        throw new NotImplementedException();
    }

    public Task<RESPONSE_RESULT> QueryScalar(CallContext context = default)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<RESPONSE_RESULT> QueryStream(CallContext context = default)
    {
        throw new NotImplementedException();
    }

    public Task<RESPONSE_RESULT> AddAsync(RESPONSE_REQUEST request, CallContext context = default)
    {
        throw new NotImplementedException();
    }

    public Task<RESPONSE_RESULT> Updatesync(RESPONSE_REQUEST request, CallContext context = default)
    {
        throw new NotImplementedException();
    }

    public Task<RESPONSE_RESULT> DeleteAsync(RESPONSE_REQUEST request, CallContext context = default)
    {
        throw new NotImplementedException();
    }


}
