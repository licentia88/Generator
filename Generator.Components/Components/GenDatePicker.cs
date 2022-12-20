using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;

namespace Generator.Components.Components
{
	public class GenDatePicker:MudDatePicker,IGenDatePicker
	{
		 
        #region CascadingParameters
        [CascadingParameter(Name = nameof(ParentComponent))]
        public GenGrid ParentComponent { get; set; }
        #endregion
 
        [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
        public object Model { get; set; }

        [Parameter]
        [EditorRequired()]
        public string BindingField { get; set; }

        public Type DataType { get; set; } = typeof(DateTime);

        public object GetDefaultValue => DataType.GetDefaultValue();

        [Parameter]
        [Range(1, 12, ErrorMessage = "Column width must be between 1 and 12")]
        public int Width { get; set; }

        [Parameter]
        public int Order { get; set; }

        [Parameter]
        public bool VisibleOnEdit { get; set; } = true;

        [Parameter]
        public bool VisibleOnGrid { get; set; } = true;

        [Parameter]
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

        protected override Task OnInitializedAsync()
        {
            
            ParentComponent?.AddChildComponent(this);

            return Task.CompletedTask;
        }

        protected override void BuildRenderTree(RenderTreeBuilder __builder)
        {
            if (Model is not null && ParentComponent is not null)
                base.BuildRenderTree(__builder);
        }

 
        public void OnDateChanged(DateTime? date)
        {
            Model.SetPropertyValue(BindingField, date);
        }

       

        public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => (builder) =>
        {
            Model = model;

            DateChanged = EventCallback.Factory.Create<DateTime?>(this, OnDateChanged);

            Date = (DateTime?)model.GetPropertyValue(BindingField);

            builder.RenderComponent(new RenderParameters<GenDatePicker>(this,model,ignoreLabels));

        };
         

        public RenderFragment RenderAsGridComponent(object model) => (builder) =>
        {
            Date = (DateTime?)model.GetPropertyValue(BindingField);

            if (Date is null) return;
            
            RenderExtensions.RenderGrid(builder, Date.Value.ToString(DateFormat));
        };

         
    }
}

