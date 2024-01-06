using Generator.Components.Enums;
using Microsoft.AspNetCore.Components;

namespace Generator.Components.Interfaces;


public interface INonGenView: IPageBase
{
    public bool IsIndividual { get; set; }

    public string Title { get; set; }

    public ViewState ViewState { get; set; }

    public EditMode EditMode { get; set; }

    public List<(Type type, IGenComponent component)> Components { get; set; }

    public List<IGenControl> SearchFieldComponents { get; set; }

    public TComponent GetComponent<TComponent>(string bindingField) where TComponent : IGenControl;

    public TComponent GetSearchFieldComponent<TComponent>(string bindingField) where TComponent : IGenControl;

    bool IsTopLevel { get; set; }


    Task OnCommit();

    Task OnCommitAndWait();

    public bool HasErrors();

    public INonGenGrid Parent { get; set; }


}

/// <summary>
/// Has common properties 
/// </summary>
/// <typeparam name="TModel"></typeparam>
public interface IGenView<TModel> : INonGenView where TModel:new()
{
    TModel OriginalEditItem { get; set; }

    public TModel SelectedItem { get;  set; }

    public Dictionary<string,object> Parameters { get; set; }

    public bool ShouldShowDialog { get; set; }

    public EventCallback<IGenView<TModel>> Load { get; set; }

    Task OnCommit(TModel model);

    Task OnCommit(TModel model, ViewState viewState);

}

