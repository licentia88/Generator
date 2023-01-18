using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using IComponent = Microsoft.AspNetCore.Components.IComponent;

namespace Generator.Components.Components
{
	public partial class GenSpacer:IGenSpacer
    {
        [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
        public string BindingField { get; set; }

        [Parameter]
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

        [Parameter, EditorRequired]
        public int xs { get; set; }

        [Parameter, EditorRequired]
        public int sm { get; set; }

        [Parameter, EditorRequired]
        public int md { get; set; }

        [Parameter, EditorRequired]
        public int lg { get; set; }

        [Parameter, EditorRequired]
        public int xl { get; set; }

        [Parameter, EditorRequired]
        public int xxl { get; set; }

        [CascadingParameter(Name = nameof(ParentComponent))]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public INonGenGrid ParentComponent { get; set; }


        [EditorBrowsable(EditorBrowsableState.Never)]
        public object Model { get; set; }


        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Label { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Type DataType { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public object GetDefaultValue { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool Required { get; set; }

        public bool Error { get; set; }

        public string ErrorText { get; set; }

        protected override Task OnInitializedAsync()
        {
            ParentComponent.CastTo<GenGrid<dynamic>>()?.AddChildComponent(this);


            return base.OnInitializedAsync();
        }

        public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => (builder) =>
        {

        };
        

        public RenderFragment RenderAsGridComponent(object model) => (builder) =>
        {

        };

        public Task ValidateObject()
        {
            return Task.CompletedTask;
        }
    }
}

