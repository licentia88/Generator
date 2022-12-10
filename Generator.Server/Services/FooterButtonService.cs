using Generator.Shared.Models;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;

namespace Generator.Server.Services;

public class FooterButtonService : GenericServiceBase<GeneratorContext, FOOTER_BUTTON>, IFooterButtonService
{
    public FooterButtonService(IServiceProvider provider) : base(provider)
    {
    }
}
