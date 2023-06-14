using System;
using Generator.Components.Args;
using Generator.Shared.Models.ComponentModels;
using Generator.UI.Models;
using Humanizer;
using Microsoft.AspNetCore.Components;

namespace Generator.UI.Pages.UserPages;

public partial class RolesDetailsPage
{
    [Inject]
    public List<PERMISSIONS> PermissionsList { get; set; }


    protected override async Task OnInitializedAsync()
    {
        await  ReadByParent();
        await base.OnInitializedAsync();
    }

   
}

