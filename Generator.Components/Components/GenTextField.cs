﻿using Generator.Components.Extensions;
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
    [CascadingParameter(Name = nameof(IGenComponent.ParentGrid))]
    INonGenGrid IGenComponent.ParentGrid { get; set; }

 
    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }
    

 
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
        if (Model is not null && Model.GetType().Name != "Object")
            base.BuildRenderTree(builder);

        AddComponents();

        
    }

    protected override  Task OnInitializedAsync()
    {
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

        return Task.CompletedTask;
    }

    private void AddComponents()
    {
        if (((IGenComponent)this).IsSearchField)
            ((IGenComponent)this).ParentGrid?.AddSearchFieldComponent(this);
        else
            ((IGenComponent)this).ParentGrid?.AddChildComponent(this);
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

            comp.ParentGrid.StateHasChanged();
            comp.ParentGrid.CurrentGenPage?.StateHasChanged();
        }
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
    public EventCallback<object> OnValueChanged { get; set; }

    protected override async Task SetValueAsync(object value, bool updateText = true, bool force = false)
    {
        await base.SetValueAsync(value, updateText, force);
        await OnValueChanged.InvokeAsync(value);
    }

    private void SetCallBackEvents()
    {
        // if (!ValueChanged.HasDelegate)
            ValueChanged = EventCallback.Factory.Create<object>(this, SetValue);

        OnClearButtonClick = EventCallback.Factory.Create<MouseEventArgs>(this, OnClearClicked);

        OnBlur = EventCallback.Factory.Create<FocusEventArgs>(this, () => {
            Validate();
        });

    }



    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false, params (string Key, object Value)[] valuePairs) => builder =>
    {
        if (Model is null || Model.GetType().Name == "Object")
            Model = model;

        SetCallBackEvents();

        var loValue = Model.GetPropertyValue(BindingField);
        var additionalParams = valuePairs.Select(x => (x.Key, x.Value)).ToList();

        additionalParams.Add((nameof(Value), loValue));

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
        ((IGenComponent)this).ParentGrid.ValidateField(BindingField);
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
            return ((IGenComponent)this).ParentGrid.ValidateSearchField(BindingField);

        return ((IGenComponent)this).ParentGrid.ValidateField(BindingField);
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






