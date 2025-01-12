﻿using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Generator.Components.Args;
using Generator.Components.Enums;

namespace Generator.Components.Components;

public class GenTextField : MudTextField<object>, IGenTextField, IComponentMethods<GenTextField>, IDisposable
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
    public Func<object, bool> EditorVisibleIf { get; set; }

    [Parameter]
    public Func<object, bool> DisabledIf { get; set; }

    Type IGenControl.DataType { get; set; }

    object IGenControl.GetDefaultValue => ((IGenControl)this).DataType.GetDefaultValue();

    [CascadingParameter(Name = nameof(IGenControl.IsSearchField))]
    bool IGenControl.IsSearchField { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (((IGenControl)this).Parent is not null && Model is not null)
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
            ((IGenControl)this).DataType = typeof(DateTime);
        }
        else
        {
            Converter = _stringConverter;
            ((IGenControl)this).DataType = typeof(string);
        }

        AddComponents();

        ErrorText = string.IsNullOrEmpty(ErrorText) ? "*" : ErrorText;

        if (Model is null || Model.GetType().Name == "Object") return;

        if (InitialValue is not null && ((INonGenGrid)((IGenControl)this).Parent).ViewState != ViewState.Update)
            SetValue(InitialValue);
    }

    private void AddComponents()
    {
        if (((IGenControl)this).IsSearchField)
            ((INonGenGrid)((IGenControl)this).Parent)?.AddSearchFieldComponent(this);
        else
            ((IGenControl)this).Parent?.AddChildComponent(this);
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

    [Parameter]
    public Func<object, bool> RequiredIf { get; set; }

    [Parameter]
    public bool ClearIfNotVisible { get; set; }

    public void SetValue(object value)
    {
        if (this is not IGenControl comp) return;

        if (value is string str && string.IsNullOrEmpty(str))
        {
            value = null;
        }

        if (comp.IsSearchField)
        {
            comp.SetSearchValue(value);
        }
        else
        {
            Model?.SetPropertyValue(BindingField, value);
        }

        comp.Parent.StateHasChanged();

        if (comp.Parent is INonGenGrid grid)
        {
            grid.CurrentGenPage?.StateHasChanged();
        }
    }

    public void OnClearClicked(MouseEventArgs arg)
    {
        if (((IGenControl)this).IsSearchField)
        {
            SetValue(null);
            Validate();
            return;
        }

        ((IGenControl)this).SetEmpty();
    }

    [Parameter]
    public EventCallback<ValueChangedArgs<object>> OnValueChanged { get; set; }

    protected override async Task SetValueAsync(object value, bool updateText = true, bool force = false)
    {
        await base.SetValueAsync(value, updateText, force);
        await OnValueChanged.InvokeAsync(new ValueChangedArgs<object>(Model, Value, value, ((IGenControl)this).IsSearchField));
    }

    private void SetCallBackEvents()
    {
        ValueChanged = EventCallback.Factory.Create<object>(this, SetValue);
        OnClearButtonClick = EventCallback.Factory.Create<MouseEventArgs>(this, OnClearClicked);
    }

    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false, params (string Key, object Value)[] valuePairs) => builder =>
    {
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

        var loValue = ((IGenControl)this).GetValue()?.ToString();
        
        // if(string.IsNullOrEmpty(loValue) && !string.IsNullOrEmpty(InitialValue?.ToString()))
        //     loValue = InitialValue?.ToString();
        
        var additionalParams = valuePairs.Select(x => (x.Key, x.Value)).ToList();

        additionalParams.Add((nameof(Value), loValue));
        additionalParams.Add((nameof(_value), loValue));
        additionalParams.Add((nameof(Text), loValue));
        additionalParams.Add((nameof(Disabled), DisabledIf?.Invoke(Model) ?? Disabled));
        additionalParams.Add((nameof(Required), RequiredIf?.Invoke(Model) ?? Required));
        additionalParams.Add((nameof(Label), Label is null or "" ? " " : Label));

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

        if (InputType == InputType.Password)
            data = new string((char)0x25CF, data.ToString().Length);

        RenderExtensions.RenderGrid(builder, data);
    };

    void IGenControl.ValidateObject()
    {
        if (((IGenControl)this).Parent is INonGenGrid grid)
            grid.ValidateField(BindingField);
    }

    void IGenControl.SetSearchValue(object value)
    {
        Model.CastTo<Dictionary<string, object>>()[BindingField] = value;
        ((IGenControl)this).Parent?.StateHasChanged();
    }

    object IGenControl.GetValue()
    {
        return Model.GetPropertyValue(BindingField);
    }

    void IGenControl.SetEmpty()
    {
        if (Model is null) return;

        var defaultValue = ((IGenControl)this).DataType.GetDefaultValue();

        Model.SetPropertyValue(BindingField, defaultValue);
        Value = null;
        _value = null;
    }

    public new async Task ClearAsync()
    {
        await base.Clear();
        ((IGenControl)this).SetEmpty();
    }

    public new bool Validate()
    {
        if (((IGenControl)this).IsSearchField)
            return ((INonGenGrid)((IGenControl)this).Parent).ValidateSearchField(BindingField);

        else if (((IGenControl)this).Parent is INonGenGrid grid)
            return grid.ValidateField(BindingField);

        return true;
    }

    void IGenControl.ValidateField()
    {
        if (Model is null) return;

        if (!((IGenComponent)this).IsEditorVisible(Model)) return;

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

    bool IGenComponent.IsEditorVisible(object model)
    {
        return ((IGenControl)this).EditorVisibleIf?.Invoke(model) ?? ((IGenControl)this).EditorVisible;
    }

    bool IGenControl.IsRequired(object model)
    {
        return ((IGenControl)this).RequiredIf?.Invoke(model) ?? ((IGenControl)this).Required;
    }
    
    protected override void Dispose(bool disposing)
    {
        // if (disposing)
        // {
        //     // Dispose managed resources
        //     _stringConverter = null;
        //     _dateConverter = null;
        //     Model = null;
        //     // InitialValue = null;
        //     // ((IGenComponent)this).Parent = null;
        //     // BindingField = null;
        //     // EditorVisible = false;
        //     // EditorVisibleIf = null;
        //     // DisabledIf = null;
        //     // RequiredIf = null;
        //     OnValueChanged = default;
        //     ValueChanged = default;
        //     TextChanged = default;
        //     OnClearButtonClick = default;
        //     OnAdornmentClick = default;
        //     OnBlur = default;
        //     OnKeyUp = default;
        //     OnKeyDown = default;
        //     OnDebounceIntervalElapsed = default;
        //     OnInternalInputChanged = default;
        //     OnlyValidateIfDirty = default;
        //     OnInternalInputChanged = default;
        // }

        base.Dispose(disposing);
        GC.SuppressFinalize(this);
    }
} 