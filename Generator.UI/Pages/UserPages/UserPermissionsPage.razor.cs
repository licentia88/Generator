using System;
using Generator.Client;
using Generator.Shared.Models.ComponentModels;
using Microsoft.AspNetCore.Components;

namespace Generator.UI.Pages.UserPages
{
	public partial class UserPermissionsPage
    {
        [Inject]
		public PermissionsService PermissionsService { get; set; }

        public List<PERMISSIONS> PermissionsList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            PermissionsList = await PermissionsService.ReadAll();

            await base.OnInitializedAsync();
        }
    }
}

