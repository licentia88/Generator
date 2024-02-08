using Generator.Components.Args;
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
    [CascadingParameter(Name = nameof(IGenComponent.Parent))]
    IPageBase IGenComponent.Parent { get; set; }

    [Parameter, EditorRequired]
    public string DisplayField { get; set; }

    [Parameter, EditorRequired]
    public string ValueField { get; set; }

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }

    [Parameter]
    public object InitialValue { get; set; }

    [Parameter]
    [EditorRequired]
    public string BindingField { get; set; }

    Type IGenControl.DataType { get; set; } = typeof(object);

    object IGenControl.GetDefaultValue => ((IGenControl)this).DataType.GetDefaultValue();

    [Parameter]
    [Range(1, 12, ErrorMessage = "Column width must be between 1 and 12")]
    public int Width { get; set; }

    [Parameter]
    public int Order { get; set; }

    //[Parameter]
    //public bool EditorEnabled { get; set; } = true;

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

    [CascadingParameter(Name = nameof(IGenControl.IsSearchField))]
    bool IGenControl.IsSearchField { get; set; }

    [Parameter]
    public Func<object, bool> EditorVisibleIf { get; set; }

    [Parameter]
    public Func<object, bool> DisabledIf { get; set; }

    //[Parameter]
    //public Func<(object Model, object Value), bool> Where { get; set; }

    [Parameter]
    public Func<ComponentArgs<object>, bool> Where { get; set; }


    [Parameter]
    public Func<object, bool> RequiredIf { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        AddComponents();
        ErrorText = string.IsNullOrEmpty(ErrorText) ? "*" : ErrorText;

        if (Model is null || Model.GetType().Name == "Object") return;

        if (InitialValue is not null && Value is null)
            SetValue(InitialValue);
    }
 

    private void AddComponents()
    {
        if (((IGenControl)this).IsSearchField)
            ((INonGenGrid)((IGenControl)this).Parent)?.AddSearchFieldComponent(this);
        else
            ((IGenControl)this).Parent?.AddChildComponent(this);

    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (((IGenControl)this).Parent is not null && Model is not null)
            base.BuildRenderTree(builder);

        //if (Model is not null && Model.GetType().Name != "Object")
        //    base.BuildRenderTree(builder);

        AddComponents();
    }


    public async Task OnClearClickedAsync(MouseEventArgs arg)
    {

        if (((IGenControl)this).IsSearchField)
        {
            SetValue(null);

            await OnValueChanged.InvokeAsync(new ComponentArgs<object>(Model, default, ((IGenControl)this).IsSearchField));

            Validate();
            return;
        }

        ((IGenControl)this).SetEmpty();

        ////Model.SetPropertyValue(BindingField, null);

        ////((IGenComponent)this).SetEmpty();

        //((IGenComponent)this).Parent.StateHasChanged();
        //((IGenComponent)this).Parent?.CurrentGenPage?.StateHasChanged();
    }

    public void SetValue(object value)
    {
        //if (value is null) return;

       
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        // ReSharper disable once HeuristicUnreachableCode
        if (this is not IGenControl comp) return;

        if (comp.IsSearchField)
        {
            //comp.SetSearchValue(value);
            Model?.SetPropertyValue(BindingField, value.GetPropertyValue(ValueField));
            ((INonGenGrid)comp.Parent).ResetConditionalSearchFields();

        }
        else
        {
            Model?.SetPropertyValue(BindingField, value.GetPropertyValue(ValueField));

            //SelectOption(value);
        }


        comp.Parent.StateHasChanged();
        if (comp.Parent is INonGenGrid grid)
            grid.CurrentGenPage?.StateHasChanged();
    }


    protected override async Task SetValueAsync(object value, bool updateText = true, bool force = false)
    {
        if (value is null) return;
        await base.SetValueAsync(value, updateText, force);
        await OnValueChanged.InvokeAsync(new ComponentArgs<object>(Model, value, ((IGenControl)this).IsSearchField));

        Validate();
    }

    [Parameter]
    public EventCallback<ComponentArgs<object>> OnValueChanged { get; set; }

    private void SetCallBackEvents()
    {
        ToStringFunc = x => x?.GetPropertyValue(DisplayField)?.ToString();

        // if (!ValueChanged.HasDelegate)
        ValueChanged = EventCallback.Factory.Create<object>(this, SetValue);

        if (!OnClearButtonClick.HasDelegate)
            OnClearButtonClick = EventCallback.Factory.Create<MouseEventArgs>(this, OnClearClickedAsync);


        //OnBlur = EventCallback.Factory.Create<FocusEventArgs>(this, () =>
        //{
        //    Validate();
        //});

        //OnKeyDown = EventCallback.Factory.Create<KeyboardEventArgs>(this, () =>
        //{
        //    Validate();
        //});


    }

    //public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => builder =>
    //{
    //    RenderAsComponent(model, ignoreLabels, new KeyValuePair<string, object>(nameof(Disabled), !(EditorEnabledFunc?.Invoke(Model) ?? EditorEnabled))).Invoke(builder);

    //};

    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false, params (string Key, object Value)[] valuePairs) => builder =>
    {
        //if (Model is null || Model.GetType().Name == "Object" && !((IGenComponent)this).IsSearchField))
        //if (Model?.GetType().Name == "Object" || !((IGenComponent)this).IsSearchField)
        //    Model = model;

        if (!((IGenControl)this).IsSearchField)
            Model = model;
        
        if (((IGenControl)this).IsSearchField && Model is null)
        {
            Model = model;
        }
       
        


        SetCallBackEvents();

        if (((IGenControl)this).IsSearchField)
        {
            Clearable = true;
        }
        var innerFragment = (nameof(ChildContent), (RenderFragment)(treeBuilder =>
        {
            var i = 1000;

            if (DataSource is null) return;

            foreach (var data in DataSource.Where(x => Where?.Invoke(new ComponentArgs<object>(Model,x,((IGenControl)this).IsSearchField)) ?? true))
            {
                treeBuilder.OpenComponent(i++, typeof(MudSelectItem<object>));

                treeBuilder.AddAttribute(i++, nameof(Value), data);

                treeBuilder.CloseComponent();
            }

        }));


        var loValue = DataSource?.FirstOrDefault(x => x.GetPropertyValue(ValueField)?.ToString() == Model.GetPropertyValue(BindingField)?.ToString());

        var additionalParams = valuePairs.Select(x => (x.Key, x.Value)).ToList();

        additionalParams.Add((nameof(Value), loValue??Value));

        additionalParams.Add((nameof(Disabled), (DisabledIf?.Invoke(Model) ?? Disabled)));

        additionalParams.Add((nameof(Required), RequiredIf?.Invoke(Model) ?? Required));

        additionalParams.Add(innerFragment);

        if (!Required && (!RequiredIf?.Invoke(Model) ?? false))
            Error = false;

        builder.RenderComponent(this, ignoreLabels, additionalParams.ToArray());
        // (nameof(Disabled), !EditorEnabled)
    };

    public RenderFragment RenderAsGridComponent(object model) => (builder) =>
    {
        var selectedField = DataSource?.FirstOrDefault(x => x.GetPropertyValue(ValueField)?.ToString() == model.GetPropertyValue(BindingField)?.ToString());

        RenderExtensions.RenderGrid(builder, selectedField.GetPropertyValue(DisplayField));
    };

    void IGenControl.ValidateObject()
    {
        if(((IGenControl)this).Parent is INonGenGrid grid)
            grid.ValidateField(BindingField);

        //((IGenComponent)this).Parent.ValidateField(BindingField);
    }

    public object GetValue()
    {
        if (((IGenControl)this).IsSearchField)
            return ((IGenControl)this).GetSearchValue();
        else
            return this.GetFieldValue(nameof(_value));
    }



    void IGenControl.SetSearchValue(object value)
    {
        Model.CastTo<Dictionary<string, object>>()[BindingField] = value.GetPropertyValue(BindingField);
        ((IGenControl)this).Parent.StateHasChanged();
    }

    object IGenControl.GetSearchValue()
    {
        return Model.GetPropertyValue(BindingField);
    }

    void IGenControl.SetEmpty()
    {
        var defaultValue = ((IGenControl)this).DataType.GetDefaultValue();

        Model?.SetPropertyValue(BindingField, defaultValue);

        //Value = null;

        //_value = Model;
    }

    public new async Task Clear()
    {
        await base.Clear();

        ((IGenControl)this).SetEmpty();
    }

    public new bool Validate()
    {
        if (((IGenControl)this).IsSearchField)
            return ((INonGenGrid)((IGenControl)this).Parent).ValidateSearchField(BindingField);

        if(((IGenControl)this).Parent is INonGenGrid grid)
            return grid.ValidateField(BindingField);

        return true;
        //return ((IGenComponent)this).Parent.ValidateField(BindingField);
    }

    bool IGenComponent.IsEditorVisible(object model)
    {
        return ((IGenControl)this).EditorVisibleIf?.Invoke(model) ?? ((IGenControl)this).EditorVisible;
    }

    bool IGenControl.IsRequired(object model)
    {
        return ((IGenControl)this).RequiredIf?.Invoke(model) ?? ((IGenControl)this).Required;
    }

    void IGenControl.ValidateField()
    {
        if (Model is null) return;

        if (((IGenControl)this).IsEditorVisible(Model))
        {
            var loValue = Model.GetPropertyValue(BindingField);

            if (RequiredIf is not null)
            {
                Error = RequiredIf.Invoke(Model);
            }
            else if (Required)
            {
                Error = loValue is null || loValue.ToString() == string.Empty;
            }
        }
    }
}