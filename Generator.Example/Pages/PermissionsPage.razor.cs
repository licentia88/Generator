using System;
using Generator.Client.Hubs;
using Generator.Shared.Enums;
using Generator.Shared.Models.ComponentModels;
using MessagePipe;
using Microsoft.AspNetCore.Components;

namespace Generator.Example.Pages;

public partial class PermissionsPage
{
    [Inject]
    public PermissionHub PermissionHub { get; set; }

    [Inject]
    public ISubscriber<Operation,PERMISSIONS> Subscriber { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Subscriber.Subscribe(Operation.Create, async (PERMISSIONS model) =>
        {
            await InvokeAsync(StateHasChanged);
        });

        Subscriber.Subscribe(Operation.Update, async (PERMISSIONS model) =>
        {
            await InvokeAsync(StateHasChanged);
        });

        Subscriber.Subscribe(Operation.Delete, async (PERMISSIONS model) =>
        {
            await InvokeAsync(StateHasChanged);
        });
        await base.OnInitializedAsync();
    }
}

