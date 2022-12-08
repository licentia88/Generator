using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Generator.Components.Enums;
using Generator.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.Components.Components
{
	public class GenSpacer:IGenSpacer
	{
        [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
        public string BindingField { get; set; }

        [Parameter, AllowNull]
        [Range(1, 12, ErrorMessage = "Column width must be between 1 and 12")]
        public int Width { get; set; }

        [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
        public int Order { get; set; }

        [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
        public bool VisibleOnEdit { get; set; } = true;

        [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
        public bool VisibleOnGrid { get; set; } = true;

        [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
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
}

