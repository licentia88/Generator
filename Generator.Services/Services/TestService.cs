using Generator.Shared.Services;
using Generator.Shared.Models;
using ProtoBuf.Grpc;
using Generator.Service.Helpers;
using Generator.Service.Extensions;
using Mapster;
using Generator.Shared.Extensions;
using Microsoft.Extensions.Options;
using System.Dynamic;
using GenFu;
using Generator.Shared.TEST_WILL_DELETE_LATER;

namespace Generator.Service.Services;


public class TestService : ServiceBase, ITestService, IDisposable
{
    public TestService(IServiceProvider provider) : base(provider)
    {
    }

    //Part 1
    public ValueTask<RESPONSE_RESULT> InsertWithCodeTableTest(CallContext context = default)
    {
       //await  CreateFakeData(nameof(TEST_TABLE),5);

        return  Delegates.ExecuteAsync(async () =>
        {
 
            var newData = A.New<STRING_TABLE>();

            var dictType = newData.Adapt<IDictionary<string, object>>();

            var result = await Connection.InsertAsync(nameof(STRING_TABLE), dictType);

            return result;
        });
    }

    public ValueTask<RESPONSE_RESULT> InsertWithIdentityTest(CallContext context = default)
    {
        return  Delegates.ExecuteAsync(async () =>
        {

            var newData = A.New<TEST_TABLE>();

            var dictType = newData.Adapt<IDictionary<string, object>>();

            var result = await Connection.InsertAsync(nameof(TEST_TABLE), dictType);

            return result;
        });
    }

    public ValueTask<RESPONSE_RESULT> InsertWithoutIdentityTest(CallContext context = default)
    {
        return  Delegates.ExecuteAsync(async () =>
        {

            var newData = A.New<COMPUTED_TABLE>();

            var dictType = newData.Adapt<IDictionary<string, object>>();

            var result = await Connection.InsertAsync(nameof(COMPUTED_TABLE), dictType);

            return result;
        });
    }

    public ValueTask<RESPONSE_RESULT> QueryAsync(CallContext context = default)
    {
        return Delegates.ExecuteAsync(async () =>
        {
            var result = await Connection.QueryAsync($"SELECT * FROM {nameof(TEST_TABLE)}");

            return result;
        });
    }

    public ValueTask<RESPONSE_RESULT> QueryScalarTest(CallContext context = default)
    {
        return Delegates.ExecuteAsync(async () =>
        {
            var result = await Connection.QueryScalar<int>($"SELECT 1 FROM {nameof(TEST_TABLE)}");

            return result;
        });
        
    }

    public IAsyncEnumerable<RESPONSE_RESULT> QueryStream(CallContext context =default)
    {
        return Delegates.ExecuteStreamAsync(() => Connection.QueryStreamAsync($"SELECT * FROM {nameof(TEST_TABLE)}"));
    }


    //Part 2

    public IAsyncEnumerable<RESPONSE_RESULT> QueryStreamObject(CallContext context = default)
    {
        return Delegates.ExecuteStreamAsync(() => Connection.QueryStreamAsync<TEST_TABLE>($"SELECT * FROM {nameof(TEST_TABLE)}"));

    }

    public ValueTask<RESPONSE_RESULT> QueryAsyncObject(CallContext context = default)
    {
        return Delegates.ExecuteAsync(async () =>
        {
            var result = await Connection.QueryAsync<TEST_TABLE>($"SELECT * FROM {nameof(TEST_TABLE)}");

            return result;
        });
    }

    public ValueTask<RESPONSE_RESULT> InsertWithIdentityTestObject(CallContext context = default)
    {
        return Delegates.ExecuteAsync(async () =>
        {
            var newData = A.New<TEST_TABLE>();

            var dictType = newData.Adapt<IDictionary<string, object>>();

            var result = await Connection.InsertAsync<TEST_TABLE>(dictType);

            return result;
        });
    }

    public ValueTask<RESPONSE_RESULT> InsertWithCodeTableTestObject(CallContext context = default)
    {
        return Delegates.ExecuteAsync(async () =>
        {
            var newData = A.New<STRING_TABLE>();

            var dictType = newData.Adapt<IDictionary<string, object>>();

            var result = await Connection.InsertAsync<STRING_TABLE>(dictType);

            return result;
        });
    }

    public ValueTask<RESPONSE_RESULT> InsertWithoutIdentityTestObject(CallContext context = default)
    {
        return Delegates.ExecuteAsync(async () =>
        {
            var newData = A.New<COMPUTED_TABLE>();

            var dictType = newData.Adapt<IDictionary<string, object>>();

            var result = await Connection.InsertAsync<COMPUTED_TABLE>(dictType);

            return result;
        });
    }

    public void Dispose()
    {
        Connection.Dispose();
        Db.Dispose();
        GC.SuppressFinalize(this);
    }
}
