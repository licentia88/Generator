﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using Generator.Components.Interfaces;
//using Generator.Shared.Extensions;
using System.ComponentModel;
using Microsoft.AspNetCore.Components.Rendering;
using Generator.Components.Extensions;

namespace Generator.Components.Components;

public class GenCheckBox : MudCheckBox<bool>, IGenCheckBox, IComponentMethods<GenCheckBox>
{
    #region CascadingParameters

    [CascadingParameter(Name = nameof(IGenComponent.ParentGrid))]
    INonGenGrid IGenComponent.ParentGrid { get; set; }

    #endregion CascadingParameters

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }

 

    [Parameter]
    [EditorRequired()]
    public string BindingField { get; set; }

    [Parameter, EditorRequired]
    public string TrueText { get; set; }

    [Parameter, EditorRequired]
    public string FalseText { get; set; }

    public Type DataType { get; set; } = typeof(bool);

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

    //bool HasErrors { get; set; } 

    //public IGenComponent Reference { get; set; }

    [CascadingParameter(Name = nameof(IsSearchField))]
    public bool IsSearchField { get; set; }

     protected override Task OnInitializedAsync()
    {
        AddComponents();

        if (string.IsNullOrEmpty(Class))
            Class = "mt-3";

        return Task.CompletedTask;
    }

     private void AddComponents()
     {
         if (IsSearchField)
            ((IGenComponent)this).ParentGrid?.AddSearchFieldComponent(this);
         else
            ((IGenComponent)this).ParentGrid?.AddChildComponent(this);
     }

     public void Initialize()
    {
        if (((IGenComponent)this).ParentGrid.EditMode != Enums.EditMode.Inline && ((IGenComponent)this).ParentGrid.CurrentGenPage is null) return;
 
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Model is not null && Model.GetType().Name != "Object")
            base.BuildRenderTree(builder);

        AddComponents();
    }

    void IGenComponent.SetValue(object value)
    {
        SetValue((bool)value);
    }

    public void SetValue(bool value)
    {
        Model?.SetPropertyValue(BindingField, value);

        Checked = value;
 
        _value = value;

        ((IGenComponent)this).ParentGrid.StateHasChanged();
        ((IGenComponent)this).ParentGrid.CurrentGenPage?.StateHasChanged();

    }

    private void SetCallBackEvents()
    {
        if (!CheckedChanged.HasDelegate)
            CheckedChanged = EventCallback.Factory.Create<bool>(this, SetValue);
    }

    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => (builder) =>
    {
        RenderAsComponent(model, ignoreLabels,new KeyValuePair<string, object>(nameof(Disabled),!EditorEnabled)).Invoke(builder);
    };

    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false,
        params KeyValuePair<string, object>[] valuePairs) => (builder) =>
    {
        if (Model is null || Model.GetType().Name == "Object")
            Model = model;
 
        SetCallBackEvents();

        var val = (Model.GetPropertyValue(BindingField)) ?? false;

        var additionalParams = valuePairs.Select(x => (x.Key, x.Value)).ToList();
        additionalParams.Add((nameof(Checked), val));
        
        builder.RenderComponent(this, ignoreLabels,  additionalParams.ToArray());
        
    };

    public RenderFragment RenderAsGridComponent(object model) => (builder) =>
    {
        var val = model.GetPropertyValue(BindingField) as bool?;

        var gridValue = val == true ? TrueText : FalseText;

        RenderExtensions.RenderGrid(builder, gridValue);
    };

    public void ValidateObject()
    {
        ((IGenComponent)this).ParentGrid.ValidateValue(BindingField);
    }

    public object GetValue()
    {
        //return Model.GetPropertyValue(BindingField);
        return this.GetFieldValue(nameof(_value));
    }

    public object GetSearchValue()
    {
        if (!TriState)
            return Model.GetPropertyValue(BindingField) ?? false;

        return Model.GetPropertyValue(BindingField) ?? false;
    }

    public void SetSearchValue(object Value)
    {
        Model.CastTo<Dictionary<string, object>>()[BindingField] = Value;
        ((IGenComponent)this).ParentGrid.StateHasChanged();
    }

    public void SetEmpty()
    {
        Model?.SetPropertyValue(BindingField, default);
        _value = false;
    }
    //public GenCheckBox GetReference()
    //{
    //    return (GenCheckBox)Reference;
    //}
}






