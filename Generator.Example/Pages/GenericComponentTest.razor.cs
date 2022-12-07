using System;
using System.Reflection;
using Generator.Components.Components;
using Generator.Shared.Extensions;
using Generator.Shared.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.Example.Pages
{
	public partial class GenericComponentTest:ComponentBase
	{
        public List<object> InternalDataSource { get; set; }

        [Inject]
        public ITestService ITestService { get; set; }

        public IDictionary<string,object> firstData { get; set; }

        public GenTextField GenTextField { get; set; }


        protected  override async Task OnInitializedAsync()
        {
            await QueryAsync();

        }
        private async Task QueryAsync()
        {
            var result = await ITestService.QueryAsync();
            var data = result.Data.Deserialize<List<IDictionary<string, object>>>();
            InternalDataSource = new List<object>(data);

            firstData = data.First();

            GenTextField = new GenTextField();
            GenTextField.Variant = MudBlazor.Variant.Outlined;
             
            GenTextField.BindingField = "TT_DESC";
             

            Console.WriteLine();
        }
        
    }
}

