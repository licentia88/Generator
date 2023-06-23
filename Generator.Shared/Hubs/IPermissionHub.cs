using System;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MagicOnion;

namespace Generator.Shared.Hubs;

public interface IPermissionsHub : IHubBase<IPermissionsHub,IPermissionReceiver,PERMISSIONS>
{
    
}

public interface IPermissionReceiver:IHubReceiverBase<PERMISSIONS>  
{
   
}

