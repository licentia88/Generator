using System;
using Generator.Components.Args;
using Generator.Components.Enums;
using Generator.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.Components.Components
{
	public partial class GenPage: IGenPage
    {
        [Parameter]
        public IGenGrid GenGrid { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public ViewState ViewState { get; set; }

        [Parameter]
        public EditMode EditMode { get; set; }

        public object OriginalEditItem { get; set; }

        [Parameter]
        public EventCallback<GenGridArgs> Create { get; set; }

        [Parameter]
        public EventCallback<GenGridArgs> Update { get; set; }

        [Parameter]
        public EventCallback<GenGridArgs> Delete { get; set; }

        [Parameter]
        public EventCallback<GenGridArgs> Cancel { get; set; }

        [Parameter]
        public object ViewModel { get; set; }

        public MudDialogInstance MudDialog { get; set; }
        public bool EnableModelValidation { get; set; }
        public List<IGenComponent> Components { get; set; }

        public TComponent GetComponent<TComponent>(string BindingField) where TComponent : IGenComponent
        {
            throw new NotImplementedException();
        }

        public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false)
        {
            throw new NotImplementedException();
        }

        public RenderFragment RenderAsGridComponent(object model)
        {
            throw new NotImplementedException();
        }


        public Task OnCommit()
        {
            return Task.CompletedTask;
        }

        public virtual void Close()
        {
            MudDialog.Cancel();
        }

        private string GetSubmitTextFromViewState()
        {
            if (ViewState == ViewState.Create)
                return GenGrid.CreateText;
            if (ViewState == ViewState.Update)
                return GenGrid.UpdateText;
            if (ViewState == ViewState.Delete)
                return GenGrid.DeleteText;

            return "";
        }
    }
}

