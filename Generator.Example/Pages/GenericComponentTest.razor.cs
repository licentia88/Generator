using System.Collections.ObjectModel;
using Generator.Components.Components;
using Generator.Components.Enums;
using Generator.Components.Interfaces;
using Generator.Examples.Shared;
using Generator.Shared.Extensions;
using Generator.Shared.Services;
using Generator.Shared.TEST_WILL_DELETE_LATER;
using Microsoft.AspNetCore.Components;

namespace Generator.Example.Pages
{
	public partial class GenericComponentTest:ComponentBase
	{


        public ObservableCollection<object> InternalDataSource { get; set; }

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


            //InternalDataSource.add
            InternalDataSource = new ObservableCollection<object>(result.GenObject.DynamicData().Take(2)); ;

            ComboDataSource = comboResult.GenObject.DynamicData().ToList();

            Random rnd = new Random();

           
            var list = new List<string> { "batch", "belly", "carles", "DIY", "dreamcatcher", "fap", "fund" };

            foreach (IDictionary<string,object> item in InternalDataSource)
            {
                int number = rnd.Next(0, 6);

                item["TT_STRING_TABLE_CODE"] = list[number];
            }
          
        }

        public async ValueTask OnCreate(object data)
        {
            //args.NewData.SetPropertyValue("TT_ROWID", "TEST");

            await Task.Delay(3000);
            InternalDataSource.Insert(0, data);

            
        }

        public ValueTask Cancel(object args)
        {
            return ValueTask.CompletedTask;
        }

        public ValueTask OnUpdate(object data)
        {
            //InternalDataSource.Replace(args.OldData, args.NewData);

            //InternalDataSource =  InternalDataSource.ReplaceExistingData(args.OldData, args.NewData).ToList();
            return ValueTask.CompletedTask;
        }

        public ValueTask OnDelete(object data)
        {
            InternalDataSource.Remove(data);

            return ValueTask.CompletedTask;
        }

        public ValueTask OnLoad(IGenView<object> view)
        {
            if(view.ViewState == ViewState.Create)
            {
                var prop = view.GetPropertyValue(nameof(GenGrid<object>.SelectedItem));
                prop.SetPropertyValue("TT_ROWID", "222");

                //InternalDataSource.Insert(0, prop);
            }
           
            //test.Value = "221";
            return ValueTask.CompletedTask;
        }

    }
}

