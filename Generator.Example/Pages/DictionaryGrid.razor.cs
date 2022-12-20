namespace Generator.Example.Pages
{
    public partial class DictionaryGrid
    {
        private ICollection<object> DataSource { get; }

        public DictionaryGrid()
        {
            DataSource = new List<object>();
        }

        protected override Task OnInitializedAsync()
        {
            //var data = await Service.QueryAsync();

            //var result2 = data.Data.Deserialize<object>();
            //var result = data.Data.Deserialize<List<IDictionary<string,object>>>();

            //((List<object>)DataSource).AddRange(result);

            StateHasChanged();
            return Task.CompletedTask;
        }
    }
}

