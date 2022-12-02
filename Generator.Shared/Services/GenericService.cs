using System;
using Generator.Shared.Models;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Generator.Shared.Services
{
	[Service]
	public interface IGenericService 
	{
 
		public ValueTask<RESPONSE_RESULT> QueryAsync(CallContext context = default);

		public ValueTask<RESPONSE_RESULT> AddAsync(RESPONSE_REQUEST request, CallContext context = default);


    }

    [Service]
    public interface ITestService
    {
        public IAsyncEnumerable<RESPONSE_RESULT> QueryStream(CallContext context = default);

        public ValueTask<RESPONSE_RESULT> QueryAsync(CallContext context = default);

        public ValueTask<RESPONSE_RESULT> QueryScalarTest(CallContext context = default);

        public ValueTask<RESPONSE_RESULT> InsertWithIdentityTest(CallContext context = default);

        public ValueTask<RESPONSE_RESULT> InsertWithCodeTableTest(CallContext context = default);

        public ValueTask<RESPONSE_RESULT> InsertWithoutIdentityTest(CallContext context = default);

        public IAsyncEnumerable<RESPONSE_RESULT> QueryStreamObject(CallContext context = default);

        public ValueTask<RESPONSE_RESULT> QueryAsyncObject(CallContext context = default);


        public ValueTask<RESPONSE_RESULT> InsertWithIdentityTestObject(CallContext context = default);

        public ValueTask<RESPONSE_RESULT> InsertWithCodeTableTestObject(CallContext context = default);

        public ValueTask<RESPONSE_RESULT> InsertWithoutIdentityTestObject(CallContext context = default);
    }

}

