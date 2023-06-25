﻿using Generator.Client.Services;
using Generator.Components.Args;
using Generator.Components.Interfaces;
using Generator.Shared.Enums;
using Generator.Shared.Extensions;
using Generator.Shared.Services;
using Generator.Shared.Services.Base;
using Generator.UI.Extensions;
using Generator.UI.Models;
using MessagePipe;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.UI.Pages.Base;

public abstract class ServicePageBase<TModel, TService> : PageBaseClass where TModel : new()
where TService : IGenericService<TService, TModel> 
{
    [Inject]
    protected TService Service { get; set; }

    [Inject]
    public IDatabaseService DatabaseService { get; set; }

    [Inject]
    public IAuthService AuthService { get; set; }

    [Inject]
    protected List<TModel> DataSource { get; set; } = new();


    [Inject]
    public ISubscriber<Operation, TModel> Subscriber { get; set; }


    protected override Task OnInitializedAsync()
    {

        Subscriber.Subscribe(Operation.Create, model => InvokeAsync(StateHasChanged));
        Subscriber.Subscribe(Operation.Read, model => InvokeAsync(StateHasChanged));
        Subscriber.Subscribe(Operation.Update, model => InvokeAsync(StateHasChanged));
        Subscriber.Subscribe(Operation.Delete, model => InvokeAsync(StateHasChanged));
        Subscriber.Subscribe(Operation.Stream, model => InvokeAsync(StateHasChanged));

        return base.OnInitializedAsync();
    }
    protected virtual async Task Create(GenArgs<TModel> args)
    {
        await ExecuteAsync(async () =>
        {
            var result = await Service.Create(args.Model);

            var primaryKey = args.Model.GetPrimaryKey();

            args.Model.SetPropertyValue(primaryKey, result.GetPropertyValue(primaryKey));

            args.Model = result;

            DataSource.Add(result);

            return result;
        });
    }

    protected virtual async Task Read(SearchArgs args)
    {
        await ExecuteAsync(async () =>
        {
            var result = await Service.ReadAll();

            DataSource.AddRange(result);

            return result;
        });
    }

    protected virtual async Task Update(GenArgs<TModel> args)
    {
        await ExecuteAsync(async () =>
        {
            var result = await Service.Update(args.Model);

            return result;
        });

        //Datasource da mevcut Datayi replace yap
    }

    protected virtual async Task Delete(GenArgs<TModel> args)
    {
        var Dialog = await DialogService.ShowAsync<ConfirmDelete>("Confirm Delete");

        var dialogResult = await Dialog.Result;

        if (dialogResult.Cancelled)
            NotificationsView.Notifications.Add(new NotificationVM("Cancelled", Severity.Info));

        await ExecuteAsync(async () =>
        {

            var result = await Service.Delete(args.Model);

            DataSource.Remove(args.Model);

            return result;
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

public abstract class ServicePageBase<TModel, TChild, TService> : ServicePageBase<TChild, TService>
    where TService : IGenericService<TService, TChild> 
    where TModel : new() where TChild : new()
{
    [Parameter]
    public TModel ParentModel { get; set; }

  

    protected override Task OnInitializedAsync()
    {
        return base.OnInitializedAsync();
    }

    protected override Task Create(GenArgs<TChild> args)
    {
        var pk = ParentModel.GetPrimaryKey();

        var fk = ModelExtensions.GetForeignKey<TModel, TChild>();

        args.Model.SetPropertyValue(fk, ParentModel.GetPropertyValue(pk));

        return base.Create(args);
    }

    protected virtual async Task ReadByParent()
    {
        await ExecuteAsync(async () =>
        {
            var pk = ParentModel.GetPrimaryKey();

            var fk = ModelExtensions.GetForeignKey<TModel, TChild>();
            var result = await Service.FindByParent(ParentModel.GetPropertyValue(pk).ToString(), fk);

            DataSource = result;
            return result;
        });
    }
}