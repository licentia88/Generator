using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Generator.Components.ComponentInterfaces;
using Microsoft.AspNetCore.Components;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;

namespace Generator.Components.Components;

/// <summary>
/// Property olmayan controller icin
/// </summary>
public abstract class GenColumnBase: MudComponentBase
{
    [CascadingParameter(Name = nameof(ParentComponent))]
    public GenGrid ParentComponent { get; set; }

    #region Display


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
    public string Label { get; set; }

    #endregion

    #region Sizes

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
    #endregion
}


   

 

