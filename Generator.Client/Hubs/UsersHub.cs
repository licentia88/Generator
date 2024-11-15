using Generator.Client.Hubs.Base;
using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;

namespace Generator.Client.Hubs;

public class UsersHub : MagicHubClientBase<IUsersHub, IUsersReceiver, USERS>, IUsersReceiver
{
    public UsersHub(IServiceProvider provider) : base(provider)
    {
    }
}