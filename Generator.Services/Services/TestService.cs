using System.Data;
using Generator.Server.Extensions;
using Generator.Server.Helpers;
using Generator.Server.Services;
using Generator.Shared.Extensions;
using Generator.Shared.Models;
using Generator.Shared.Services;
using Generator.Shared.TEST_WILL_DELETE_LATER;
using Mapster;
using MBrace.FsPickler;
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

            var newData = GenFu.GenFu.New<STRING_TABLE>();

            var dictionaryModel = newData.Adapt<IDictionary<string, object>>();

            var test = await SqlQueryFactory("DefaultConnection").InsertAsync(nameof(STRING_TABLE), dictionaryModel);

            var result = await GeneratorConnection.InsertAsync(nameof(STRING_TABLE), dictionaryModel);

            return new GenObject(result);
        });
    }

   

    public Task<RESPONSE_RESULT> InsertWithIdentityTest(CallContext context = default)
    {
        return  TaskHandler.ExecuteAsync(async () =>
        {
            var serializer = FsPickler.CreateBinarySerializer();

            var newData = GenFu.GenFu.New<TEST_TABLE>();

            newData.TT_NULLABLE_DATE = null;
            //newData.TT_DEFAULT_VALUE_STRING = "";

            var dictionaryModel = newData.Adapt<IDictionary<string, object>>();

            var test = await SqlQueryFactory("DefaultConnection").InsertAsync(nameof(TEST_TABLE), dictionaryModel);

            var result = await GeneratorConnection.InsertAsync(nameof(TEST_TABLE), dictionaryModel);


           return new GenObject(result);
            //return result;
        });
    }

    public Task<RESPONSE_RESULT> InsertWithoutIdentityTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {

            var newData = GenFu.GenFu.New<COMPUTED_TABLE>();

            var dictType = newData.Adapt<IDictionary<string, object>>();

            var result = await GeneratorConnection.InsertAsync(nameof(COMPUTED_TABLE), dictType);

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> QueryAsync(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
           
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

            var test = await SqlQueryFactory("DefaultConnection").UpdateAsync(nameof(STRING_TABLE), EXISTINGDATA.FirstOrDefault());

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

            var test = await SqlQueryFactory("DefaultConnection").UpdateAsync(nameof(TEST_TABLE), EXISTINGDATA.FirstOrDefault());

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

            var test = await SqlQueryFactory("DefaultConnection").UpdateAsync(nameof(COMPUTED_TABLE), EXISTINGDATA.FirstOrDefault());

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

            var r1= await SqlQueryFactory("DefaultConnection").DeleteAsync(nameof(TEST_TABLE), quey.First());
            var result = await GeneratorConnection.DeleteAsync(nameof(TEST_TABLE), quey.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> DeleteWithCodeTableTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var quey = await GeneratorConnection.QueryAsync($"SELECT * FROM {nameof(STRING_TABLE)}");

            var r1 = await SqlQueryFactory("DefaultConnection").DeleteAsync(nameof(STRING_TABLE), quey.First());

            var result = await GeneratorConnection.DeleteAsync(nameof(STRING_TABLE), quey.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> DeleteWithoutIdentityTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var quey = await GeneratorConnection.QueryAsync($"SELECT * FROM {nameof(COMPUTED_TABLE)}");

            var r1 = await SqlQueryFactory("DefaultConnection").DeleteAsync(nameof(STRING_TABLE), quey.First());

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

    public async Task<RESPONSE_RESULT> ParametricQuery(CallContext context = default)
    {
        var whereStatement = new WhereStatement("CT_ROWID", "apparel");
       

        //whereStatement.AddPropertyValue("apparel");
        //whereStatement.AddPropertyValue("bag");
        //whereStatement.AddPropertyValue("batch");

        var result = await SqlQueryFactory("DefaultConnection").QueryAsync($"SELECT * FROM {nameof(STRING_TABLE)} WHERE CT_ROWID IN (@CT_ROWID)", whereStatement);


        return new RESPONSE_RESULT(new GenObject(result));
    }

    public async Task<RESPONSE_RESULT> ExecuteSp(CallContext context = default)
    {
        var sqlManager = SqlQueryFactory("DefaultConnection");

        var EXISTINGDATA = await sqlManager.QueryAsync("SELECT TOP 1 * FROM TEST_TABLE",null);

        var newWhereStatement = new WhereStatement("TT_ROWID", EXISTINGDATA.First()["TT_ROWID"]);

        //ExampleFunction

        var result = await SqlQueryFactory("DefaultConnection").QueryAsync("ExampleStoredProcedure", CommandBehavior.Default,CommandType.StoredProcedure, newWhereStatement);

        Console.WriteLine();
        return new RESPONSE_RESULT(new GenObject(result));
    }

    public async Task<RESPONSE_RESULT> ExecuteFunction(CallContext context = default)
    {

        var spMetaData = await SqlQueryFactory("DefaultConnection").GetMethodParameters("ExampleStoredProcedure");
        var newWhereStatement = new WhereStatement("long", 9);

        var result = await SqlQueryFactory("DefaultConnection").QueryAsync("SELECT dbo.ExampleFunction(@long)", CommandBehavior.Default, CommandType.Text, newWhereStatement);

        return new RESPONSE_RESULT(new GenObject(result));

    }
}
