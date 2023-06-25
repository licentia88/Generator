using Generator.Shared.Hubs.Base;
using Generator.Shared.Models.ComponentModels;

namespace Generator.Shared.Hubs;

public interface IUsersHub : IHubBase<IUsersHub, IUsersReceiver, USERS>
{
}

public interface IUsersReceiver : IHubReceiverBase<USERS>
{
}
