using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;

namespace Generator.Server.Services;

public class DisplayFieldsService : GenericServiceBase<GeneratorContext, DISPLAY_FIELDS>, IDisplayFieldsService
{
    public DisplayFieldsService(IServiceProvider provider) : base(provider)
    {
    }
}
