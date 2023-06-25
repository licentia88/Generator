namespace Generator.UI.Pages.HubPages;

public partial class HubTestPage
{

    protected override Task OnInitializedAsync()
    {
        var data = Service.ReadAsync();

        return base.OnInitializedAsync();
    }
}