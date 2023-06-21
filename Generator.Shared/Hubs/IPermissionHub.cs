using System;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MagicOnion;

namespace Generator.Shared.Hubs;

public interface IPermissionsHub : IStreamingHub<IPermissionsHub, IPermissionReceiver>
{
    Task Subscribe();

    Task OnCreate(PERMISSIONS model);

    Task OnRemove(PERMISSIONS model);

    Task OnUpdate(PERMISSIONS model);

    Task CollectionChanged();

}

public interface IPermissionReceiver  
{
    void OnSubscribe(List<PERMISSIONS> collection);

    void OnCreate(PERMISSIONS model);

    void OnRemove(PERMISSIONS model);

    void OnUpdate(PERMISSIONS model);

    void OnCollectionChanged(List<PERMISSIONS> collection);
}

