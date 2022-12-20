using Generator.Shared.Services;
using Generator.Shared.TEST_WILL_DELETE_LATER;
using Microsoft.AspNetCore.Components;

namespace Generator.Example.Pages
{
    public partial class ModelGrid
    {
        public List<TEST_TABLE> DataSource { get; set; }

        [Inject]
        public ITestService Service { get; set; }

        public ModelGrid()
        {
            DataSource = new List<TEST_TABLE>();
        }

        protected override Task OnInitializedAsync()
        {
            return Task.CompletedTask;
            //var data = await Service.QueryAsyncObject();

            //var result = data.Data.Deserialize<List<TEST_TABLE>>();

            //DataSource.AddRange(result);

            //StateHasChanged();
        }
    }
}

