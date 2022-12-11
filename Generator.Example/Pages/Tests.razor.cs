﻿using Generator.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace Generator.Example.Pages
{

    public partial class Tests
    {
        [Inject]
        public ITestService ITestService { get; set; }

        public List<object> DataSource { get; set; }

        private int currentCount = 0;

        protected override Task OnInitializedAsync()
        {
            DataSource = new List<object>();

            return base.OnInitializedAsync();
        }

        private async void InsertWithCodeTableTest()
        {
            var result = await ITestService.InsertWithCodeTableTest();

            var data = result.GenObject.DynamicData().First();
        }

        private async void InsertWithIdentityTest()
        {
            var result = await ITestService.InsertWithIdentityTest();

            var testREsult = result.GenObject.DynamicData().First();
        }

        private async void InsertWithoutIdentityTest()
        {
            var result = await ITestService.InsertWithoutIdentityTest();

            var data = result.GenObject.DynamicData().First();//.Deserialize<IDictionary<string, object>>();
        }



        private async void QueryAsync()
        {

            var result = await ITestService.QueryAsync();
            var data = result.GenObject.DynamicData().ToList();
 
            StateHasChanged();
        }

        private async void QueryStream()
        {
            await foreach (var item in ITestService.QueryStream())
            {
                var streamedData = item.GenObject.DynamicData().ToList();

                DataSource.AddRange(streamedData);

                StateHasChanged();

            }
        }

        

        private async void InsertWithCodeTableTestObject()
        {
            var result = await ITestService.InsertWithCodeTableTestObject();

            var data = result.GenObject.DynamicData().FirstOrDefault();
        }

        private async void InsertWithIdentityTestObject()
        {
            var result = await ITestService.InsertWithIdentityTestObject();

            var data = result.GenObject.DynamicData().FirstOrDefault();
        }

        private async void InsertWithoutIdentityTestObject()
        {
            var result = await ITestService.InsertWithoutIdentityTestObject();

            var data = result.GenObject.DynamicData().FirstOrDefault();
        }

        private async void QueryAsyncObject()
        {
            var result = await ITestService.QueryAsyncObject();

            var data = result.GenObject.DynamicData().FirstOrDefault();

        }

        private async void QueryStreamObject()
        {
            await foreach (var item in ITestService.QueryStreamObject())
            {
                var streamedData = item.GenObject.DynamicData().ToList();

                DataSource.AddRange(streamedData);

                StateHasChanged();

            }
        }
    }
}

