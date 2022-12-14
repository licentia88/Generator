using Generator.Components.Enums;
using Microsoft.AspNetCore.Components;

namespace Generator.Components.Interfaces;

public interface IGenCompRenderer
{
    //MudComponentBase parent,
    public RenderFragment RenderAsComponent(object model);

    public RenderFragment RenderAsGridComponent(object model);
}


