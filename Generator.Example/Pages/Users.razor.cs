using System;
using System.Collections.ObjectModel;
using Generator.Components.Args;
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

        public List<USER> DataSource { get; set; }

        private IGenView<USER> View { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var res  = await UserService.ReadAsync();

            DataSource =res;
        }

        public async ValueTask Load(IGenView<USER> view)
        {
            View = view;
        }

        public async ValueTask CreateAsync(GenGridArgs<USER> args)
        {
            if (args.NewItem is not USER data) return;

            var result = await UserService.CreateAsync(new RESPONSE_REQUEST<USER>(data));

            View.SelectedItem = result;

            DataSource.Add(result);
        }

        public async ValueTask UpdateAsync(GenGridArgs<USER> args)
        {
            if (args.NewItem is not USER data) return;

            var result = await UserService.UpdateAsync(new RESPONSE_REQUEST<USER>(data));

            DataSource.Replace(data, result);

        }

        public async ValueTask DeleteAsync(GenGridArgs<USER> args)
        {
            if (args.NewItem is not USER data) return;

            var result = await UserService.DeleteAsync(new RESPONSE_REQUEST<USER>(data));

            DataSource.Remove(data);
        }
    }
}

