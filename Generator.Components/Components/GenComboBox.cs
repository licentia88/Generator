using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;

namespace Generator.Components.Components
{
	public class GenComboBox:MudSelect<object>,IGenComboBox
	{
        #region CascadingParameters
        [CascadingParameter(Name = nameof(ParentComponent))]
        public GenGrid ParentComponent { get; set; }
        #endregion

        [Parameter,EditorRequired]
        public string DisplayField { get; set; }

        [Parameter, EditorRequired]
        public string ValueField { get; set; }

        [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
        public object Model { get; set; }

        [Parameter]
        [EditorRequired()]
        public string BindingField { get; set; }

        public Type DataType { get; set; } = typeof(object);

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

        [Parameter,EditorRequired]
        public IEnumerable<object> DataSource { get; set; }


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

        public void OnValueChanged(object value)
        {
            if (value is null) return;
            Model.SetPropertyValue(BindingField, value.GetPropertyValue(ValueField));

        }


        public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => (builder) =>
        {
            Model = model;
            ToStringFunc = x => x?.GetPropertyValue(DisplayField)?.ToString();
            ValueChanged = EventCallback.Factory.Create<object>(this, x => OnValueChanged(x));


            var innerFragment  = (nameof(ChildContent), (RenderFragment)((bldr) =>
            {
                var i = 1000;

                foreach (var data in DataSource)
                {
                    bldr.OpenComponent(i++, typeof(MudSelectItem<object>));

                    bldr.AddAttribute(i++, nameof(Value), data);

                    bldr.CloseComponent();
                }
            }));

            var loValue = DataSource.FirstOrDefault(x => x.GetPropertyValue(ValueField)?.ToString() == model.GetPropertyValue(BindingField)?.ToString()); 
            this.RenderComponent(model, builder,ignoreLabels, (nameof(Value), loValue), innerFragment);

        };
          
       

        public RenderFragment RenderAsGridComponent(object model) => (builder) =>
        {
            //Model = model;

            var selectedField = DataSource.FirstOrDefault(x => x.GetPropertyValue(ValueField)?.ToString() == model.GetPropertyValue(BindingField)?.ToString());

            this.RenderGrid(null, builder, selectedField.GetPropertyValue(DisplayField));
        };

    }
}

