using System;
using Generator.Components.Components;
using Generator.Components.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;

namespace Generator.Components.Interfaces;

public interface IGenCompRenderer
{
    //MudComponentBase parent,
    public RenderFragment RenderComponent(object model, ComponentType componentType);


}


