using Generator.Client.Hubs;
using Generator.Shared.Models.ComponentModels;
using Microsoft.AspNetCore.Components;

namespace Generator.UI.Pages.UserPages
{
    public partial class PermissionsPage
	{
        [Inject]
        public PermissionHub PermissionHub { get; set; }

        private Func<PERMISSIONS, bool> whereClause;

        protected override async Task OnInitializedAsync()
        {
            await PermissionHub.StreamReadAsync();

            if (ParentModel is not null)
                whereClause = x => x.PER_COMPONENT_REFNO == ParentModel.CB_ROWID;
            else
                whereClause = x => x.AUTH_TYPE == nameof(PERMISSIONS);

            await base.OnInitializedAsync();
        }
    }
}

