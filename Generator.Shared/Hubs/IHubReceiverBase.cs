﻿namespace Generator.Shared.Hubs;


public interface IHubReceiverBase<TModel>
{
    void OnCreate(TModel model);

    void OnRead(List<TModel> collection);

    void OnStreamRead(List<TModel> collection);

    void OnUpdate(TModel model);

    void OnDelete(TModel model);

    void OnCollectionChanged(List<TModel> collection);
}


