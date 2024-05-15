using Generator.Components.Enums;

namespace Generator.Components.Interfaces;

public interface IPageBase
{
    void AddChildComponent(IGenComponent component);

    void StateHasChanged();

    public bool Validate();

    bool IsValid { get; set; }

    Task OnCommitAndWait();

    public ViewState ViewState { get; set; }

    public object GetSelectedItem();

    public void Close();
}
