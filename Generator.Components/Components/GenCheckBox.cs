using Microsoft.AspNetCore.Components;
using Generator.Components.Enums;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;

namespace Generator.Components.Components;

public class GenCheckBox : MudCheckBox<object>, IGenCheckBox
{
    [Parameter]
    [EditorRequired()]
    public string BindingField { get; set; }

    public Type DataType { get; set; } = typeof(bool);

    public object GetDefaultValue => DataType.GetDefaultValue();

    [Parameter, AllowNull]
    [Range(1, 12, ErrorMessage = "Column width must be between 1 and 12")]
    public int Width { get; set; }

    [Parameter, AllowNull]
    public int Order { get; set; }

    [Parameter, AllowNull]
    public bool VisibleOnEdit { get; set; } = true;

    [Parameter, AllowNull]
    public bool VisibleOnGrid { get; set; } = true;

    [Parameter, AllowNull]
    public bool EnabledOnEdit { get; set; } = true;

    [Parameter]
    public int xs { get; set; }

    [Parameter]
    public int sm { get; set; }

    [Parameter]
    public int md { get; set; }

    [Parameter]
    public int lg { get; set; }

    [Parameter]
    public int xl { get; set; }

    [Parameter]
    public int xxl { get; set; }
    public GenGrid ParentComponent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public object Model { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


    

    public RenderFragment RenderComponent(object model, ComponentType componentType)
    {
        throw new NotImplementedException();
    }
}





