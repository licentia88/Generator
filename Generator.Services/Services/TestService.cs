using System.Data.Common;
using System.Dynamic;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Unicode;
using Generator.Server;
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
//using MBrace.FsPickler.Json;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProtoBuf.Grpc;
using SolTechnology.Avro;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Generator.Services.Services;

public class TestService : ServiceBase<TestContext>, ITestService, IDisposable // GenericServiceBase<TestContext,STRING_TABLE>, ITestService, IDisposable
{
    //public DbConnection Connection { get; set; }

     
    //TODO: OpenIfleri kontrol et
    public TestService(IServiceProvider provider) : base(provider)
    {
        //Connection = new DbConnection();
        //var faker = CreateFakeData<GRIDS_M>(5);

        //Connection.ConnectionString = "Server=Localhost;Database=TestContext;User Id=sa;Password=LucidNala88!;TrustServerCertificate=true"
        //Db.Database.SetConnectionString("Server=Localhost;Database=TestContext;User Id=sa;Password=LucidNala88!;TrustServerCertificate=true");
        //ChangeDb(nameof(TestContext));

        //ChangeDb(nameof(GeneratorContext));
        //Db = provider.GetService<TestContext>();
    }

    //Part 1
    public ValueTask<RESPONSE_RESULT> InsertWithCodeTableTest(CallContext context = default)
    {
        return  Delegates.ExecuteAsync(async () =>
        {
 
            var newData = A.New<STRING_TABLE>();

            var dictType = newData.Adapt<IDictionary<string, object>>();

            var result = await GeneratorConnection.InsertAsync(nameof(STRING_TABLE), dictType);

            return result;
        });
    }

    public ValueTask<RESPONSE_RESULT> InsertWithIdentityTest(CallContext context = default)
    {
        return  Delegates.ExecuteAsync(async () =>
        {
            var serializer = FsPickler.CreateBinarySerializer();

            var newData = A.New<TEST_TABLE>();

            var dictType = newData.Adapt<IDictionary<string, object>>();

            var result = await GeneratorConnection.InsertAsync(nameof(TEST_TABLE), dictType);

            var classGen = new ClassGenerator();
            var newClass= classGen.GenerateClass(result);

            var adapted = result.Adapt(result.GetType(), newClass.GetType());

            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            string res = JsonConvert.SerializeObject(adapted, Formatting.Indented);



            return result;
        });
    }

    public ValueTask<RESPONSE_RESULT> InsertWithoutIdentityTest(CallContext context = default)
    {
        return  Delegates.ExecuteAsync(async () =>
        {

            var newData = A.New<COMPUTED_TABLE>();

            var dictType = newData.Adapt<IDictionary<string, object>>();

            var result = await GeneratorConnection.InsertAsync(nameof(COMPUTED_TABLE), dictType);

            return result;
        });
    }

    public ValueTask<RESPONSE_RESULT> QueryAsync(CallContext context = default)
    {
        return Delegates.ExecuteAsync(async () =>
        {
             
            var result = await GeneratorConnection.QueryAsync($"SELECT * FROM {nameof(TEST_TABLE)}");
  
            return result;
        });
    }

    public ValueTask<RESPONSE_RESULT> QueryScalarTest(CallContext context = default)
    {
        return Delegates.ExecuteAsync(async () =>
        {
            var result = await GeneratorConnection.QueryScalar<int>($"SELECT 1 FROM {nameof(TEST_TABLE)}");

            return result;
        });
        
    }

    public IAsyncEnumerable<RESPONSE_RESULT> QueryStream(CallContext context =default)
    {
        return Delegates.ExecuteStreamAsync(() => GeneratorConnection.QueryStreamAsync($"SELECT * FROM {nameof(TEST_TABLE)}"));
    }


    //Part 2

    public IAsyncEnumerable<RESPONSE_RESULT> QueryStreamObject(CallContext context = default)
    {
        return Delegates.ExecuteStreamAsync(() => GeneratorConnection.QueryStreamAsync<TEST_TABLE>($"SELECT * FROM {nameof(TEST_TABLE)}"));

    }

    public ValueTask<RESPONSE_RESULT> QueryAsyncObject(CallContext context = default)
    {
        return Delegates.ExecuteAsync(async () =>
        {
            var result = await GeneratorConnection.QueryAsync<TEST_TABLE>($"SELECT * FROM {nameof(TEST_TABLE)}");

            return result;
        });
    }

    public ValueTask<RESPONSE_RESULT> InsertWithIdentityTestObject(CallContext context = default)
    {
        return Delegates.ExecuteAsync(async () =>
        {
           
            var newData = await CreateFakeDataAsync(nameof(TEST_TABLE), 1);

            var dictType = newData.First().Adapt<IDictionary<string, object>>();

            var result = await GeneratorConnection.InsertAsync<TEST_TABLE>(dictType);

            return result;
        });
    }

    public ValueTask<RESPONSE_RESULT> InsertWithCodeTableTestObject(CallContext context = default)
    {
        return Delegates.ExecuteAsync(async () =>
        {
            var newData = await CreateFakeDataAsync(nameof(STRING_TABLE), 1);

            var dictType = newData.First().Adapt<IDictionary<string, object>>();

            var result = await GeneratorConnection.InsertAsync<STRING_TABLE>(dictType);

            return result;
        });
    }

    public ValueTask<RESPONSE_RESULT> InsertWithoutIdentityTestObject(CallContext context = default)
    {
        return Delegates.ExecuteAsync(async () =>
        {
            //var newData = A.New<COMPUTED_TABLE>();

            //var dictType = newData.Adapt<IDictionary<string, object>>();

            var newData = await CreateFakeDataAsync(nameof(COMPUTED_TABLE), 1);

            var dictType = newData.First().Adapt<IDictionary<string, object>>();

            var result = await GeneratorConnection.InsertAsync<COMPUTED_TABLE>(dictType);

            return result;
        });
    }

    public void Dispose()
    {
        GeneratorConnection.Dispose();
        Db.Dispose();
        GC.SuppressFinalize(this);
    }
}
