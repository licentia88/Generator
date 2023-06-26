using System;
using Generator.Client.Hubs;
using Microsoft.AspNetCore.Components;

namespace Generator.Example.Pages;

public partial class Index
{
	[Inject]
	public PermissionHub PermissionHub { get; set; }

    protected override Task OnInitializedAsync()
    {
        return base.OnInitializedAsync();
    }

    public async Task OnTest() {

        await  PermissionHub.CreateAsync(new Generator.Shared.Models.ComponentModels.PERMISSIONS());
        Console.WriteLine();
    }
}

