using System;
using System.Collections.ObjectModel;
using Generator.Components.Args;
using Generator.Examples.Shared;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;

namespace Generator.Example.Pages
{
	public partial class Users
	{
		[Inject]
		public IUserService UserService { get; set; }

        public List<USER> DataSource { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var res  = await UserService.ReadAsync();

            DataSource =res;//;.ToList<object>();
        }

        public async ValueTask CreateAsync(GenGridArgs args)
        {
            if (args.NewData is not USER data) return;

            var result = await UserService.CreateAsync(new RESPONSE_REQUEST<USER>(data));

            DataSource.Add(result);
        }

        public async ValueTask UpdateAsync(GenGridArgs args)
        {
            if (args.NewData is not USER data) return;

            var result = await UserService.UpdateAsync(new RESPONSE_REQUEST<USER>(data));

            DataSource.Replace(data, result);

        }

        public async ValueTask DeleteAsync(GenGridArgs args)
        {
            if (args.NewData is not USER data) return;

            var result = await UserService.DeleteAsync(new RESPONSE_REQUEST<USER>(data));

            DataSource.Remove(data);

        }

        

    }
}

