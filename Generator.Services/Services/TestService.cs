using Generator.Examples.Shared.Models;
using Generator.Services.Helpers;
using Generator.Shared.Extensions;
using Generator.Shared.Models.ServiceModels;
using GenFu;
using Mapster;
using MBrace.FsPickler;
using ProtoBuf.Grpc;

namespace Generator.Services;

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

            var result = await SqlQueryFactory("DefaultConnection").InsertAsync(nameof(STRING_TABLE), dictionaryModel);

            //var result = await SqlQueryFactory("DefaultConnection").InsertAsync(nameof(STRING_TABLE), dictionaryModel);

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

            var result = await SqlQueryFactory("DefaultConnection").InsertAsync(nameof(TEST_TABLE), dictionaryModel);


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

            var result = await SqlQueryFactory("DefaultConnection").InsertAsync(nameof(COMPUTED_TABLE), dictType);

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> QueryAsync(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
           
            var result = await SqlQueryFactory("DefaultConnection").QueryAsync($"SELECT * FROM {nameof(TEST_TABLE)}", new KeyValuePair<string, object>());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> QueryScalarTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var result = await SqlQueryFactory("DefaultConnection").QueryAsync($"SELECT 1 FROM {nameof(TEST_TABLE)}",new KeyValuePair<string, object>());

            var genObj = new GenObject();

            //genObj.Add("", result);
            return genObj;
        });

    }

    public new IAsyncEnumerable<RESPONSE_RESULT> QueryStream(CallContext context = default)
    {
        //await foreach (var data in SqlQueryFactory("DefaultConnection").QueryStreamAsync($"SELECT * FROM {nameof(TEST_TABLE)}"))
        //{
        //    yield return new RESPONSE_RESULT(new GenObject(data));
        //}

        //return default;

        return default;
    }


    ////Part 2

    public new IAsyncEnumerable<RESPONSE_RESULT> QueryStreamObject(CallContext context = default)
    {
        //await foreach (var data in SqlQueryFactory("DefaultConnection").QueryStreamAsync<TEST_TABLE>($"SELECT * FROM {nameof(TEST_TABLE)}"))
        //{

        //    yield return new RESPONSE_RESULT(new GenObject(data.AdaptToDictionary()));
        //}

        //return Delegates.ExecuteStreamAsync(() => SqlQueryFactory("DefaultConnection").QueryStreamAsync<TEST_TABLE>($"SELECT * FROM {nameof(TEST_TABLE)}"));
        return default;
    }

    public Task<RESPONSE_RESULT> QueryAsyncObject(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var result = await SqlQueryFactory("DefaultConnection").QueryAsync($"SELECT * FROM {nameof(TEST_TABLE)}", new KeyValuePair<string, object>());

            return new GenObject(result.AdaptToDictionary());
        });
    }

    public Task<RESPONSE_RESULT> InsertWithIdentityTestObject(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {

            var newData = A.New<TEST_TABLE>();

            var dictType = newData.Adapt<IDictionary<string, object>>();

            var result = await SqlQueryFactory("DefaultConnection").InsertAsync(nameof(TEST_TABLE),dictType);

            return new GenObject(result.AdaptToDictionary()); ;
        });
    }

    public Task<RESPONSE_RESULT> InsertWithCodeTableTestObject(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var newData = A.New<STRING_TABLE>();

            var dictType = newData.Adapt<IDictionary<string, object>>();

            var result = await SqlQueryFactory("DefaultConnection").InsertAsync((nameof(STRING_TABLE)),dictType);

            return new GenObject(result.AdaptToDictionary()); ;
        });
    }

    public Task<RESPONSE_RESULT> InsertWithoutIdentityTestObject(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var newData = A.New<COMPUTED_TABLE>();

            var dictType = newData.Adapt<IDictionary<string, object>>();

            var result = await SqlQueryFactory("DefaultConnection").InsertAsync(nameof(COMPUTED_TABLE),dictType);

            return new GenObject(result.AdaptToDictionary()); 
        });
    }

    public void Dispose()
    {
        //SqlQueryFactory("DefaultConnection").Dispose();
        Db.Dispose();
        GC.SuppressFinalize(this);
    }

    public Task<RESPONSE_RESULT> UpdateCodeTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var EXISTINGDATA = await SqlQueryFactory("DefaultConnection").QueryAsync("SELECT TOP 1 * FROM STRING_TABLE", new KeyValuePair<string, object>());

            var test = await SqlQueryFactory("DefaultConnection").UpdateAsync(nameof(STRING_TABLE), EXISTINGDATA.FirstOrDefault());

            var result = await SqlQueryFactory("DefaultConnection").UpdateAsync(nameof(STRING_TABLE), EXISTINGDATA.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> UpdateCodeModelTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var EXISTINGDATA = await SqlQueryFactory("DefaultConnection").QueryAsync("SELECT TOP 1 * FROM STRING_TABLE", new KeyValuePair<string, object>());

            var result = await SqlQueryFactory("DefaultConnection").UpdateAsync(nameof(STRING_TABLE),EXISTINGDATA.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> UpdateIdentityTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var EXISTINGDATA = await SqlQueryFactory("DefaultConnection").QueryAsync("SELECT TOP 1 * FROM TEST_TABLE", new KeyValuePair<string, object>());

            var test = await SqlQueryFactory("DefaultConnection").UpdateAsync(nameof(TEST_TABLE), EXISTINGDATA.FirstOrDefault());

            var result = await SqlQueryFactory("DefaultConnection").UpdateAsync(nameof(TEST_TABLE), EXISTINGDATA.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> UpdateIdentityModelTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var EXISTINGDATA = await SqlQueryFactory("DefaultConnection").QueryAsync("SELECT TOP 1 * FROM TEST_TABLE", new KeyValuePair<string, object>());

            var result = await SqlQueryFactory("DefaultConnection").UpdateAsync(nameof(TEST_TABLE), EXISTINGDATA.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> UpdateComputedTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var EXISTINGDATA = await SqlQueryFactory("DefaultConnection").QueryAsync("SELECT TOP 1 * FROM COMPUTED_TABLE", new KeyValuePair<string, object>());

            var test = await SqlQueryFactory("DefaultConnection").UpdateAsync(nameof(COMPUTED_TABLE), EXISTINGDATA.FirstOrDefault());

            var result = await SqlQueryFactory("DefaultConnection").UpdateAsync(nameof(COMPUTED_TABLE), EXISTINGDATA.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> UpdateComputedModelTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var EXISTINGDATA = await SqlQueryFactory("DefaultConnection").QueryAsync("SELECT TOP 1 * FROM COMPUTED_TABLE", new KeyValuePair<string, object>());

            var result = await SqlQueryFactory("DefaultConnection").UpdateAsync(nameof(COMPUTED_TABLE), EXISTINGDATA.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> DeleteWithIdentityTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var quey = await SqlQueryFactory("DefaultConnection").QueryAsync($"SELECT * FROM {nameof(TEST_TABLE)}", new KeyValuePair<string, object>());

            var r1= await SqlQueryFactory("DefaultConnection").DeleteAsync(nameof(TEST_TABLE), quey.First());
            var result = await SqlQueryFactory("DefaultConnection").DeleteAsync(nameof(TEST_TABLE), quey.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> DeleteWithCodeTableTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var quey = await SqlQueryFactory("DefaultConnection").QueryAsync($"SELECT * FROM {nameof(STRING_TABLE)}", new KeyValuePair<string, object>());

            var r1 = await SqlQueryFactory("DefaultConnection").DeleteAsync(nameof(STRING_TABLE), quey.First());

            var result = await SqlQueryFactory("DefaultConnection").DeleteAsync(nameof(STRING_TABLE), quey.First());

            return new GenObject(result);
        });
    }

    public Task<RESPONSE_RESULT> DeleteWithoutIdentityTest(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var quey = await SqlQueryFactory("DefaultConnection").QueryAsync($"SELECT * FROM {nameof(COMPUTED_TABLE)}", new KeyValuePair<string, object>());


            var result = await SqlQueryFactory("DefaultConnection").DeleteAsync(nameof(COMPUTED_TABLE), quey.First());

            return new GenObject(result);
        }); 
    }

    public Task<RESPONSE_RESULT> DeleteWithIdentityTestObject(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var quey = await SqlQueryFactory("DefaultConnection").QueryAsync($"SELECT * FROM {nameof(TEST_TABLE)}", new KeyValuePair<string, object>());

            var r1 = await SqlQueryFactory("DefaultConnection").DeleteAsync(nameof(TEST_TABLE), quey.First());

            return new GenObject(r1);
        }); 
    }

    public Task<RESPONSE_RESULT> DeleteWithCodeTableTestObject(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var quey = await SqlQueryFactory("DefaultConnection").QueryAsync($"SELECT * FROM {nameof(STRING_TABLE)}", new KeyValuePair<string, object>());

            var r1 = await SqlQueryFactory("DefaultConnection").DeleteAsync(nameof(STRING_TABLE), quey.First());

            return new GenObject(r1);

        });
    }

    public Task<RESPONSE_RESULT> DeleteWithoutIdentityTestObject(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var quey = await SqlQueryFactory("DefaultConnection").QueryAsync($"SELECT * FROM {nameof(COMPUTED_TABLE)}", new KeyValuePair<string, object>());

            var r1 = await SqlQueryFactory("DefaultConnection").DeleteAsync(nameof(COMPUTED_TABLE), quey.First());

            return new GenObject(r1);
        });
    }

    public Task<RESPONSE_RESULT> QueryTestStringDataAsync(CallContext context = default)
    {
        return TaskHandler.ExecuteAsync(async () =>
        {
            var result = await SqlQueryFactory("DefaultConnection").QueryAsync($"SELECT * FROM {nameof(STRING_TABLE)}", new KeyValuePair<string, object>());

            return new GenObject(result);
        });
    }

    public async Task<RESPONSE_RESULT> ParametricQuery(CallContext context = default)
    {
       

        //whereStatement.AddPropertyValue("apparel");
        //whereStatement.AddPropertyValue("bag");
        //whereStatement.AddPropertyValue("batch");

        var result = await SqlQueryFactory("DefaultConnection").QueryAsync($"SELECT * FROM {nameof(STRING_TABLE)} WHERE CT_ROWID IN (@CT_ROWID)", ("CT_ROWID", "apparel"));


        return new RESPONSE_RESULT(new GenObject(result));
    }

    public async Task<RESPONSE_RESULT> ExecuteSp(CallContext context = default)
    {
        var sqlManager = SqlQueryFactory("DefaultConnection");

        var EXISTINGDATA = await sqlManager.QueryAsync("SELECT TOP 1 * FROM TEST_TABLE", new KeyValuePair<string, object>());

        //var newWhereStatement = new WhereStatement("TT_ROWID", EXISTINGDATA.First()["TT_ROWID"]);

        //ExampleFunction

        var result = await SqlQueryFactory("DefaultConnection").QueryAsync("EXEC ExampleStoredProcedure", ("TT_ROWID", EXISTINGDATA.First()["TT_ROWID"]));

        Console.WriteLine();
        return new RESPONSE_RESULT(new GenObject(result));
    }

    public async Task<RESPONSE_RESULT> ExecuteFunction(CallContext context = default)
    {

        var spMetaData = await SqlQueryFactory("DefaultConnection").GetMethodParameters("ExampleStoredProcedure");
        //var newWhereStatement = new WhereStatement("long", 9);

        var result = await SqlQueryFactory("DefaultConnection").QueryAsync("SELECT dbo.ExampleFunction(@long)",  ("long", 9));

        return new RESPONSE_RESULT(new GenObject(result));

    }


    public async IAsyncEnumerable<string> Subscribe(IAsyncEnumerable<string> requests)
    {
        await foreach (string item in requests)
        {
            await Task.Delay(3000);
            Console.WriteLine($"Processing {item} {DateTime.Now}");
            yield return item;
        }
 
    }
}
