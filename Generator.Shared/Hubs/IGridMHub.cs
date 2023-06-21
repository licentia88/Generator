using Generator.Shared.Models.ComponentModels;

namespace Generator.Shared.Hubs;

public interface IGridMHub:IHubBase<IGridMHub, IGridMReceiver,GRID_M>
{

}

public interface IGridMReceiver : IHubReceiverBase<GRID_M>
{

}