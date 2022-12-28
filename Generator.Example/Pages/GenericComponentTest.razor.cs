using System.Collections.ObjectModel;
using Generator.Components.Args;
using Generator.Components.Components;
using Generator.Components.Enums;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Generator.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace Generator.Example.Pages
{
	public partial class GenericComponentTest:ComponentBase
	{
        public GenGrid GridRef { get; set; }


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

        public ValueTask OnCreate(GenGridArgs args)
        {
            args.NewData.SetPropertyValue("TT_ROWID", "TEST");

            if (args.EditMode != EditMode.Inline)
            {
                InternalDataSource.Insert(0, args.NewData);
            }
           
            return ValueTask.CompletedTask;
        }

        public ValueTask Cancel(GenGridArgs args)
        {
            return ValueTask.CompletedTask;
        }

        public ValueTask OnUpdate(GenGridArgs args)
        {
            //InternalDataSource.Replace(args.OldData, args.NewData);

            //InternalDataSource =  InternalDataSource.ReplaceExistingData(args.OldData, args.NewData).ToList();
            return ValueTask.CompletedTask;
        }

        public ValueTask OnDelete(GenGridArgs args)
        {
            InternalDataSource.Remove(args.OldData);

            return ValueTask.CompletedTask;
        }

        public ValueTask OnLoad(IGenView view)
        {
            if(view.ViewState == ViewState.Create)
            {
                var prop = view.GetPropertyValue(nameof(GenGrid.SelectedItem));
                prop.SetPropertyValue("TT_ROWID", "222");

                //InternalDataSource.Insert(0, prop);
            }
           
            //test.Value = "221";
            return ValueTask.CompletedTask;
        }

    }
}

