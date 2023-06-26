﻿using Generator.Client.Hubs.Base;
using Generator.Client.Services;
using Generator.Components.Args;
using Generator.Components.Interfaces;
using Generator.Shared.Enums;
using Generator.Shared.Extensions;
using Generator.Shared.Hubs.Base;
using Generator.UI.Extensions;
using Generator.UI.Models;
using MessagePipe;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.UI.Pages.Base;

public abstract class HubPageBase<THub, THubReceiver,TModel> : PageBaseClass
where TModel:new()
where THub : IMagicHub<THub, THubReceiver,TModel>
where THubReceiver: class, IMagicReceiver<TModel> 
{
    [Inject]
    protected THub Service { get; set; }

    [Inject]
    public DatabaseService DatabaseService { get; set; }

    [Inject]
    public AuthService AuthService { get; set; }
    
    [Inject]
    protected List<TModel> DataSource { get; set; } = new();

    [Inject]
    public virtual ISubscriber<Operation, TModel> Subscriber { get; set; }

    
    protected override Task OnInitializedAsync()
    {
        Subscriber.Subscribe(Operation.Create, model => InvokeAsync(StateHasChanged) );
        Subscriber.Subscribe(Operation.Read, model => InvokeAsync(StateHasChanged) );
        Subscriber.Subscribe(Operation.Update, model => InvokeAsync(StateHasChanged) );
        Subscriber.Subscribe(Operation.Delete, model => InvokeAsync(StateHasChanged) );
        Subscriber.Subscribe(Operation.Stream, model => InvokeAsync(StateHasChanged) );

        return base.OnInitializedAsync();
    }

    protected virtual async Task Create(GenArgs<TModel> args)
    {
        await ExecuteAsync(async () =>
        {
            var result = await Service.CreateAsync(args.Model);

            var primaryKey = args.Model.GetPrimaryKey();

            args.Model.SetPropertyValue(primaryKey, result.GetPropertyValue(primaryKey));

            args.Model = result.Data;
            
            return result;
        });
    }

    protected virtual async Task Read(SearchArgs args)
    {
        await ExecuteAsync(async () =>
        {
            await Service.ReadAsync();
        });
    }

    protected virtual async Task Update(GenArgs<TModel> args)
    {
        await ExecuteAsync(async () =>
        {
            await Service.UpdateAsync(args.Model);
        });
    }

    protected virtual async Task Delete(GenArgs<TModel> args)
    {
        var Dialog = await DialogService.ShowAsync<ConfirmDelete>("Confirm Delete");

        var dialogResult = await Dialog.Result;

        if (dialogResult.Cancelled)
            NotificationsView.Notifications.Add(new NotificationVM("Cancelled", Severity.Info));

        await ExecuteAsync(async () =>
        {
            await Service.DeleteAsync(args.Model);
        });

    }

    protected virtual void Cancel(GenArgs<TModel> args)
    {
        DataSource[args.Index] = args.OldModel;
    }

    protected virtual Task Load(IGenView<TModel> View)
    {
        return Task.CompletedTask;
    }

}
