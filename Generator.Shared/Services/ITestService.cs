using Generator.Shared.Models;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Generator.Shared.Services;

[Service]
public interface ITestService
{
    public IAsyncEnumerable<RESPONSE_RESULT> QueryStream(CallContext context = default);

    public Task<RESPONSE_RESULT> QueryAsync(CallContext context = default);

    public Task<RESPONSE_RESULT> ParametricQuery(CallContext context = default);

    public Task<RESPONSE_RESULT> ExecuteSp(CallContext context = default);

    public Task<RESPONSE_RESULT> ExecuteFunction(CallContext context = default);


    public Task<RESPONSE_RESULT> QueryTestStringDataAsync(CallContext context = default);


    public Task<RESPONSE_RESULT> QueryScalarTest(CallContext context = default);

    public Task<RESPONSE_RESULT> UpdateCodeTest(CallContext context = default);

    public Task<RESPONSE_RESULT> UpdateCodeModelTest(CallContext context = default);

    public Task<RESPONSE_RESULT> UpdateIdentityTest(CallContext context = default);

    public Task<RESPONSE_RESULT> UpdateIdentityModelTest(CallContext context = default);

    public Task<RESPONSE_RESULT> UpdateComputedTest(CallContext context = default);

    public Task<RESPONSE_RESULT> UpdateComputedModelTest(CallContext context = default);

    public Task<RESPONSE_RESULT> InsertWithIdentityTest(CallContext context = default);

    public Task<RESPONSE_RESULT> InsertWithCodeTableTest(CallContext context = default);

    public Task<RESPONSE_RESULT> InsertWithoutIdentityTest(CallContext context = default);

    public Task<RESPONSE_RESULT> DeleteWithIdentityTest(CallContext context = default);

    public Task<RESPONSE_RESULT> DeleteWithCodeTableTest(CallContext context = default);

    public Task<RESPONSE_RESULT> DeleteWithoutIdentityTest(CallContext context = default);

    public IAsyncEnumerable<RESPONSE_RESULT> QueryStreamObject(CallContext context = default);

    public Task<RESPONSE_RESULT> QueryAsyncObject(CallContext context = default);

    public Task<RESPONSE_RESULT> InsertWithIdentityTestObject(CallContext context = default);

    public Task<RESPONSE_RESULT> InsertWithCodeTableTestObject(CallContext context = default);

    public Task<RESPONSE_RESULT> InsertWithoutIdentityTestObject(CallContext context = default);

    public Task<RESPONSE_RESULT> DeleteWithIdentityTestObject(CallContext context = default);

    public Task<RESPONSE_RESULT> DeleteWithCodeTableTestObject(CallContext context = default);

    public Task<RESPONSE_RESULT> DeleteWithoutIdentityTestObject(CallContext context = default);
}

