using System;
using MagicOnion.Client;
using System.Diagnostics;
using System.Numerics;
using Generator.Shared.Hubs;
using Generator.Shared.Models.ComponentModels;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using MagicOnion.Serialization.MemoryPack;
using Generator.Client.Hubs.Base;

namespace Generator.Client;

public class PermissionHub : MagicHubBase<IPermissionsHub, IPermissionReceiver, PERMISSIONS>, IPermissionReceiver
{
    public PermissionHub(IServiceProvider provider) : base(provider)
    {
    }
}

