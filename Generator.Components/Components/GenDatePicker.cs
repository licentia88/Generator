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
    public class GenDatePicker : MudDatePicker, IGenDatePicker, IComponentMethods<GenDatePicker>
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


        [CascadingParameter(Name = nameof(IsSearchField))]
        public bool IsSearchField { get; set; }

       
        protected override Task OnInitializedAsync()
        {
            if (IsSearchField)
                ParentGrid?.AddSearchFieldComponent(this);
            else
            {
                if(Model is not null)
                {
                    Date = (DateTime?)Model?.GetPropertyValue(BindingField);
                }

                ParentGrid?.AddChildComponent(this);
            }




            return Task.CompletedTask;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Model is not null || ParentGrid.IsRendered)
                base.BuildRenderTree(builder);

        }

        public void SetValue(DateTime? date)
        {
            Model?.SetPropertyValue(BindingField, date);

            //ParentGrid.ValidateValue(BindingField);
         }

        protected override void OnClosed()
        {
            if (!Error && !IsSearchField)
                 ParentGrid.ValidateValue(BindingField);

            base.OnClosed();
        }

        public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => (builder) =>
        {
           
            Model = model;

            if (!DateChanged.HasDelegate)
            {
                DateChanged = EventCallback.Factory.Create<DateTime?>(this, x => SetValue(x.CastTo<DateTime?>()));
            }

            var valDate = (DateTime?)Model?.GetPropertyValue(BindingField);


                builder.RenderComponent(this, ignoreLabels,(nameof(_value),valDate), (nameof(Disabled), !EditorEnabled));

        };

        public RenderFragment RenderAsGridComponent(object model) => (builder) =>
        {
            var val = (DateTime?)model.GetPropertyValue(BindingField);

           

            if (val is not null)
                RenderExtensions.RenderGrid(builder, val.Value.ToString(DateFormat));

        };

        public void ValidateObject()
        {
            ParentGrid.ValidateValue(BindingField);
        }
        public object GetValue()
        {
            return this.GetFieldValue(nameof(_value));
        }

    }
}


