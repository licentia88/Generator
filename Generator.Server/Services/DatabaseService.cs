using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;

namespace Generator.Server.Services;

public class DatabaseService : GenericServiceBase<GeneratorContext, DATABASES>,IDatabaseService
{
    public DatabaseService(IServiceProvider provider) : base(provider)
    {
    }

     
}
