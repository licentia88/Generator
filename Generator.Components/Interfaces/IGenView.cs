using Generator.Components.Args;
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

    public EventCallback<GenGridArgs> Cancel { get; set; }

    public List<IGenComponent> Components { get; set; }

    public TComponent GetComponent<TComponent>(string bindingField) where TComponent : IGenComponent;
}
