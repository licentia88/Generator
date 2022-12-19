using Generator.Components.Args;
using Generator.Components.Components;
using Generator.Components.Enums;
using Microsoft.AspNetCore.Components;

namespace Generator.Components.Interfaces;

public interface IGenView : IGenCompRenderer
{
    public string Title { get; set; }

    public ViewState ViewState { get; set; }

    public EditMode EditMode { get; set; }

    public object OriginalEditItem { get; set; }

    public EventCallback<GenGridArgs> Create { get; set; }

    public EventCallback<GenGridArgs> Update { get; set; }

    public EventCallback<GenGridArgs> Delete { get; set; }

    //public EventCallback<IGenView> OnBeforeCreate { get; set; }

    //public EventCallback<IGenView> OnBeforeUpdate { get; set; }

    //public EventCallback<IGenView> OnBeforeDelete { get; set; }

    //public EventCallback<IGenView> OnLoad { get; set; }


    public TComponent GetComponent<TComponent>(string BindingField) where TComponent : IGenComponent;

}

public interface IGenGrid : IGenView
{
    
    public bool EnableSearch { get; set; }

    public bool IsFirstRender { get; set; }

    public IEnumerable<object> DataSource { get; set; }

    public bool NewDisabled { get; set; }

    public bool ExpandDisabled { get; set; }

    public bool SearchDisabled { get; set; }

    public RenderFragment GenColumns { get; set; }

    public RenderFragment<object> GenDetailGrid { get; set; }

    public bool  HasDetail { get;}

    public bool DetailClicked { get; set; }

    public GenGrid ParentComponent { get; set; }

    public string SearchPlaceHolderText { get; set; }

 



 }

