using System;
using System.Data.Common;
using Generator.Shared.Models.ComponentModels;
using Generator.UI.Models;
using Humanizer;
using Microsoft.AspNetCore.Components;

namespace Generator.UI.Pages.UserPages
{
	public partial class PermissionsPage
	{
        [Inject]
        public List<PERMISSIONS> PermissionsList { get; set; }

        private Func<PERMISSIONS, bool> whereClause; 

        protected override async Task OnInitializedAsync()
        {
            DataSource = PermissionsList;

            if (ParentModel is not null)
                whereClause = x => x.PER_COMPONENT_REFNO == ParentModel.CB_ROWID;
            else
                whereClause = x => x.AUTH_TYPE == nameof(PERMISSIONS);

            await base.OnInitializedAsync();
        }
    }
}

