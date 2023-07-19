﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
//using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;

namespace Generator.Components.Components;

public class GenDatePicker : MudDatePicker, IGenDatePicker, IComponentMethods<GenDatePicker>
{
    [CascadingParameter(Name = nameof(IGenComponent.ParentGrid))]
    INonGenGrid  IGenComponent.ParentGrid { get; set; }

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }

     

    [Parameter]
    [EditorRequired]
    public string BindingField { get; set; }

    Type IGenComponent.DataType { get; set; } = typeof(DateTime);

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


    [CascadingParameter(Name = nameof(IGenComponent.IsSearchField))]
    bool IGenComponent.IsSearchField { get; set; }

    [Parameter]
    public Func<object, bool> EditorVisibleFunc { get; set; }

    [Parameter]
    public Func<object, bool> EditorEnabledFunc { get; set; }

    [Parameter]
    public Func<object, bool> RequiredIf { get; set; }

    protected override Task OnInitializedAsync()
    {
        Date = (DateTime?)Model?.GetPropertyValue(BindingField);

        AddComponents();

        return Task.CompletedTask;

     }

  

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Model is not null && Model.GetType().Name != "Object")
            base.BuildRenderTree(builder);

        AddComponents();
    }

    public void SetValue(DateTime? date)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        // ReSharper disable once HeuristicUnreachableCode
        if (this is not IGenComponent comp) return;

        if (comp.IsSearchField)
            comp.SetSearchValue(date);
        else
        {
            Model?.SetPropertyValue(BindingField, date);
            _value = date;

        }

        comp.ParentGrid.StateHasChanged();
        comp.ParentGrid.CurrentGenPage?.StateHasChanged();
    }

 

    protected override void OnClosed()
    {
        if (!Error)
        {
            if (!((IGenComponent)this).IsSearchField)
                ((IGenComponent)this).ParentGrid.ValidateValue(BindingField);
            else
                ((IGenComponent)this).ParentGrid.ValidateSearchField(BindingField);
        }
            

        base.OnClosed();
    }

    private void AddComponents()
    {
        if (((IGenComponent)this).IsSearchField)
            ((IGenComponent)this).ParentGrid?.AddSearchFieldComponent(this);
        else
            ((IGenComponent)this).ParentGrid?.AddChildComponent(this);

       
    }

    private void SetCallBackEvents()
    {
        if (((IGenComponent)this).IsSearchField)
        {
            DateChanged = EventCallback.Factory.Create<DateTime?>(this, x =>
            {
                SetValue(x.CastTo<DateTime?>());
                Validate();
            });
        }
        else
        {
            if (!DateChanged.HasDelegate)
            {
                DateChanged = EventCallback.Factory.Create<DateTime?>(this, x => SetValue(x.CastTo<DateTime?>()));
            }
        }
    }

    //public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => (builder) =>
    //{
    //    RenderAsComponent(model, ignoreLabels, new KeyValuePair<string, object>(nameof(Disabled), !(EditorEnabledFunc?.Invoke(Model) ?? EditorEnabled))).Invoke(builder);
    // };

    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false, params (string Key, object Value)[] valuePairs) => (builder) =>
    {
        if (Model is null || Model.GetType().Name == "Object")
            Model = model;

        SetCallBackEvents();

        var valDate = (DateTime?)Model?.GetPropertyValue(BindingField);

        var additionalParams = valuePairs.Select(x => (x.Key, x.Value)).ToList();

        additionalParams.Add((nameof(_value), valDate));

        additionalParams.Add((nameof(Disabled), !(EditorEnabledFunc?.Invoke(Model) ?? EditorEnabled)));

        additionalParams.Add((nameof(Required), RequiredIf?.Invoke(Model) ?? Required));

        if (!RequiredIf?.Invoke(Model) ?? Required)
            Error = false;

        builder.RenderComponent(this, ignoreLabels,  additionalParams.ToArray());
        //throw new NotImplementedException();
    };

    public RenderFragment RenderAsGridComponent(object model) => (builder) =>
    {
        var val = (DateTime?)model.GetPropertyValue(BindingField);
             
        if (val is not null)
            RenderExtensions.RenderGrid(builder, val.Value.ToString(DateFormat));

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

    void IGenComponent.SetSearchValue(object Value)
    {
        Model.CastTo<Dictionary<string, object>>()[BindingField] = Value;
        ((IGenComponent)this).ParentGrid.StateHasChanged();

    }

    object IGenComponent.GetSearchValue()
    {
        return Model.GetPropertyValue(BindingField);
    }

    void IGenComponent.SetValue(object value)
    {
        SetValue((DateTime?)value);
    }

    void IGenComponent.SetEmpty()
    {
        var defaultValue = ((IGenComponent)this).DataType.GetDefaultValue().CastTo<DateTime?>();

        Model?.SetPropertyValue(BindingField, defaultValue);

        _value = defaultValue;
    }

    public Task Clear()
    {
        base.Clear();

        ((IGenComponent)this).SetEmpty();
        
        return Task.CompletedTask;
    }

    public new bool Validate()
    {
        if (((IGenComponent)this).IsSearchField)
            return ((IGenComponent)this).ParentGrid.ValidateSearchField(BindingField);

        return ((IGenComponent)this).ParentGrid.ValidateValue(BindingField);
    }

    bool IGenComponent.IsEditorVisible(object Model)
    {
        return ((IGenComponent)this).EditorVisibleFunc?.Invoke(Model) ?? ((IGenComponent)this).EditorVisible;
    }

    bool IGenComponent.IsRequired(object Model)
    {
        return ((IGenComponent)this).RequiredIf?.Invoke(Model) ?? ((IGenComponent)this).Required;
    }
}