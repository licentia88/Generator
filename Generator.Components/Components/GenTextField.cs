using DocumentFormat.OpenXml.EMMA;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Generator.Components.Components;

public class GenTextField : MudTextField<object>, IGenTextField, IComponentMethods<GenTextField>
{ 
    [CascadingParameter(Name = nameof(IGenComponent.Parent))]
    IPageBase IGenComponent.Parent { get; set; }

 
    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }

    [Parameter]
    public object InitialValue { get; set; }

    [Parameter]
    [EditorRequired]
    public string BindingField { get; set; }

    [Parameter]
    public int MinLength { get; set; }

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


    [Parameter]
    public Func<object,bool> EditorVisibleIf { get; set; }

    [Parameter]
    public Func<object, bool> EditorEnabledIf { get; set; }

   

    Type IGenComponent.DataType { get; set; }

    object IGenComponent.GetDefaultValue => ((IGenComponent)this).DataType.GetDefaultValue();

    [CascadingParameter( Name = nameof(IGenComponent.IsSearchField))]
    bool IGenComponent.IsSearchField { get; set; }

   
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        //if (Model is not null && Model.GetType().Name != "Object")
        //    base.BuildRenderTree(builder);

        if (((IGenComponent)this).Parent is not null && Model is not null)
        {
            base.BuildRenderTree(builder);
        }

        AddComponents();

        
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (InputType == InputType.Date)
        {
            Converter = _dateConverter;
            Culture = CultureInfo.GetCultureInfo("en-US");
            Format = "yyyy-MM-dd";
            ((IGenComponent)this).DataType = typeof(DateTime);
        }
        else
        {
            Converter = _stringConverter;
            ((IGenComponent)this).DataType = typeof(string);
        }

        //Immediate = true;

        AddComponents();

        ErrorText = string.IsNullOrEmpty(ErrorText) ? "*" : ErrorText;

        if (Model is null || Model.GetType().Name == "Object") return;

         if (InitialValue is not null && Value is null)
            SetValue(InitialValue);
    }
   

    private void AddComponents()
    {
        if (((IGenComponent)this).IsSearchField)
            ((INonGenGrid)((IGenComponent)this).Parent)?.AddSearchFieldComponent(this);
        else
            ((IGenComponent)this).Parent?.AddChildComponent(this);
    }

  
    private Converter<object> _stringConverter = new()
    {
        SetFunc = value => value?.ToString(),
        GetFunc = text => text?.ToString()
    };

    private Converter<object> _dateConverter = new()
    {
        SetFunc = value => Convert.ToDateTime(value).ToString("yyyy-MM-dd"),
        GetFunc = text =>
        {
            DateTime.TryParse(text, out var dataToReturn);
            return dataToReturn;
        }
    };

    [EditorBrowsable(EditorBrowsableState.Never)]
    public new string Text => Model.GetPropertyValue(BindingField).ToString();

    [Parameter]
    public Func<object, bool> RequiredIf { get; set; }

    [Parameter]
    public bool ClearIfNotVisible { get; set; } = false;

    public void SetValue(object value)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        // ReSharper disable once HeuristicUnreachableCode
        if (this is not IGenComponent comp) return;

        if (comp.IsSearchField)
            comp.SetSearchValue(value);
        else
        {
            Model?.SetPropertyValue(BindingField, value);

            Value = value;
            _value = value;

            //comp.Parent?.StateHasChanged();
            //comp.Parent?.CurrentGenPage?.StateHasChanged();
        }

        comp.Parent.StateHasChanged();
        if (comp.Parent is INonGenGrid grid)
            grid.CurrentGenPage?.StateHasChanged();
    }


    public void OnClearClicked(MouseEventArgs arg)
    {
        if (((IGenComponent)this).IsSearchField)
        {
            SetValue(null);
            Validate();
            return;
        }

        ((IGenComponent)this).SetEmpty();
        //Model?.SetPropertyValue(BindingField, null);
    }

    [Parameter]
    public EventCallback<(object Model, object Value)> OnValueChanged { get; set; }

    protected override async Task SetValueAsync(object value, bool updateText = true, bool force = false)
    {
        await base.SetValueAsync(value, updateText, force);
        await OnValueChanged.InvokeAsync((Model, value));
        Validate();
    }

    private void SetCallBackEvents()
    {
        // if (!ValueChanged.HasDelegate)
            ValueChanged = EventCallback.Factory.Create<object>(this, SetValue);

        OnClearButtonClick = EventCallback.Factory.Create<MouseEventArgs>(this, OnClearClicked);

        //OnBlur = EventCallback.Factory.Create<FocusEventArgs>(this, () =>
        //{
        //    Console.WriteLine();
        //    Validate();
        //});

    }



    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false, params (string Key, object Value)[] valuePairs) => builder =>
    {
        //if (Model is null || Model?.GetType().Name == "Object")
        //if (Model?.GetType().Name == "Object" || !((IGenComponent)this).IsSearchField)
        //    Model = model;

        if (!((IGenComponent)this).IsSearchField)
            Model = model;

        if (((IGenComponent)this).IsSearchField && Model is null)
        {
            Model = model;
        }

        SetCallBackEvents();

        if (((IGenComponent)this).IsSearchField)
        {
            Clearable = true;
        }

        var loValue = Model.GetPropertyValue(BindingField);
        var additionalParams = valuePairs.Select(x => (x.Key, x.Value)).ToList();

        //Bunu neden koyduk? simdilik acik kalsin, gozlemle
        additionalParams.Add((nameof(Value), loValue??Value));

        additionalParams.Add((nameof(Disabled), !(EditorEnabledIf?.Invoke(Model) ?? EditorEnabled) ));

        additionalParams.Add((nameof(Required), RequiredIf?.Invoke(Model) ?? Required));

        //TODO burada render yapmadan value yu kontrol et
        if (!Required && (!RequiredIf?.Invoke(Model) ?? false))
            Error = false;

        builder.RenderComponent(this, ignoreLabels, additionalParams.ToArray());
    };

    public RenderFragment RenderAsGridComponent(object model) => (builder) =>
    {
        var data = model.GetPropertyValue(BindingField);

        if (data is DateTime dt)
        {
            data = dt.ToString(Format);
        }

        RenderExtensions.RenderGrid(builder, data);
    };

    void IGenComponent.ValidateObject()
    {
        if (((IGenComponent)this).Parent is INonGenGrid grid)
            grid.ValidateField(BindingField);

        //((IGenComponent)this).Parent.ValidateField(BindingField);
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
        Model.CastTo<Dictionary<string, object>>()[BindingField] = value;
        ((IGenComponent)this).Parent?.StateHasChanged();
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
        _value = null;
    }

    

    public new async Task Clear()
    {
        await base.Clear();

        ((IGenComponent)this).SetEmpty();
    }

    public new bool Validate()
    {
        if (((IGenComponent)this).IsSearchField)
            return ((INonGenGrid)((IGenComponent)this).Parent).ValidateSearchField(BindingField);

        if (((IGenComponent)this).Parent is INonGenGrid grid)
            return grid.ValidateField(BindingField);

        return true;
    }


    void IGenComponent.ValidateField()
    {
        if (Model is null) return;

        if (((IGenComponent)this).IsEditorVisible(Model))
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

    bool IGenComponent.IsEditorVisible(object model)
    {
        return ((IGenComponent)this).EditorVisibleIf?.Invoke(model) ?? ((IGenComponent)this).EditorVisible;
    }

    bool IGenComponent.IsRequired(object model)
    {
         return ((IGenComponent)this).RequiredIf?.Invoke(model) ?? ((IGenComponent)this).Required;
    }

   
}






