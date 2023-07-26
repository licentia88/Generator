﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Generator.Components.Interfaces;
//using Generator.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Generator.Components.Components;

public partial class GenSpacer : ComponentBase, IGenSpacer
{
    public GenSpacer()
    {
        BindingField = Guid.NewGuid().ToString();
    }
    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public string BindingField { get; set; }

    [Parameter]
    [Range(1, 12, ErrorMessage = "Column width must be between 1 and 12")]
    public int Width { get; set; }
 

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public int Order { get; set; }

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public bool EditorEnabled { get; set; } = true;

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public bool EditorVisible { get; set; } = true;

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public bool GridVisible { get; set; } = true;

    [Parameter, EditorRequired]
    public int xs { get; set; }

    [Parameter, EditorRequired]
    public int sm { get; set; }

    [Parameter, EditorRequired]
    public int md { get; set; }

    [Parameter, EditorRequired]
    public int lg { get; set; }

    [Parameter, EditorRequired]
    public int xl { get; set; }

    [Parameter, EditorRequired]
    public int xxl { get; set; }

    [Parameter]
    public bool ClearIfNotVisible { get; set; } = false;

    [CascadingParameter(Name = nameof(ParentGrid))]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public INonGenGrid ParentGrid { get; set; }


    [EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }


    [EditorBrowsable(EditorBrowsableState.Never)]
    public string Label { get; set; }

    [EditorBrowsable(EditorBrowsableState.Never)]
    Type IGenComponent.DataType { get; set; }

    [EditorBrowsable(EditorBrowsableState.Never)]
    object IGenComponent.GetDefaultValue { get;  }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool Required { get; set; }

    public bool Error { get; set; }

    public string ErrorText { get; set; }

    public IGenComponent Reference { get; set; }

    public Action<object> ValueChangedAction { get; set; }

    [CascadingParameter(Name = nameof(IGenComponent.IsSearchField))]
    bool IGenComponent.IsSearchField { get; set; }
    public Func<object, bool> EditorVisibleIf { get; set; }
    public Func<object, bool> EditorEnabledIf { get; set; }
    public Func<object, bool> RequiredIf { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);

        AddComponents();
    }
    protected override Task OnInitializedAsync()
    {
        AddComponents();


        return Task.CompletedTask;
    }

    private void AddComponents()
    {
        if (((IGenComponent)this).IsSearchField)
            ParentGrid?.AddSearchFieldComponent(this);
        else
            ParentGrid?.AddChildComponent(this);
    }
    public void Initialize()
    {

    }
    //protected override void BuildRenderTree(RenderTreeBuilder builder)
    //{
    //    if (Model is not null || ParentGrid.IsRendered)
    //        base.BuildRenderTree(builder);
    //}


    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false) => (builder) =>
    {
        //RenderAsComponent(model, ignoreLabels,null).Invoke(builder);


    };

    public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false,
        params (string, object)[] valuePairs) => (builder) =>
        {
            //builder.RenderComponent(this, ignoreLabels);
        };

    public RenderFragment RenderAsGridComponent(object model) => (builder) =>
    {
        //RenderExtensions.RenderGrid(builder, "");
    };

    void IGenComponent.ValidateObject()
    {
    }

    public object GetValue()
    {
        throw new NotImplementedException();
    }

    void IGenComponent.SetSearchValue(object Value)
    {
        throw new NotImplementedException();
    }

    object IGenComponent.GetSearchValue()
    {
        throw new NotImplementedException();
    }

    void IGenComponent.SetValue(object value)
    {
        //throw new NotImplementedException();
    }

    public void SetEmpty()
    {
        //throw new NotImplementedException();
    }

    public new bool Validate()
    {
        return default;
    }

    public Task Clear()
    {
        throw new NotImplementedException();
    }

    bool IGenComponent.IsEditorVisible(object Model)
    {
        return true;
    }

    bool IGenComponent.IsRequired(object Model)
    {
        return false;
    }

    void IGenComponent.ValidateField()
    {
         
    }
}