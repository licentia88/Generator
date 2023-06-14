using System;
using DocumentFormat.OpenXml.EMMA;
using Generator.Client;
using Generator.Components.Args;
using Generator.Components.Components;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using Generator.Shared.Services.Base;
using Generator.UI.Extensions;
using Generator.UI.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.UI.Pages.Base;

public abstract class PagesBase<TModel, TService, TServiceInterface> : MyBaseClass where TModel : new()
where TService : ServiceBase<TServiceInterface, TModel>
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
            var result = await Service.Create(args.Model);

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
            var result = await Service.ReadAll();

            DataSource.AddRange(result);

            return result;
        });
    }

    public virtual async Task Update(GenArgs<TModel> args)
    {
        await ExecuteAsync(async () =>
        {
            var result = await Service.Update(args.Model);

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

            var result = await Service.Delete(args.Model);

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
    where TService : ServiceBase<TServiceInterface, TChild>
    where TModel : new() where TChild : new()
    where TServiceInterface : IGenericService<TServiceInterface, TChild>
{
    [Parameter]
    public TModel ParentModel { get; set; }



    public override Task Create(GenArgs<TChild> args)
    {
        var pk = ParentModel.GetPrimaryKey();

        var fk = ModelExtensions.GetForeignKey<TModel, TChild>();

        args.Model.SetPropertyValue(fk, ParentModel.GetPropertyValue(pk));

        return base.Create(args);
    }

    public virtual async Task ReadByParent()
    {
        await ExecuteAsync(async () =>
        {
            var pk = ParentModel.GetPrimaryKey();

            var fk = ModelExtensions.GetForeignKey<TModel, TChild>();
            var result = await Service.FindByParent(ParentModel.GetPropertyValue(pk).ToString(), fk);

            DataSource = result;
            return result;
        });
      

        //DataSource.AddRange(result);
    }

}