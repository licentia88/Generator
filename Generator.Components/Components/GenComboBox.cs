using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Generator.Components.Components
{
    public class GenComboBox : MudSelect<object>, IGenComboBox
    {
        [CascadingParameter(Name = nameof(ParentComponent))]
        public GenGrid ParentComponent { get; set; }

        [Parameter, EditorRequired]
        public string DisplayField { get; set; }

        [Parameter, EditorRequired]
        public string ValueField { get; set; }

        [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
        public object Model { get; set; }

        [Parameter]
        [EditorRequired]
        public string BindingField { get; set; }

        public Type DataType { get; set; } = typeof(object);

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

        [Parameter, EditorRequired]
        public IEnumerable<object> DataSource { get; set; }

        protected override Task OnInitializedAsync()
        {
            ParentComponent?.AddChildComponent(this);

            return Task.CompletedTask;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Model is not null )
                base.BuildRenderTree(builder);
        }

        public void OnValueChanged(object value)
        {
            if (value is null) return;
            Model.SetPropertyValue(BindingField, value.GetPropertyValue(ValueField));
        }

        public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => builder =>
        {
            Model = model;
            ToStringFunc = x => x?.GetPropertyValue(DisplayField)?.ToString();
            ValueChanged = EventCallback.Factory.Create<object>(this, OnValueChanged);

            var innerFragment = (nameof(ChildContent), (RenderFragment)(treeBuilder =>
            {
                var i = 1000;

                foreach (var data in DataSource)
                {
                    treeBuilder.OpenComponent(i++, typeof(MudSelectItem<object>));

                    treeBuilder.AddAttribute(i++, nameof(Value), data);

                    treeBuilder.CloseComponent();
                }
            }));

            var loValue = DataSource.FirstOrDefault(x => x.GetPropertyValue(ValueField)?.ToString() == model.GetPropertyValue(BindingField)?.ToString());

            builder.RenderComponent(this,ignoreLabels, (nameof(Value), loValue), innerFragment);
        };

        public RenderFragment RenderAsGridComponent(object model) => (builder) =>
        {
            var selectedField = DataSource.FirstOrDefault(x => x.GetPropertyValue(ValueField)?.ToString() == model.GetPropertyValue(BindingField)?.ToString());

            RenderExtensions.RenderGrid(builder, selectedField.GetPropertyValue(DisplayField));
        };
    }
}

