using Generator.Server.FIlters;
using Generator.Server.Helpers;
using Generator.Server.Hubs.Base;
using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ServiceModels;
using Microsoft.EntityFrameworkCore;

namespace Generator.Server.Hubs;

 public class PermissionHub : MagicHubServerBase<IPermissionsHub, IPermissionReceiver, PERMISSIONS>, IPermissionsHub
{
    public PermissionHub(IServiceProvider provider) : base(provider)
    {

         
    }

   
}
