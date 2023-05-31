using System;
using Generator.Client;
using Generator.Components.Args;
using Generator.Components.Components;
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

    [Inject]
    public AuthService AuthService { get; set; }

    public List<TModel> DataSource { get; set; } = new List<TModel>();

    protected override Task OnInitializedAsync()
    {
        NotificationsView.Snackbar = Snackbar;
        NotificationsView.Notifications = new();
        return base.OnInitializedAsync();
    }

    public virtual async Task Create(GenArgs<TModel> model)
    {
        var service = ((IGenericService<TServiceInterface, TModel>)Service);

        var result = await service.Create(model.Data);

        var primaryKey = model.GetPrimaryKey();

        model.SetPropertyValue(primaryKey, result.GetPropertyValue(primaryKey));

        model.Data = result;
 
        DataSource.Add(result);
    }

    public virtual async Task Read(SearchArgs args)
    {
        var service = ((IGenericService<TServiceInterface, TModel>)Service);

        var result = await service.ReadAll();

        DataSource.AddRange(result);
    }

    public virtual async Task Update(GenArgs<TModel> model)
    {
        var service = ((IGenericService<TServiceInterface, TModel>)Service);

        var result = await service.Update(model.Data);

        //Datasource da mevcut Datayi replace yap
    }

    public virtual async Task Delete(GenArgs<TModel> model)
    {
        var service = ((IGenericService<TServiceInterface, TModel>)Service);

        var result = await service.Delete(model.Data);

        DataSource.Remove(model.Data);
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