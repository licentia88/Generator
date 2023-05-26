using Generator.Examples.Shared.Models;
using Generator.Examples.Shared.Services;
using Generator.Server.Services.Base;

namespace Generator.Server.Services;

public class OrdersDService : MagicBase<IOrdersDService, ORDERS_D>, IOrdersDService
{
    public OrdersDService(IServiceProvider provider) : base(provider)
    {
    }
}