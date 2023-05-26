using System;
using Generator.Client;
using Generator.Components.Args;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Generator.Shared.Services.Base;
using Generator.UI.Extensions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.UI.Pages.Base;

public abstract class PagesBase<TModel, TService,TServiceInterface>:ComponentBase where TModel:new()
where TService :IClientService
where TServiceInterface : IGenericService<TServiceInterface, TModel>
{
    [Inject]
    public TService Service { get; set; }

    [Inject]
    public DatabaseService DatabaseService { get; set; }

    [Inject]
    public NotificationsView NotificationsView { get; set; }

    [Inject] ISnackbar Snackbar { get; set; }


    public List<TModel> DataSource { get; set; } = new List<TModel>();

    protected override Task OnInitializedAsync()
    {
        NotificationsView.Snackbar = Snackbar;
        NotificationsView.Notifications = new();
        return base.OnInitializedAsync();
    }

    public virtual async Task Create(TModel model)
    {
        var service = ((IGenericService<TServiceInterface, TModel>)Service);

        var result = await service.Create(model);

        var primaryKey = model.GetPrimaryKey();

        model.SetPropertyValue(primaryKey, result.GetPropertyValue(primaryKey));

        model = result;
 
        DataSource.Add(result);
    }

    public virtual async Task Read(SearchArgs args)
    {
        var service = ((IGenericService<TService, TModel>)Service);

        var result = await service.ReadAll();

        DataSource.AddRange(result);
    }

    public virtual async Task Update(TModel model)
    {
        var service = ((IGenericService<TService, TModel>)Service);

        var result = await service.Update(model);

        //Datasource da mevcut Datayi replace yap
    }

    public virtual async Task Delete(TModel model)
    {
        var service = ((IGenericService<TService, TModel>)Service);

        var result = await service.Delete(model);

        DataSource.Remove(model);
    }

    public abstract Task Load(IGenView<TModel> View);


 }

public  abstract class PagesBase<TModel, TChild, TService,TServiceInterface> : PagesBase<TChild, TService, TServiceInterface>
    where TService : IClientService
    where TModel : new() where TChild : new()
    where TServiceInterface : IGenericService<TServiceInterface, TChild>
{
    [Parameter]
    public TModel ParentModel { get; set; }


    public virtual async Task ReadByParent(SearchArgs args)
    {
        //var service = ((IGenericService<TService, TModel>)Service);

        //var result = await service.rea();

        //DataSource.AddRange(result);
    }

}