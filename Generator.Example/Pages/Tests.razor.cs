using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Generator.Components.Components;
using Generator.Shared.Extensions;
using Generator.Shared.Services;
using Generator.Shared.TEST_WILL_DELETE_LATER;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.Example.Pages
{

    public partial class Tests
    {
        public List<IDictionary<string, object>> DataSource { get; set; }

        

        [Inject]
        public ITestService ITestService { get; set; }

        private int currentCount = 0;

        protected override Task OnInitializedAsync()
        {
            DataSource = new List<IDictionary<string, object>>();

          


            return base.OnInitializedAsync();
        }

        private async void InsertWithCodeTableTest()
        {
            var result = await ITestService.InsertWithCodeTableTest();

            var data = result.Data.Deserialize<IDictionary<string, object>>();
        }

        private async void InsertWithIdentityTest()
        {
            var result = await ITestService.InsertWithIdentityTest();

            var data = result.Data.Deserialize<IDictionary<string, object>>();
        }

        private async void InsertWithoutIdentityTest()
        {
            var result = await ITestService.InsertWithoutIdentityTest();

            var data = result.Data.Deserialize<IDictionary<string, object>>();
        }

        private IDictionary<string, object> firstData;
        //public GenTextField Txtfield { get; set; }


        private async void QueryAsync()
        {
          
            var result = await ITestService.QueryAsync();
            var data = result.Data.Deserialize<List<IDictionary<string, object>>>();

            //Txtfield = new(data.First());
            firstData = data.First();

            
           

            StateHasChanged();
        }

        private async void QueryStream()
        {
            await foreach (var item in ITestService.QueryStream())
            {
                var streamedData = item.Data.Deserialize<ObservableCollection<IDictionary<string, object>>>();

                DataSource.AddRange(streamedData);

                StateHasChanged();

            }
        }

        Converter<object> StringConverter = new Converter<object>
        {
            SetFunc = value => value.ToString(),
            GetFunc = text => text.ToString(),
        };

        Converter<object> DateConverter = new Converter<object>
        {
            SetFunc = value => value.ToString(),
            GetFunc = text => Convert.ToDateTime(text),
        };

        private async void InsertWithCodeTableTestObject()
        {
            var result = await ITestService.InsertWithCodeTableTestObject();

            var data = result.Data.Deserialize<object>();
        }

        private async void InsertWithIdentityTestObject()
        {
            var result = await ITestService.InsertWithIdentityTestObject();

            var data = result.Data.Deserialize<object>();
        }

        private async void InsertWithoutIdentityTestObject()
        {
            var result = await ITestService.InsertWithoutIdentityTestObject();

            var data = result.Data.Deserialize<object>();
        }

        private async void QueryAsyncObject()
        {
            var result = await ITestService.QueryAsyncObject();

            //var data = result.Data.Deserialize<List<TEST_TABLE>>();
            var data = result.Data.Deserialize<List<object>>();
        }

        private async void QueryStreamObject()
        {
            await foreach (var item in ITestService.QueryStreamObject())
            {
                var streamedData = item.Data.Deserialize<ObservableCollection<object>>();

                //DataSource.AddRange(streamedData);

                StateHasChanged();

            }
        }
    }
}

