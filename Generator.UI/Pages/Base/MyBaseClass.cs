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
