using Generator.Examples.Shared.Models;
using Generator.Examples.Shared.Services;
using Generator.Server.Database;
using Generator.Server.Services.Base;

namespace Generator.Server.Services;

public class OrdersMService : MagicBase<IOrdersMService, ORDERS_M,TestContext>, IOrdersMService
{
    public OrdersMService(IServiceProvider provider) : base(provider)
    {
    }
}