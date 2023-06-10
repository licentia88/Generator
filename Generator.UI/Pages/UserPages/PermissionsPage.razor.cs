using System;
using System.Data.Common;
using Generator.Shared.Models.ComponentModels;
using Generator.UI.Models;
using Humanizer;

namespace Generator.UI.Pages.UserPages
{
	public partial class PermissionsPage
	{
		public List<CODE_TABLE> AuthTypes { get; set; }

		public PermissionsPage()
		{
			AuthTypes = new List<CODE_TABLE>
			{
				new CODE_TABLE{ C_CODE = nameof(ROLES), C_DESC = nameof(ROLES).Humanize()},
				new CODE_TABLE{ C_CODE = nameof(PERMISSIONS), C_DESC = nameof(PERMISSIONS).Humanize()}
			};

        }

        protected override async Task OnInitializedAsync()
        {
			if(ParentModel is not null)
            {
                DataSource = await	Service.FindByComponent(ParentModel.CB_ROWID);
			}

            await base.OnInitializedAsync();
        }
    }
}

