namespace Generator.Example.Pages;

public partial class DictionaryGrid
{
    private List<object> DataSource { get; }

    public DictionaryGrid()
    {
        DataSource = new List<object>();
    }

    protected override Task OnInitializedAsync()
    {
            

        StateHasChanged();
        return Task.CompletedTask;
    }
}