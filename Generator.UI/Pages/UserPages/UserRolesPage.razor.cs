using System;
using Generator.Client;
using Generator.Client.Services;
using Generator.Shared.Models.ComponentModels;
using Microsoft.AspNetCore.Components;

namespace Generator.UI.Pages.UserPages
{
	public partial class UserRolesPage
    {
        [Inject]
		public RolesService RolesService { get; set; }

        public List<ROLES> RolesList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            RolesList = await RolesService.ReadAll();

            await base.OnInitializedAsync();
        }
    }
}

