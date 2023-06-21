using System;
using System.Numerics;
using Generator.Shared.Hubs;

namespace Generator.Shared.Hubs;


public interface IHubReceiverBase<TModel>
{
    // Event handlers for CRUD operations
    void OnCreate(TModel model);
    void OnRead(List<TModel> collection);
    void OnUpdate(TModel model);
    void OnDelete(TModel model);
}


