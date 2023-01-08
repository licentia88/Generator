using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Generator.Components.Validators;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;

namespace Generator.Components.Components
{
    public class GenDatePicker : MudDatePicker, IGenDatePicker
    {
        ObjectValidator<GenDatePicker> ObjectValidator = new ObjectValidator<GenDatePicker>();

        [CascadingParameter(Name = nameof(ParentComponent))]
        public dynamic  ParentComponent { get; set; }

        [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
        public object Model { get; set; }

        [Parameter]
        [EditorRequired]
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
        public int xs { get; set; } = 12;

        [Parameter]
        public int sm { get; set; } = 12;

        [Parameter]
        public int md { get; set; } = 12;

        [Parameter]
        public int lg { get; set; } = 12;

        [Parameter]
        public int xl { get; set; } = 12;

        [Parameter]
        public int xxl { get; set; } = 12;


        
        protected override Task OnInitializedAsync()
        {
            ParentComponent?.AddChildComponent(this);

            Date = (DateTime?)Model?.GetPropertyValue(BindingField);

            return Task.CompletedTask;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Model is not null)
                base.BuildRenderTree(builder);
        }

        public void OnDateChanged(DateTime? date)
        {
            Model.SetPropertyValue(BindingField, date);

            ValidateObject();
        }

        public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => (builder) =>
        {
            Model = model;

            DateChanged = EventCallback.Factory.Create<DateTime?>(this, OnDateChanged);

            //Date = (DateTime?)model.GetPropertyValue(BindingField);

            builder.RenderComponent(this,ignoreLabels);
        };

        public RenderFragment RenderAsGridComponent(object model) => (builder) =>
        {
            var val = (DateTime?)model.GetPropertyValue(BindingField);

            if(val is not null)
                RenderExtensions.RenderGrid(builder, val.Value.ToString(DateFormat));

        };

        public void ValidateObject()
        {
            ObjectValidator.Validate(this);
        }
    }
}


