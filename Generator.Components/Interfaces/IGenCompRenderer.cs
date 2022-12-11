using Generator.Components.Enums;
using Microsoft.AspNetCore.Components;

namespace Generator.Components.Interfaces;

public interface IGenCompRenderer
{
    //MudComponentBase parent,
    public RenderFragment RenderComponent(object model, ComponentType componentType);


}


