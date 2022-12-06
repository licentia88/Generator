using Generator.Server;
using Generator.Server.Services;
using Generator.Shared.Models;
using Generator.Shared.Services;
using ProtoBuf.Grpc;

namespace Generator.Server.Services;

public class DatabaseService : GenericServiceBase<GeneratorContext, DATABASES>,IDatabaseService
{
    public DatabaseService(IServiceProvider provider) : base(provider)
    {
    }

     
}
