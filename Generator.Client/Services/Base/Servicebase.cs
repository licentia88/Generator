﻿using System.Reflection;
using Generator.Shared.Services.Base;
using Grpc.Core;
using Grpc.Net.Client;
using MagicOnion;
using MagicOnion.Client;
using MagicOnion.Serialization.MemoryPack;

namespace Generator.Client.Services.Base;

/// <summary>
/// Abstract base class for a generic service implementation.
/// </summary>
/// <typeparam name="TService">The type of service.</typeparam>
/// <typeparam name="TModel">The type of model.</typeparam>
public abstract class ServiceBase<TService, TModel> : IGenericService<TService, TModel>
    where TService : IGenericService<TService, TModel>, IService<TService>
{
    protected readonly TService Client;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceBase{TService, TModel}"/> class.
    /// </summary>
    /// <param name="client">The client instance for the service.</param>
    protected ServiceBase()
    {   
        var channel = GrpcChannel.ForAddress("http://localhost:5002");
        Client = MagicOnionClient.Create<TService>(channel,MemoryPackMagicOnionSerializerProvider.Instance);

        Client =  Client.WithOptions(SenderOption); 
    }

    /// <summary>
    /// Configures the service instance with a cancellation token.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The configured service instance.</returns>
    public virtual TService WithCancellationToken(CancellationToken cancellationToken)
    {
        return Client.WithCancellationToken(cancellationToken);
    }

    /// <summary>
    /// Configures the service instance with a deadline.
    /// </summary>
    /// <param name="deadline">The deadline.</param>
    /// <returns>The configured service instance.</returns>
    public virtual TService WithDeadline(DateTime deadline)
    {
       return  Client.WithDeadline(deadline);
    }

    /// <summary>
    /// Configures the service instance with custom headers.
    /// </summary>
    /// <param name="headers">The headers.</param>
    /// <returns>The configured service instance.</returns>
    public virtual TService WithHeaders(Metadata headers)
    {
        return Client.WithHeaders(headers);
    }

    /// <summary>
    /// Configures the service instance with a custom host.
    /// </summary>
    /// <param name="host">The host.</param>
    /// <returns>The configured service instance.</returns>
    public virtual TService WithHost(string host)
    {
        return Client.WithHost(host);
    }

    /// <summary>
    /// Configures the service instance with custom call options.
    /// </summary>
    /// <param name="option">The call options.</param>
    /// <returns>The configured service instance.</returns>
    public virtual TService WithOptions(CallOptions option)
    {
        return Client.WithOptions(option);
    }

    /// <summary>
    /// Creates a new instance of the specified model.
    /// </summary>
    /// <param name="model">The model to create.</param>
    /// <returns>A unary result containing the created model.</returns>
    public UnaryResult<TModel> Create(TModel model)
    {
        return Client.Create(model);
    }

   

    public UnaryResult<List<TModel>> FindByParent(string parentId, string ForeignKey)
    {
        return Client.FindByParent(parentId,ForeignKey);
    }

    /// <summary>
    /// Updates the specified model.
    /// </summary>
    /// <param name="model">The model to update.</param>
    /// <returns>A unary result containing the updated model.</returns>
    public UnaryResult<TModel> Update(TModel model)
    {
        return Client.Update(model);
    }

    /// <summary>
    /// Deletes the specified model.
    /// </summary>
    /// <param name="model">The model to delete.</param>
    /// <returns>A unary result containing the deleted model.</returns>
    public UnaryResult<TModel> Delete(TModel model)
    {
        return Client.Delete(model);
    }

    /// <summary>
    /// Retrieves all models.
    /// </summary>
    /// <returns>A unary result containing a list of all models.</returns>
    public UnaryResult<List<TModel>> ReadAll()
    {
        return Client.ReadAll();
    }

    public Task<ServerStreamingResult<List<TModel>>> StreamReadAll(int batchSize)
    {
        return Client.StreamReadAll(batchSize);
    }

    
    public TService SetToken(byte[] token)
    {
        var cop = new CallOptions().WithHeaders(new Metadata
                {
                    { "auth-token-bin", token}
                });

        return Client.WithOptions(cop);
    }

    private CallOptions SenderOption => new CallOptions().WithHeaders(new Metadata
        {
             { "client", Assembly.GetEntryAssembly().GetName().Name}
         });
}

