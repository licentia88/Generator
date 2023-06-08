using System;
using Generator.Shared.Models.ComponentModels;
using Generator.UI.Models;
using Humanizer;
using Microsoft.AspNetCore.Components;

namespace Generator.UI.Pages.UserPages;

public partial class RolesDetailsPage
{
    [Inject]
    public List<PERMISSIONS> PermissionsList { get; set; }


}

