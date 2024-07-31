using Generator.Components.Enums;
using MudBlazor;

namespace Generator.Components.Interfaces;

public interface IPageBase
{
    void AddChildComponent(IGenComponent component);

    void StateHasChanged();

    public bool Validate();

    bool IsValid { get; set; }

    Color TemplateColor { get; set; }

    public bool GridIsBusy { get; set; }

    Task OnCommitAndWait();

    public ViewState ViewState { get; set; }

    public object GetSelectedItem();

    public void Close();
}
