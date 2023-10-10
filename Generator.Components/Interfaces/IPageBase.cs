namespace Generator.Components.Interfaces;

public interface IPageBase
{
    void AddChildComponent(IGenComponent component);

    void StateHasChanged();

}
