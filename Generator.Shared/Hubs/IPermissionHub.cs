using Generator.Shared.Hubs.Base;
using Generator.Shared.Models.ComponentModels;

namespace Generator.Shared.Hubs;

public interface IPermissionsHub : IMagicHub<IPermissionsHub,IPermissionReceiver,PERMISSIONS>
{
    
}

public interface IPermissionReceiver:IMagicReceiver<PERMISSIONS>  
{
   
}

