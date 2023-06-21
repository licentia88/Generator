using System;
using Generator.Shared.Models.ComponentModels;
using Generator.UI.Models;
using Humanizer;
using Microsoft.AspNetCore.Components;

namespace Generator.UI.Pages.UserPages;


public partial class RolesPage
{
    [Inject]
    public List<ROLES> RolesList { get; set; }

    protected override Task OnInitializedAsync()
    {
        DataSource = RolesList;
        return base.OnInitializedAsync();
    }
}

