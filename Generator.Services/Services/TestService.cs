using System.Data;
using Generator.Server.Extensions;
using Generator.Server.Helpers;
using Generator.Server.Services;
using Generator.Shared.Extensions;
using Generator.Shared.Models;
using Generator.Shared.Services;
using Generator.Shared.TEST_WILL_DELETE_LATER;
using GenFu;
using Mapster;
using MBrace.FsPickler;
using Microsoft.AspNetCore.Connections;
//using MBrace.FsPickler.Json;
using ProtoBuf.Grpc;

namespace Generator.Services.Services;

public class TestService : ServiceBase<TestContext>, ITestService, IDisposable 
{
    public TestService(IServiceProvider provider) : base(provider)
    {
    }


    //Part 1
    public Task<RESPONSE_RESULT> InsertWithCodeTableTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {  
            var newData = A.New<STRING_TABLE>();

            var dictType = newData.Adapt<IDictionary<string, object>>();

            var result = await GeneratorConnection.InsertAsync(nameof(STRING_TABLE), dictType);

            return new GenObject(result);
        });
    }

   

    public Task<RESPONSE_RESULT> InsertWithIdentityTest(CallContext context = default)
    {
        return  TaskHandler.ExecuteAsync(async () =>
        {
            var serializer = FsPickler.CreateBinarySerializer();

            var newData = A.New<TEST_TABLE>();

            newData.TT_NULLABLE_DATE = null;
            //newData.TT_DEFAULT_VALUE_STRING = "";

            var dictType = newData.Adapt<IDictionary<string, object>>();

 
           var result = await GeneratorConnection.InsertAsync(nameof(TEST_TABLE), dictType);


           return new GenObject(result);
            //return result;
        });
    }

    public Task<RESPONSE_RESULT> InsertWithoutIdentityTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {

            var newData = A.New<COMPUTED_TABLE>();

            var dictType = newData.Adapt<IDictionary<string, object>>();

            var result = await GeneratorConnection.InsertAsync(nameof(COMPUTED_TABLE), dictType);

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> QueryAsync(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            //var connectionFactory = ConnectionFactory["DefaultConnection"].Invoke();

            var connectionFactory2 = ConnectionFactory["GeneratorConnection"].Invoke();

            await connectionFactory2.ExecuteQueryAsync($"SELECT * FROM {nameof(TEST_TABLE)}");

            var result = await GeneratorConnection.QueryAsync($"SELECT * FROM {nameof(TEST_TABLE)}");

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> QueryScalarTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var result = await GeneratorConnection.QueryScalar<int>($"SELECT 1 FROM {nameof(TEST_TABLE)}");

            var genObj = new GenObject();

            //genObj.Add("", result);
            return genObj;
        });

    }

    public new async IAsyncEnumerable<RESPONSE_RESULT> QueryStream(CallContext context = default)
    {
        await foreach (var data in GeneratorConnection.QueryStreamAsync($"SELECT * FROM {nameof(TEST_TABLE)}"))
        {
            yield return new RESPONSE_RESULT(new GenObject(data));
        }
    }


    ////Part 2

    public async IAsyncEnumerable<RESPONSE_RESULT> QueryStreamObject(CallContext context = default)
    {
        await foreach (var data in GeneratorConnection.QueryStreamAsync<TEST_TABLE>($"SELECT * FROM {nameof(TEST_TABLE)}"))
        {
 
            yield return new RESPONSE_RESULT(new GenObject(data.AdaptToDictionary()));
        }

        //return Delegates.ExecuteStreamAsync(() => GeneratorConnection.QueryStreamAsync<TEST_TABLE>($"SELECT * FROM {nameof(TEST_TABLE)}"));

    }

    public Task<RESPONSE_RESULT> QueryAsyncObject(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var result = await GeneratorConnection.QueryAsync<TEST_TABLE>($"SELECT * FROM {nameof(TEST_TABLE)}");

            return new GenObject(result.AdaptToDictionary());
        });
    }

    public Task<RESPONSE_RESULT> InsertWithIdentityTestObject(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {

            var newData = await CreateFakeDataAsync(nameof(TEST_TABLE), 1);

            var dictType = newData.First().Adapt<IDictionary<string, object>>();

            var result = await GeneratorConnection.InsertAsync<TEST_TABLE>(dictType);

            return new GenObject(result.AdaptToDictionary()); ;
        });
    }

    public Task<RESPONSE_RESULT> InsertWithCodeTableTestObject(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var newData = await CreateFakeDataAsync(nameof(STRING_TABLE), 1);

            var dictType = newData.First().Adapt<IDictionary<string, object>>();

            var result = await GeneratorConnection.InsertAsync<STRING_TABLE>(dictType);

            return new GenObject(result.AdaptToDictionary()); ;
        });
    }

    public Task<RESPONSE_RESULT> InsertWithoutIdentityTestObject(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var newData = await CreateFakeDataAsync(nameof(COMPUTED_TABLE), 1);

            var dictType = newData.First().Adapt<IDictionary<string, object>>();

            var result = await GeneratorConnection.InsertAsync<COMPUTED_TABLE>(dictType);

            return new GenObject(result.AdaptToDictionary()); 
        });
    }

    public void Dispose()
    {
        GeneratorConnection.Dispose();
        Db.Dispose();
        GC.SuppressFinalize(this);
    }

    public Task<RESPONSE_RESULT> UpdateCodeTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var EXISTINGDATA = await GeneratorConnection.QueryAsync("SELECT TOP 1 * FROM STRING_TABLE");
 
            var result = await GeneratorConnection.UpdateAsync(nameof(STRING_TABLE), EXISTINGDATA.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> UpdateCodeModelTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var EXISTINGDATA = await GeneratorConnection.QueryAsync("SELECT TOP 1 * FROM STRING_TABLE");

            var result = await GeneratorConnection.UpdateAsync<STRING_TABLE>(EXISTINGDATA.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> UpdateIdentityTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var EXISTINGDATA = await GeneratorConnection.QueryAsync("SELECT TOP 1 * FROM TEST_TABLE");

            var result = await GeneratorConnection.UpdateAsync(nameof(TEST_TABLE), EXISTINGDATA.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> UpdateIdentityModelTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var EXISTINGDATA = await GeneratorConnection.QueryAsync("SELECT TOP 1 * FROM TEST_TABLE");

            var result = await GeneratorConnection.UpdateAsync<TEST_TABLE>(EXISTINGDATA.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> UpdateComputedTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var EXISTINGDATA = await GeneratorConnection.QueryAsync("SELECT TOP 1 * FROM COMPUTED_TABLE");

            var result = await GeneratorConnection.UpdateAsync(nameof(COMPUTED_TABLE), EXISTINGDATA.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> UpdateComputedModelTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var EXISTINGDATA = await GeneratorConnection.QueryAsync("SELECT TOP 1 * FROM COMPUTED_TABLE");

            var result = await GeneratorConnection.UpdateAsync<COMPUTED_TABLE>(EXISTINGDATA.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> DeleteWithIdentityTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var quey = await GeneratorConnection.QueryAsync($"SELECT * FROM {nameof(TEST_TABLE)}");

            var result = await GeneratorConnection.DeleteAsync(nameof(TEST_TABLE), quey.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> DeleteWithCodeTableTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var quey = await GeneratorConnection.QueryAsync($"SELECT * FROM {nameof(STRING_TABLE)}");

            var result = await GeneratorConnection.DeleteAsync(nameof(STRING_TABLE), quey.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> DeleteWithoutIdentityTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var quey = await GeneratorConnection.QueryAsync($"SELECT * FROM {nameof(COMPUTED_TABLE)}");

            var result = await GeneratorConnection.DeleteAsync(nameof(COMPUTED_TABLE), quey.First());

            return new GenObject(result);
        }); 
    }

    public Task<RESPONSE_RESULT> DeleteWithIdentityTestObject(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var quey = await GeneratorConnection.QueryAsync($"SELECT * FROM {nameof(TEST_TABLE)}");

            var result = await GeneratorConnection.DeleteAsync<TEST_TABLE>(nameof(TEST_TABLE), quey.First());

            return new GenObject(result);
        }); 
    }

    public Task<RESPONSE_RESULT> DeleteWithCodeTableTestObject(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var quey = await GeneratorConnection.QueryAsync($"SELECT * FROM {nameof(STRING_TABLE)}");

            var result = await GeneratorConnection.DeleteAsync<STRING_TABLE>(nameof(STRING_TABLE), quey.First());

            return new GenObject(result);

        });
    }

    public Task<RESPONSE_RESULT> DeleteWithoutIdentityTestObject(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var quey = await GeneratorConnection.QueryAsync($"SELECT * FROM {nameof(COMPUTED_TABLE)}");

            var result = await GeneratorConnection.DeleteAsync<COMPUTED_TABLE>(nameof(COMPUTED_TABLE), quey.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> QueryTestStringDataAsync(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var result = await GeneratorConnection.QueryAsync($"SELECT * FROM {nameof(STRING_TABLE)}");

            return new GenObject(result);
        });
    }
}
