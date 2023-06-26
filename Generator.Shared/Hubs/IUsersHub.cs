using Generator.Shared.Hubs.Base;
using Generator.Shared.Models.ComponentModels;

namespace Generator.Shared.Hubs;

public interface IUsersHub : IMagicHub<IUsersHub, IUsersReceiver, USERS>
{
}

public interface IUsersReceiver : IMagicReceiver<USERS>
{
}
