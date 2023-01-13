using Generator.Components.Args;
using Generator.Components.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.Components.Interfaces;

public interface IGenGrid<TModel> : IGenView<TModel> where TModel:new()  
{
    public bool HasErrors();// { get; }

    public GenPage<TModel> CurrentGenPage { get; set; }

    public IDialogService DialogService { get; set; }

    public DialogResult DialogResult { get; set; }

    public DialogParameters DialogParameters { get; set; }

    public DialogOptions DialogOptions { get; set; }

    public string CancelText { get; set; }

    public string CreateText { get; set; }

    public string UpdateText { get; set; }

    public string DeleteText { get; set; }

    public bool EnableSearch { get; set; }

    public bool IsFirstRender { get; set; }

    public ICollection<TModel> DataSource { get; set; }

    public bool NewDisabled { get; set; }

    public bool ExpandDisabled { get; set; }

    public bool SearchDisabled { get; set; }

    public RenderFragment GenColumns { get; set; }

    public RenderFragment GenHeaderButtons { get; set; }

    public RenderFragment<TModel> GenDetailGrid { get; set; }

    public bool HasDetail { get; }

    public bool DetailClicked { get; set; }

    public object ParentComponent { get; set; }

    public string SearchPlaceHolderText { get; set; }

    public EventCallback<GenGridArgs<TModel>> Create { get; set; }

    public EventCallback<GenGridArgs<TModel>> Update { get; set; }

    public EventCallback<GenGridArgs<TModel>> Delete { get; set; }

    public EventCallback<GenGridArgs<TModel>> Cancel { get; set; }
}
