﻿using Generator.Components.Extensions;
using Generator.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Generator.Components.Components;

public class GenAutoComplete : MudAutocomplete<object>, IGenAutoComplete
{
    [CascadingParameter(Name = nameof(IGenComponent.ParentGrid))]
    INonGenGrid IGenComponent.ParentGrid { get; set; }

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

    [CascadingParameter(Name = nameof(IGenComponent.IsSearchField))]
    bool IGenComponent.IsSearchField { get; set; }

    [Parameter]
    public Func<object, bool> EditorVisibleIf { get; set; }

    [Parameter]
    public Func<object, bool> EditorEnabledIf { get; set; }

    
    [Parameter]
    public Func<object, bool> RequiredIf { get; set; }

    protected override Task OnInitializedAsync()
    {
        AddComponents();
        ErrorText = string.IsNullOrEmpty(ErrorText) ? "*" : ErrorText;
        return Task.CompletedTask;
         
    }

    private async Task<IEnumerable<object>> FindMethod(string value)
    {
        // In real life use an asynchronous function for fetching data from an api.
        await Task.Delay(5);

        // if text is null or empty, show complete list
        if (string.IsNullOrEmpty(value))
            return DataSource;

        var  filteredData= DataSource.Where(x => x.GetType().GetProperty(DisplayField).GetValue(x).ToString().StartsWith(value, StringComparison.InvariantCultureIgnoreCase));

        return filteredData;
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
        if (((IGenComponent)this).IsSearchField)
        {
            SetValue(null);
            Validate();
            return;
        }

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
            //comp.SetSearchValue(value);
            Model?.SetPropertyValue(BindingField, value.GetPropertyValue(ValueField));
            comp.ParentGrid.ResetConditionalSearchFields();

        }
        else
        {
            Model?.SetPropertyValue(BindingField, value.GetPropertyValue(ValueField));

            //SelectOption(value);
        }

        comp.ParentGrid.StateHasChanged();
        comp.ParentGrid.CurrentGenPage?.StateHasChanged();
    }


    protected override async Task SetValueAsync(object value, bool updateText = true, bool force = false)
    {
        await base.SetValueAsync(value, updateText, force);
        await OnValueChanged.InvokeAsync((Model,value));
    }

    [Parameter]
    public EventCallback<(object Model, object Value)> OnValueChanged { get; set; }

    private void SetCallBackEvents()
    {
        ToStringFunc = x => x?.GetPropertyValue(DisplayField)?.ToString();

        // if (!ValueChanged.HasDelegate)
        ValueChanged = EventCallback.Factory.Create<object>(this, SetValue);

        if (SearchFunc is null)
            SearchFunc = FindMethod;

        if (!OnClearButtonClick.HasDelegate)
            OnClearButtonClick = EventCallback.Factory.Create<MouseEventArgs>(this, OnClearClicked);


        OnBlur = EventCallback.Factory.Create<FocusEventArgs>(this, () =>
        {
            Validate();
        });

        OnKeyDown = EventCallback.Factory.Create<KeyboardEventArgs>(this, () =>
        {
            Validate();
        });


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

        var loValue = Model.GetPropertyValue(BindingField);
        var additionalParams = valuePairs.Select(x => (x.Key, x.Value)).ToList();

        additionalParams.Add((nameof(Value), loValue));

        additionalParams.Add((nameof(Disabled), !(EditorEnabledIf?.Invoke(Model) ?? EditorEnabled)));

        additionalParams.Add((nameof(Required), RequiredIf?.Invoke(Model) ?? Required));

        //TODO burada render yapmadan value yu kontrol et
        if (!Required && (!RequiredIf?.Invoke(Model) ?? false))
            Error = false;

        builder.RenderComponent(this, ignoreLabels, additionalParams.ToArray());
    };

    public RenderFragment RenderAsGridComponent(object model) => (builder) =>
    {
        var selectedField = DataSource?.FirstOrDefault(x => x.GetPropertyValue(ValueField)?.ToString() == model.GetPropertyValue(BindingField)?.ToString());

        RenderExtensions.RenderGrid(builder, selectedField.GetPropertyValue(DisplayField));
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

        //Value = null;

        //_value = Model;
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

    bool IGenComponent.IsEditorVisible(object model)
    {
        return ((IGenComponent)this).EditorVisibleIf?.Invoke(model) ?? ((IGenComponent)this).EditorVisible;
    }

    bool IGenComponent.IsRequired(object model)
    {
        return ((IGenComponent)this).RequiredIf?.Invoke(model) ?? ((IGenComponent)this).Required;
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
}