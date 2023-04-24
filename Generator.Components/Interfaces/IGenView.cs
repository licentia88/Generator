using Generator.Components.Args;
using Generator.Components.Enums;
using Microsoft.AspNetCore.Components;

namespace Generator.Components.Interfaces;


public interface INonGenView
{
    public string Title { get; set; }

    public ViewState ViewState { get; set; }

    public EditMode EditMode { get; set; }

    public List<IGenComponent> Components { get; set; }

    public List<IGenComponent> SearchFieldComponents { get; set; }

    public TComponent GetComponent<TComponent>(string bindingField) where TComponent : IGenComponent;

    public TComponent GetSearchFieldComponent<TComponent>(string bindingField) where TComponent : IGenComponent;

    public bool IsTopLevel { get; set; }

    public void StateHasChanged();


    public Task OnCommit();

    public Task OnCommitAndWait();

}

/// <summary>
/// Has common properties 
/// </summary>
/// <typeparam name="TModel"></typeparam>
public interface IGenView<TModel> : INonGenView where TModel:new()
{
    public TModel OriginalEditItem { get; set; }

    public TModel SelectedItem { get;  set; }

    public Dictionary<string,object> Parameters { get; set; }

    public bool ShoulShowDialog { get; set; }

    //public EventCallback<IGenView<TModel>> Load { get; set; }

    public Task OnCommit(TModel model);

    public Task OnCommit(TModel model, ViewState viewState);
}

