using Generator.Shared.Hubs.Base;
using Generator.Shared.Models.ComponentModels;

namespace Generator.Shared.Hubs;

public interface IGridMHub:IMagicHub<IGridMHub, IGridMReceiver,GRID_M>
{

}

public interface IGridMReceiver : IMagicReceiver<GRID_M>
{
     
}
