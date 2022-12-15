using System;
using System.Dynamic;
using Generator.Components.Components;
using Generator.Shared.Extensions;
using Generator.Shared.Services;
using Microsoft.AspNetCore.Components;
using static System.Net.WebRequestMethods;

namespace Generator.Example.Pages
{
	public partial class GenericComponentTest:ComponentBase
	{
        public GenGrid GridRef { get; set; }


        public ICollection<object> InternalDataSource { get; set; }

        public List<object> ComboDataSource  { get; set; }

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

            var comboResult = await ITestService.QueryTestStringDataAsync();

           

            InternalDataSource = result.GenObject.DynamicData().Take(2).ToList();

            ComboDataSource = comboResult.GenObject.DynamicData().ToList();

            Random rnd = new Random();

           
            var list = new List<string> { "batch", "belly", "carles", "DIY", "dreamcatcher", "fap", "fund" };

            foreach (IDictionary<string,object> item in InternalDataSource)
            {
                int number = rnd.Next(0, 6);

                item["TT_STRING_TABLE_CODE"] = list[number];
            }
          
        }
        
    }
}

