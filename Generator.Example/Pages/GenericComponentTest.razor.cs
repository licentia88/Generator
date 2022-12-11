using System;
using System.Reflection;
using Generator.Components.Components;
using Generator.Shared.Extensions;
using Generator.Shared.Services;
using Generator.Shared.TEST_WILL_DELETE_LATER;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.Example.Pages
{
	public partial class GenericComponentTest:ComponentBase
	{
        public GenGrid GridRef { get; set; }


        public ICollection<object> InternalDataSource { get; set; }

        [Inject]
        public ITestService ITestService { get; set; }

 
        public GenTextField GenTextField { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await QueryAsync();
        }

        
        private async Task QueryAsync()
        {
            var result = await ITestService.QueryAsync();

            InternalDataSource = result.GenObject.DynamicData().ToList();
            
        }
        
    }
}

