using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Generator.Components.Validators;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Generator.Components.Components
{
    public class GenDatePicker : MudDatePicker, IGenDatePicker
    {
        [CascadingParameter(Name = nameof(ParentGrid))]
        public INonGenGrid  ParentGrid { get; set; }

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
        public bool EditorEnabled { get; set; } = true;

        [Parameter]
        public bool EditorVisible { get; set; } = true;

        [Parameter]
        public bool GridVisible { get; set; } = true;

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

        public IGenComponent Reference { get; set; }

        protected override Task OnInitializedAsync()
        {
            ParentGrid?.AddChildComponent(this);

            //To do check if this line vcan be removed
            Date = (DateTime?)Model?.GetPropertyValue(BindingField);

            return Task.CompletedTask;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Model is not null )
                base.BuildRenderTree(builder);
        }

        public void OnDateChanged(DateTime? date)
        {
            Model.SetPropertyValue(BindingField, date);

            ParentGrid.ValidateValue(BindingField);

        }

        protected override void OnClosed()
        {
            if (!Error)
                 ParentGrid.ValidateValue(BindingField);

            base.OnClosed();
        }

        public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => (builder) =>
        {
            Model = model;

            DateChanged = EventCallback.Factory.Create<DateTime?>(this, x =>  OnDateChanged(x));


            //Date = (DateTime?)model.GetPropertyValue(BindingField);

            builder.RenderComponent(this,ignoreLabels, (nameof(Disabled), !EditorEnabled));
        };

        public RenderFragment RenderAsGridComponent(object model) => (builder) =>
        {
            var val = (DateTime?)model.GetPropertyValue(BindingField);

            if(val is not null)
                RenderExtensions.RenderGrid(builder, val.Value.ToString(DateFormat));

        };

        public void ValidateObject()
        {
            ParentGrid.ValidateValue(BindingField);
        }
    }
}


