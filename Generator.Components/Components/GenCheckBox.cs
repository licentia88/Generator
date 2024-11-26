using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using Generator.Components.Interfaces;
using System.ComponentModel;
using Generator.Components.Args;
using Generator.Components.Enums;
using Microsoft.AspNetCore.Components.Rendering;
using Generator.Components.Extensions;

namespace Generator.Components.Components;

public class GenCheckBox : MudCheckBox<bool>, IGenCheckBox,IDisposable, IComponentMethods<GenCheckBox>
{
    #region CascadingParameters

    [CascadingParameter(Name = nameof(IGenComponent.Parent))]
    IPageBase IGenComponent.Parent { get; set; }

    #endregion CascadingParameters

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }


    [Parameter]
    public bool? InitialValue { get; set; }

    [Parameter]
    [EditorRequired()]
    public string BindingField { get; set; }

    [Parameter, EditorRequired]
    public string TrueText { get; set; }

    [Parameter, EditorRequired]
    public string FalseText { get; set; }

    Type IGenControl.DataType { get; set; } = typeof(bool);

    object IGenControl.GetDefaultValue => ((IGenControl)this).DataType.GetDefaultValue();

    [Parameter]
    public Func<object, bool> VisibleFunc { get; set; }

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

    //bool HasErrors { get; set; } 

    //public IGenComponent Reference { get; set; }

    [CascadingParameter(Name = nameof(IGenControl.IsSearchField))]
    bool IGenControl.IsSearchField { get; set; }

    [Parameter]
    public Func<object, bool> EditorVisibleIf { get; set; }

    [Parameter]
    public Func<object, bool> DisabledIf { get; set; }

    [Parameter]
    public Func<object, bool> RequiredIf { get; set; }



    //[Parameter]
    //public Func<object, bool> CheckedFunc { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        AddComponents();

        ErrorText = string.IsNullOrEmpty(ErrorText) ? "*" : ErrorText;

        if (string.IsNullOrEmpty(Class))
            Class = "mt-3";

        if (Model is null || Model.GetType().Name == "Object") return;

        if (InitialValue is not null && ((INonGenGrid)((IGenControl)this).Parent).ViewState != ViewState.Update)
            SetValue(InitialValue.Value);
    }
    protected override Task OnInitializedAsync()
    {

       

        return Task.CompletedTask;
    }

     private void AddComponents()
     {
         if (((IGenControl)this).IsSearchField)
            ((INonGenGrid)((IGenControl)this).Parent)?.AddSearchFieldComponent(this);
         else
            ((IGenControl)this).Parent?.AddChildComponent(this);

         
     }

    // public void Initialize()
    //{
    //    if (((IGenComponent)this).ParentGrid.EditMode != Enums.EditMode.Inline && ((IGenComponent)this).ParentGrid.CurrentGenPage is null) return;
 
    //}

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (((IGenControl)this).Parent is not null && Model is not null)
            base.BuildRenderTree(builder);
        //if (Model is not null && Model.GetType().Name != "Object")
        //    base.BuildRenderTree(builder);


        AddComponents();
    }

    
 
    protected new async Task SetCheckedAsync(bool value)
    {
        await base.SetCheckedAsync(value);
        var args = new ValueChangedArgs<bool>(Model, Value, value, ((IGenControl)this).IsSearchField);
        await OnCheckedChanged.InvokeAsync(args);
    }
    
    [Parameter]
    public EventCallback<ValueChangedArgs<bool>> OnCheckedChanged{ get; set; }

    private void SetCallBackEvents()
    {
        // if (!CheckedChanged.HasDelegate)
            ValueChanged = EventCallback.Factory.Create<bool>(this, x => { SetValue(x); Validate(); });
    }
    
    

    //public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => (builder) =>
    //{
    //    //EditorEnabled = EditorEnabledFunc?.Invoke(Model) ?? EditorEnabled;

    //    RenderAsComponent(model, ignoreLabels,new KeyValuePair<string, object>(nameof(Disabled),!EditorEnabled)).Invoke(builder);
    //};

    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false, params (string Key, object Value)[] valuePairs) => (builder) =>
    {
        //if (Model?.GetType().Name == "Object" || !((IGenComponent)this).IsSearchField)
        //    Model = model;

        if (!((IGenControl)this).IsSearchField)
            Model = model;

        if (((IGenControl)this).IsSearchField && Model is null)
        {
            Model = model;
        }


        SetCallBackEvents();

        var val = ((IGenControl)this).GetValue() ?? false;
        // var val = (Model.GetPropertyValue(BindingField)) ?? false;

        var additionalParams = valuePairs.Select(x => (x.Key, x.Value)).ToList();
        additionalParams.Add((nameof(Value), val));

        additionalParams.Add((nameof(Disabled), (DisabledIf?.Invoke(Model) ?? Disabled)));

        additionalParams.Add((nameof(Required), RequiredIf?.Invoke(Model) ?? Required));

        additionalParams.Add((nameof(Color), ((IGenControl)this).Parent.TemplateColor));

        additionalParams.Add((nameof(Label), Label is null or "" ? " " : Label));

        if (!Required && (!RequiredIf?.Invoke(Model) ?? false))
            Error = false;


        builder.RenderComponent(this, ignoreLabels,  additionalParams.ToArray());
        
    };

    public RenderFragment RenderAsGridComponent(object model) => (builder) =>
    {
        var val = model.GetPropertyValue(BindingField) as bool?;

        var gridValue = val == true ? TrueText : FalseText;

        RenderExtensions.RenderGrid(builder, gridValue);
    };

    void IGenControl.ValidateObject()
    {
        if (((IGenControl)this).Parent is INonGenGrid grid)
            grid.ValidateField(BindingField);

        //((IGenComponent)this).Parent.ValidateField(BindingField);
    }

    // public object GetValue()
    // {
    //     if (((IGenControl)this).IsSearchField)
    //         return ((IGenControl)this).GetSearchValue();
    //     else
    //         return Model.GetFieldValue(BindingField);
    //
    // }

    void IGenControl.SetValue(object value)
    {
        SetValue((bool)value);
    }

    public void SetValue(bool value)
    { 
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        // ReSharper disable once HeuristicUnreachableCode
        if (this is not IGenControl comp) return;


        if (comp.IsSearchField)
            comp.SetSearchValue(value);
        else
        {
            Model?.SetPropertyValue(BindingField, value);
            
            Value = value;

            _value = value;

        }

        comp.Parent.StateHasChanged();
        if (comp.Parent is INonGenGrid grid)
            grid.CurrentGenPage?.StateHasChanged();
    }

    object IGenControl.GetValue()
    {
        if (!TriState)
            return Model.GetPropertyValue(BindingField) ?? false;

        return Model.GetPropertyValue(BindingField) ?? false;
    }

    void IGenControl.SetSearchValue(object Value)
    {
        Model.CastTo<Dictionary<string, object>>()[BindingField] = Value;
        ((IGenControl)this).Parent.StateHasChanged();
    }

    void IGenControl.SetEmpty()
    {
        if (Model is null) return;

        var defaultValue = ((IGenControl)this).DataType.GetDefaultValue().CastTo<bool>();
        Model.SetPropertyValue(BindingField, defaultValue);
        _value = defaultValue;
        Value = defaultValue;
    }


    public Task ClearAsync()
    {
        ((IGenControl)this).SetEmpty();
        return Task.CompletedTask;
    }

    public new bool Validate()
    {
        if (((IGenControl)this).IsSearchField)
            return ((INonGenGrid)((IGenControl)this).Parent).ValidateSearchField(BindingField);
        else if (((IGenControl)this).Parent is INonGenGrid grid)
            return grid.ValidateField(BindingField);

        return true;
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
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Dispose managed resources

            Model = null;
            InitialValue = null;
            // ((IGenComponent)this).Parent = null;
            // BindingField = null;
            EditorVisibleIf = null;
            DisabledIf = null;
            RequiredIf = null;
            OnCheckedChanged = default;
            ValueChanged = default;
            ValueChanged = default;
        }

        base.Dispose(disposing);
    }
}






