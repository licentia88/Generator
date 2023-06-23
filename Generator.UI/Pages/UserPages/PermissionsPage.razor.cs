using Generator.Shared.Enums;
using Generator.Shared.Models.ComponentModels;
using MessagePipe;
using Microsoft.AspNetCore.Components;

namespace Generator.UI.Pages.UserPages
{
    public partial class PermissionsPage
	{
        [Inject]
        public List<PERMISSIONS> PermissionsList { get; set; }

        private Func<PERMISSIONS, bool> whereClause;

        [Inject]
        public ISubscriber<Operation, PERMISSIONS> Subscriber { get; set; }

        protected override async Task OnInitializedAsync()
        {
            DataSource = PermissionsList;

            //Subscriber.Subscribe(async (Operation.PERMISSIONS obj) =>
            //{
            //    await InvokeAsync(StateHasChanged);
            //});
          

            if (ParentModel is not null)
                whereClause = x => x.PER_COMPONENT_REFNO == ParentModel.CB_ROWID;
            else
                whereClause = x => x.AUTH_TYPE == nameof(PERMISSIONS);

            await base.OnInitializedAsync();
        }
    }
}

