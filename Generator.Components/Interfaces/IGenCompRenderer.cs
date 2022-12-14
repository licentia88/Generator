using Generator.Components.Enums;
using Microsoft.AspNetCore.Components;

namespace Generator.Components.Interfaces;

public interface IGenCompRenderer
{
    //MudComponentBase parent,
    public RenderFragment RenderAsComponent(object model,bool ignoreLabels = false);

    public RenderFragment RenderAsGridComponent(object model);
}


