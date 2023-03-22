using Generator.Shared.Services;
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

        private async void ParametricQuery()
        {
            var result = await ITestService.ParametricQuery();

            var data = result.GenObject.DynamicData().First();
        }

        private async void ExecuteSP()
        {
            var result = await ITestService.ExecuteSp();

            var data = result.GenObject.DynamicData().First();
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

        private async void UpdateCodeTest()
        {
            var result = await ITestService.UpdateCodeTest();

            var data = result.GenObject.DynamicData().First();
        }

        private async void UpdateIdentityTest()
        {
            var result = await ITestService.UpdateIdentityTest();

            var testREsult = result.GenObject.DynamicData().First();
        }

        private async void UpdateComputedTest()
        {
            var result = await ITestService.UpdateComputedTest();

            var data = result.GenObject.DynamicData().First();//.Deserialize<IDictionary<string, object>>();
        }

        private async void DeleteCodeTest()
        {
            var result = await ITestService.DeleteWithCodeTableTest();

            var data = result.GenObject.DynamicData().First();
        }

        private async void DeleteIdentityTest()
        {
            var result = await ITestService.DeleteWithIdentityTest();

            var testREsult = result.GenObject.DynamicData().First();
        }

        private async void DeleteComputedTest()
        {
            var result = await ITestService.DeleteWithoutIdentityTest();

            var data = result.GenObject.DynamicData().First();//.Deserialize<IDictionary<string, object>>();
        }

        private async void DeleteCodeTestObject()
        {
            var result = await ITestService.DeleteWithCodeTableTestObject();

            var data = result.GenObject.DynamicData().First();
        }

        private async void DeleteIdentityTestObject()
        {
            var result = await ITestService.DeleteWithIdentityTestObject();

            var testREsult = result.GenObject.DynamicData().First();
        }

        private async void DeleteComputedTestObject()
        {
            var result = await ITestService.DeleteWithoutIdentityTestObject();

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
            CancellationTokenSource cts = new CancellationTokenSource();

             await foreach (var item in ITestService.QueryStream(cts.Token))
            {
                var streamedData = item.GenObject.DynamicData().ToList();

                DataSource.AddRange(streamedData);

                StateHasChanged();

                //cts.Cancel();
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

        private async void UpdateCodeModelTest()
        {
            var result = await ITestService.UpdateCodeModelTest();

            var data = result.GenObject.DynamicData().First();
        }

        private async void UpdateIdentityModelTest()
        {
            var result = await ITestService.UpdateIdentityModelTest();

            var testREsult = result.GenObject.DynamicData().First();
        }

        private async void UpdateComputedModelTest()
        {
            var result = await ITestService.UpdateComputedModelTest();

            var data = result.GenObject.DynamicData().First();//.Deserialize<IDictionary<string, object>>();
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

