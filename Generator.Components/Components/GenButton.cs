using System.ComponentModel;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;

namespace Generator.Components.Components;

public class GenButton:MudButton,IGenComponent
{
    protected override void OnInitialized()
    {
        base.OnInitialized();
        BindingField = Label;
        FullWidth = true;
        AddComponents();
    }

    protected override void BuildRenderTree(RenderTreeBuilder __builder)
    {
        if (((IGenComponent)this).Parent is not null && Model is not null)
            base.BuildRenderTree(__builder);
        
        AddComponents();
    }
    private void AddComponents()
    {
        ((IGenComponent)this).Parent?.AddChildComponent(this);

    }
    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false,
        params (string Key, object Value)[] valuePairs) => builder =>
    {
        Model = model;
        builder.RenderComponent(this,ignoreLabels);
    };

    public RenderFragment RenderAsGridComponent(object model)
    {
        return default;
    }

    [Parameter]
    public Func<object, bool> EditorVisibleIf { get; set; }
    
    [Parameter]
    public Func<object, bool> EditorEnabledIf { get; set; }
    
    [CascadingParameter(Name = nameof(IGenComponent.Parent))]
    public IPageBase Parent { get; set; }
    
    [Parameter,EditorBrowsable(EditorBrowsableState.Never)]
    public string BindingField { get; set; }
    [Parameter] public int Order { get; set; }
    [Parameter] public bool EditorEnabled { get; set; } = true;
    [Parameter]public bool EditorVisible { get; set; } = true;
    [Parameter,EditorBrowsable(EditorBrowsableState.Never)]
    public bool GridVisible { get; set; } = false;
    [Parameter] public int xs { get; set; }
    [Parameter] public int sm { get; set; }
    [Parameter] public int md { get; set; }
    [Parameter]public int lg { get; set; }
    [Parameter]public int xl { get; set; }
    [Parameter]public int xxl { get; set; }
    
    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }
    
    [Parameter,EditorRequired]
    public string Label { get; set; }

    public bool IsEditorVisible(object model)
    {
        return ((IGenComponent)this).EditorVisibleIf?.Invoke(model) ?? ((IGenComponent)this).EditorVisible;

    }
}