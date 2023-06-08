using System;
using Generator.Shared.Models.ComponentModels;
using Generator.UI.Models;
using Humanizer;

namespace Generator.UI.Pages.UserPages
{
	public partial class RolesPage
	{
        public List<CODE_TABLE> AuthTypes { get; set; }

        public RolesPage()
        {
            AuthTypes = new List<CODE_TABLE>
            {
                new CODE_TABLE{ C_CODE = nameof(ROLES), C_DESC = nameof(ROLES).Humanize()},
                new CODE_TABLE{ C_CODE = nameof(PERMISSIONS), C_DESC = nameof(PERMISSIONS).Humanize()}
            };

        }
    }
}

