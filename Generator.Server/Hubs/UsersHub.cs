using Generator.Server.Hubs.Base;
using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;

namespace Generator.Server.Hubs;

public class UsersHub : MagicHubBase<IUsersHub, IUsersReceiver, USERS>, IUsersHub
{
    public UsersHub(IServiceProvider provider) : base(provider)
    {
    }
}