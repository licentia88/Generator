using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Generator.Components.Components;

public class GenComboBox : MudSelect<object>, IGenComboBox, IComponentMethods<GenComboBox>
{
    [CascadingParameter(Name = nameof(IGenComponent.ParentGrid))]
    INonGenGrid  IGenComponent.ParentGrid { get; set; }

    [Parameter, EditorRequired]
    public string DisplayField { get; set; }

    [Parameter, EditorRequired]
    public string ValueField { get; set; }

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }

   

    [Parameter]
    [EditorRequired]
    public string BindingField { get; set; }

    Type IGenComponent.DataType { get; set; } = typeof(object);

    object IGenComponent.GetDefaultValue => ((IGenComponent)this).DataType.GetDefaultValue();

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
    public bool ClearIfNotVisible { get; set; } = false;

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

    [CascadingParameter(Name = nameof(IGenComponent.IsSearchField))]
    bool IGenComponent.IsSearchField { get; set; }

    [Parameter]
    public Func<object, bool> EditorVisibleIf { get; set; }

    [Parameter]
    public Func<object, bool> EditorEnabledIf { get; set; }

    [Parameter]
    public Func<object, bool> Where { get; set; }

    [Parameter]
    public Func<object, bool> RequiredIf { get; set; }

    protected override Task OnInitializedAsync()
    {
        AddComponents();
        return Task.CompletedTask;
    }

    private void AddComponents()
    {
        if (((IGenComponent)this).IsSearchField)
            ((IGenComponent)this).ParentGrid?.AddSearchFieldComponent(this);
        else
            ((IGenComponent)this).ParentGrid?.AddChildComponent(this);
        
    }
 
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Model is not null && Model.GetType().Name != "Object")
            base.BuildRenderTree(builder);
        
        AddComponents();
    }


    public void OnClearClicked(MouseEventArgs arg)
    {
        Model.SetPropertyValue(BindingField, null);

        ((IGenComponent)this).SetEmpty();

        ((IGenComponent)this).ParentGrid.StateHasChanged();
        ((IGenComponent)this).ParentGrid?.CurrentGenPage?.StateHasChanged();
    }

    public void SetValue(object value)
    {
        if (value is null) return;

        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        // ReSharper disable once HeuristicUnreachableCode
        if (this is not IGenComponent comp) return;

        if (comp.IsSearchField)
        {
            comp.SetSearchValue(value);

            comp.ParentGrid.ResetConditionalSearchFields();

        }
        else
        {
            Model?.SetPropertyValue(BindingField, value.GetPropertyValue(ValueField));

            Value = Model;
            _value = Model;
        }

        comp.ParentGrid.StateHasChanged();
        comp.ParentGrid.CurrentGenPage?.StateHasChanged();
    }

    private void SetCallBackEvents()
    {
        ToStringFunc = x => x?.GetPropertyValue(DisplayField)?.ToString();

        if (!ValueChanged.HasDelegate)
            ValueChanged = EventCallback.Factory.Create<object>(this, SetValue);

        if (!OnClearButtonClick.HasDelegate)
            OnClearButtonClick = EventCallback.Factory.Create<MouseEventArgs>(this, OnClearClicked);

        OnBlur = EventCallback.Factory.Create<FocusEventArgs>(this, () =>  Validate());


        

     }

    //public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => builder =>
    //{
    //    RenderAsComponent(model, ignoreLabels, new KeyValuePair<string, object>(nameof(Disabled), !(EditorEnabledFunc?.Invoke(Model) ?? EditorEnabled))).Invoke(builder);

    //};

    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false, params (string Key, object Value)[] valuePairs) => builder =>
    {
        if (Model is null || Model.GetType().Name == "Object")
            Model = model;

        SetCallBackEvents();

        var innerFragment = (nameof(ChildContent), (RenderFragment)(treeBuilder =>
        {
            var i = 1000;

            if (DataSource is null) return;

            foreach (var data in DataSource.Where(_ => Where?.Invoke(Model)??true))
            {
                treeBuilder.OpenComponent(i++, typeof(MudSelectItem<object>));

                treeBuilder.AddAttribute(i++, nameof(Value), data);

                treeBuilder.CloseComponent();
            }

        }));


        var loValue = DataSource?.FirstOrDefault(x => x.GetPropertyValue(ValueField)?.ToString() == Model.GetPropertyValue(BindingField)?.ToString());

        var additionalParams = valuePairs.Select(x => (x.Key, x.Value)).ToList();

        additionalParams.Add((nameof(Value), loValue));

        additionalParams.Add((nameof(Disabled), !(EditorEnabledIf?.Invoke(Model) ?? EditorEnabled)));

        additionalParams.Add((nameof(Required), RequiredIf?.Invoke(Model) ?? Required));

        additionalParams.Add(innerFragment);

        if (!Required && (!RequiredIf?.Invoke(Model) ?? false))
            Error = false;

        builder.RenderComponent(this, ignoreLabels,  additionalParams.ToArray());
        // (nameof(Disabled), !EditorEnabled)
     };

    public RenderFragment RenderAsGridComponent(object model) => (builder) =>
    {
        var selectedField = DataSource?.FirstOrDefault(x => x.GetPropertyValue(ValueField)?.ToString() == model.GetPropertyValue(BindingField)?.ToString());

        RenderExtensions.RenderGrid(builder, selectedField.GetPropertyValue(DisplayField));
    };

    void IGenComponent.ValidateObject()
    {
        ((IGenComponent)this).ParentGrid.ValidateValue(BindingField);
    }

    public object GetValue()
    {
        if (((IGenComponent)this).IsSearchField)
            return ((IGenComponent)this).GetSearchValue();
        else
            return this.GetFieldValue(nameof(_value));
    }

    

    void IGenComponent.SetSearchValue(object value)
    {
        Model.CastTo<Dictionary<string, object>>()[BindingField] = value.GetPropertyValue(BindingField);
        ((IGenComponent)this).ParentGrid.StateHasChanged();
    }

    object IGenComponent.GetSearchValue()
    {
        return Model.GetPropertyValue(BindingField);
    }

    void IGenComponent.SetEmpty()
    {
        var defaultValue = ((IGenComponent)this).DataType.GetDefaultValue();

        Model?.SetPropertyValue(BindingField, defaultValue);

        Value = null;

        _value = Model;
    }

    public new async Task Clear()
    {
        await base.Clear();

        ((IGenComponent)this).SetEmpty();
    }

    public new bool Validate()
    {
        if (((IGenComponent)this).IsSearchField)
            return ((IGenComponent)this).ParentGrid.ValidateSearchField(BindingField);

        return ((IGenComponent)this).ParentGrid.ValidateValue(BindingField);
    }

    bool IGenComponent.IsEditorVisible(object model)
    {
        return ((IGenComponent)this).EditorVisibleIf?.Invoke(model) ?? ((IGenComponent)this).EditorVisible;
    }

    bool IGenComponent.IsRequired(object model)
    {
        return ((IGenComponent)this).RequiredIf?.Invoke(model) ?? ((IGenComponent)this).Required;
    }

    void IGenComponent.ValidateRequiredRules()
    {
        if (Model is null) return;
        ((IGenComponent)this).ParentGrid.ValidateRequiredRules(this);
    }
}