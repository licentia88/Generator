using System;
using Generator.Client;
using Generator.Components.Args;
using Generator.Components.Components;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Generator.Shared.Services.Base;
using Generator.UI.Extensions;
using Generator.UI.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.UI.Pages.Base;

public abstract class MyBaseClass : ComponentBase
{
    [Inject]
    public IDialogService DialogService { get; set; }

    [Inject]
    ISnackbar Snackbar { get; set; }

    [Inject]
    public NotificationsView NotificationsView { get; set; }


    protected override Task OnInitializedAsync()
    {
        NotificationsView.Snackbar = Snackbar;
        NotificationsView.Notifications = new();
        return base.OnInitializedAsync();
    }

    public async Task ExecuteAsync<T>(Func<Task<T>> task)
    {
        try
        {
            await task().ConfigureAwait(false);
        }
        catch (RpcException ex)
        {
            NotificationsView.Notifications.Add(new(ex.Status.Detail, Severity.Error));

            NotificationsView.Fire();
        }
    }

    public void Execute<T>(Func<T> task)
    {
        try
        {
            task();
        }
        catch (RpcException ex)
        {
            NotificationsView.Notifications.Add(new(ex.Status.Detail, Severity.Error));

            NotificationsView.Fire();
        }
    }

}
public abstract class PagesBase<TModel, TService, TServiceInterface> : MyBaseClass where TModel : new()
where TService : IClientService
where TServiceInterface : IGenericService<TServiceInterface, TModel>
{
    [Inject]
    public TService Service { get; set; }

    [Inject]
    public DatabaseService DatabaseService { get; set; }

    [Inject]
    public AuthService AuthService { get; set; }

    public List<TModel> DataSource { get; set; } = new List<TModel>();


    public virtual async Task Create(GenArgs<TModel> args)
    {
        await ExecuteAsync(async () =>
        {
            var service = ((IGenericService<TServiceInterface, TModel>)Service);

            var result = await service.Create(args.Model);

            var primaryKey = args.Model.GetPrimaryKey();

            args.Model.SetPropertyValue(primaryKey, result.GetPropertyValue(primaryKey));

            args.Model = result;

            DataSource.Add(result);

            return result;
        });
    }

    public virtual async Task Read(SearchArgs args)
    {
        await ExecuteAsync(async () =>
        {
            var service = ((IGenericService<TServiceInterface, TModel>)Service);

            var result = await service.ReadAll();

            DataSource.AddRange(result);

            return result;
        });
    }

    public virtual async Task Update(GenArgs<TModel> args)
    {
        await ExecuteAsync(async () =>
        {
            var service = ((IGenericService<TServiceInterface, TModel>)Service);

            var result = await service.Update(args.Model);

            return result;
        });

        //Datasource da mevcut Datayi replace yap
    }

    public virtual async Task Delete(GenArgs<TModel> args)
    {
        var Dialog = await DialogService.ShowAsync<ConfirmDelete>("Confirm Delete");

        var dialogResult = await Dialog.Result;

        if (dialogResult.Cancelled)
            NotificationsView.Notifications.Add(new NotificationVM("Cancelled", Severity.Info));

        await ExecuteAsync(async () =>
        {
            var service = ((IGenericService<TServiceInterface, TModel>)Service);

            var result = await service.Delete(args.Model);

            DataSource.Remove(args.Model);

            return result;
        });

    }

    public virtual void Cancel(GenArgs<TModel> args)
    {
        DataSource[args.Index] = args.OldModel;
    }

    public virtual Task Load(IGenView<TModel> View)
    {
        return Task.CompletedTask;
    }


}

public abstract class PagesBase<TModel, TChild, TService, TServiceInterface> : PagesBase<TChild, TService, TServiceInterface>
    where TService : IClientService
    where TModel : new() where TChild : new()
    where TServiceInterface : IGenericService<TServiceInterface, TChild>
{
    [Parameter]
    public TModel ParentModel { get; set; }


    public virtual async Task ReadByParent(SearchArgs args)
    {
        await ExecuteAsync(async () =>
        {
            var pk = ParentModel.GetPrimaryKey();

            var fk = ModelExtensions.GetForeignKey<TModel, TChild>();

            var service = ((IGenericService<TService, TModel>)Service);

            var result = await service.FindByParent(ParentModel.GetPropertyValue(pk).CastTo<string>(), fk);

            return result;
        });
      

        //DataSource.AddRange(result);
    }

}