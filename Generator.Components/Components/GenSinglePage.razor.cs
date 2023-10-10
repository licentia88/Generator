﻿using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using Generator.Components.Interfaces;
using Generator.Components.Enums;
using Generator.Components.Args;
using Force.DeepCloner;
using MudBlazor;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Generator.Components.Components;

public partial class GenSinglePage<TModel>:ComponentBase, ISinglePage where TModel:class
{
    [Parameter, AllowNull]
    public RenderFragment GenColumns { get; set; }

    [Parameter]
    public TModel Model { get; set; }

    public List<(Type type, IGenComponent component)> Components { get; set; } = new();

    [Parameter]
    public ViewState ViewState { get; set; }

    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; }

    [Parameter]
    public EventCallback<GenArgs<TModel>> Create { get; set; }

    [Parameter]
    public EventCallback<GenArgs<TModel>> Update { get; set; }

    [Parameter]
    public EventCallback<IEnumerable<string>> OnInvalidSubmit { get; set; }

    private TModel OriginalModel { get; set; }

    private EditContext editContext;

    protected override void OnInitialized()
    {
        editContext = new EditContext(Model); // Initialize the EditContext with your model

        OriginalModel = Model?.DeepClone();
        base.OnInitialized();
    }

    private bool _isFirstRender = true;

    //protected override void OnAfterRender(bool firstRender)
    //{
    //    base.OnAfterRender(firstRender);
    //}

    [Parameter]
    public string CancelText { get; set; } = "Cancel";

    [Parameter]
    public string CreateText { get; set; } = "Create";

    [Parameter]
    public string UpdateText { get; set; } = "Update";

    private string SubmitText => ViewState == ViewState.Create ? CreateText : UpdateText;

    void IPageBase.AddChildComponent(IGenComponent component)
    {
        var componentType = component.GetType();

        if (Components.Any(x => x.type == componentType && x.component.BindingField == component.BindingField)) return;

        Components.Add((componentType, component));
    }


    internal void InvalidSubmit()
    {
        if (OnInvalidSubmit.HasDelegate)
            OnInvalidSubmit.InvokeAsync(editContext.GetValidationMessages());
    }


    async Task OnCommit()
    {
        if (ViewState == ViewState.Create && Create.HasDelegate)
        {
            await Create.InvokeAsync(new GenArgs<TModel>(Model, null));
            Close();
        }

        if (ViewState == ViewState.Update && Update.HasDelegate)
            await Update.InvokeAsync(new GenArgs<TModel>(Model, OriginalModel));
    }

    public void Close() => MudDialog.Close();

    public new void StateHasChanged()
    {
        base.StateHasChanged();
    }
}

