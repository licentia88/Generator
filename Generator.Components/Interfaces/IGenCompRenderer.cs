using System;
using Generator.Components.Components;
using Generator.Components.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;

namespace Generator.Components.Interfaces;

public interface IGenCompRenderer
{
    public RenderFragment Render(object Model, ComponentBase parent, ComponentType componentType, params (string key, object value)[] AdditionalParameters);


}


