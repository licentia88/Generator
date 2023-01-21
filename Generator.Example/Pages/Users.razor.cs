using System;
using System.Collections.ObjectModel;
using Generator.Components.Components;
using Generator.Components.Interfaces;
using Generator.Examples.Shared;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Generator.Example.Pages
{
	public partial class Users
	{
		[Inject]
		public IUserService UserService { get; set; }

        public ObservableCollection<USER> DataSource { get; set; } = new();

        private IGenView<USER> View { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(4000);

            var res  = await UserService.ReadAsync();

            

            DataSource = new ObservableCollection<USER>(res);

        }

        public async ValueTask Load(IGenView<USER> view)
        {
            View = view;
        }

        public async ValueTask CreateAsync(USER data)
        {
            //throw new Exception();
             var result = await UserService.CreateAsync(new RESPONSE_REQUEST<USER>(data));

            //REQUIRED 
            data.U_ROWID = result.U_ROWID;
            data = result;

            DataSource.Add(result);
        }

        public async ValueTask UpdateAsync(USER data)
        {
            var result = await UserService.UpdateAsync(new RESPONSE_REQUEST<USER>(data));

            var existing = DataSource.FirstOrDefault(x => x.U_ROWID == data.U_ROWID);

            DataSource.Replace(existing, result);

            //throw new Exception("TEST");

        }

        public async ValueTask DeleteAsync(USER data)
        {
            var result = await UserService.DeleteAsync(new RESPONSE_REQUEST<USER>(data));

            DataSource.Remove(data);
        }
    }
}

