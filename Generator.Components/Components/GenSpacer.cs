using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Generator.Components.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Generator.Components.Components
{
	public class GenSpacer:IGenSpacer
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


        [EditorBrowsable(EditorBrowsableState.Never)]
        public GenGrid ParentComponent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        [EditorBrowsable(EditorBrowsableState.Never)]
        public object Model { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


       
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Label { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Type DataType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public object GetDefaultValue => throw new NotImplementedException();

        public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false)
        {
            throw new NotImplementedException();
        }

        public RenderFragment RenderAsGridComponent(object model)
        {
            throw new NotImplementedException();
        }
    }
}

