using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Generator.Components.Components
{
    public class GenComboBox : MudSelect<object>, IGenComboBox, IComponentMethods<GenComboBox>
    {
        [CascadingParameter(Name = nameof(ParentGrid))]
        public INonGenGrid ParentGrid { get; set; }

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

        [Parameter, EditorRequired]
        public IEnumerable<object> DataSource { get; set; }

        //public IGenComponent Reference { get; set; }
        //[Parameter]
        //public Action<object> ValueChangedAction { get; set; }

        [CascadingParameter(Name = nameof(IsSearchField))]
        public bool IsSearchField { get; set; }


        protected override Task OnInitializedAsync()
        {
            if (IsSearchField)
                ParentGrid?.AddSearchFieldComponent(this);
            else
                ParentGrid?.AddChildComponent(this);


            return Task.CompletedTask;
        }

        public void Initialize()
        {
            if (ParentGrid.EditMode != Enums.EditMode.Inline && ParentGrid.CurrentGenPage is null) return;

        }


        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Model is not null && Model.GetType().Name != "Object")
                base.BuildRenderTree(builder);
        }


        public void OnClearClicked(MouseEventArgs arg)
        {
            
            Model.SetPropertyValue(BindingField, null);

        }

        public void SetValue(object value)
        {
            if (value is null) return;

            Model?.SetPropertyValue(BindingField, value.GetPropertyValue(ValueField));

            //ParentGrid.ValidateValue(BindingField);

        }

        private void SetCallBackEvents()
        {
            ToStringFunc = x => x?.GetPropertyValue(DisplayField)?.ToString();

            if (!ValueChanged.HasDelegate)
                ValueChanged = EventCallback.Factory.Create<object>(this, x => SetValue(x));

            if (IsSearchField)
            {
                OnClearButtonClick = EventCallback.Factory.Create<MouseEventArgs>(this, (MouseEventArgs arg) =>
                {
                    Model?.SetPropertyValue(BindingField, null);

                    ParentGrid.ValidateSearchFields(BindingField);
                });

                OnBlur = EventCallback.Factory.Create<FocusEventArgs>(this, () => ParentGrid.ValidateSearchFields(BindingField));
            }
            else
            {
 
                OnClearButtonClick = EventCallback.Factory.Create<MouseEventArgs>(this, (MouseEventArgs arg) => OnClearClicked(arg));

                OnBlur = EventCallback.Factory.Create<FocusEventArgs>(this, () => { ParentGrid.ValidateValue(BindingField); });
            }


        }

        public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => async builder =>
        {
            if (Model is null || Model.GetType().Name == "Object")
                Model = model;

            SetCallBackEvents();

            var innerFragment = (nameof(ChildContent), (RenderFragment)(treeBuilder =>
            {
                var i = 1000;

                if (DataSource is not null)
                {
                    foreach (var data in DataSource)
                    {
                        treeBuilder.OpenComponent(i++, typeof(MudSelectItem<object>));

                        treeBuilder.AddAttribute(i++, nameof(Value), data);

                        treeBuilder.CloseComponent();
                    }
                }

            }));


            var loValue = DataSource?.FirstOrDefault(x => x.GetPropertyValue(ValueField)?.ToString() == Model.GetPropertyValue(BindingField)?.ToString());

            builder.RenderComponent(this,ignoreLabels, (nameof(Value), loValue), (nameof(Disabled), !EditorEnabled), innerFragment);
        };

        public RenderFragment RenderAsGridComponent(object model) => (builder) =>
        {
            var selectedField = DataSource?.FirstOrDefault(x => x.GetPropertyValue(ValueField)?.ToString() == model.GetPropertyValue(BindingField)?.ToString());

            RenderExtensions.RenderGrid(builder, selectedField.GetPropertyValue(DisplayField));
        };

        public void ValidateObject()
        {
            ParentGrid.ValidateValue(BindingField);
        }

        public object GetValue()
        {
            return this.GetFieldValue(nameof(_value));
        }

        public void SetSearchValue(object Value)
        {
            Model.CastTo<Dictionary<string, object>>()[BindingField] = Value;
        }
         
    }
}

