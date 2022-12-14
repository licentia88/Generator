using Generator.Components.Components;
using Generator.Shared.Services;
using Microsoft.AspNetCore.Components;

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

            InternalDataSource = result.GenObject.DynamicData().Take(5).ToList();
            
        }
        
    }
}

