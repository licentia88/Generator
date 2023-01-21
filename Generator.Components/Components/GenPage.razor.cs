using System.Data;
using Generator.Components.Args;
using Generator.Components.Enums;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Mapster;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.Components.Components
{
    public partial class GenPage<TModel> :IDisposable, IGenPage<TModel> where TModel : new() 
    {
        [Parameter]
        public GenGrid<TModel> GenGrid { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public ViewState ViewState { get; set; }

        [Parameter]
        public EditMode EditMode { get; set; }

        public TModel OriginalEditItem { get; set; }

        //[Parameter]
        public EventCallback RefreshParentGrid { get; set; }

        [Parameter]
        public TModel SelectedItem { get; set; }
 

        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        public bool EnableModelValidation { get; set; }

        public bool IsTopLevel { get; set; }

        public List<IGenComponent> Components { get; set; }

        [Parameter]
        public EventCallback<IGenView<TModel>> Load { get; set; }


        protected override Task OnInitializedAsync()
        {
            GenGrid.CurrentGenPage = this;

            if (Load.HasDelegate)
                Load.InvokeAsync(this);

            RefreshParentGrid = EventCallback.Factory.Create(this, ()=> GenGrid.RefreshButtonState());

            return base.OnInitializedAsync();
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            GetSubmitTextFromViewState();
            return base.OnAfterRenderAsync(firstRender);
        }

        public async Task<bool> ValidateAsync()
        {
            var result = await GenGrid.ValidateModel();

            StateHasChanged();

            return result;
        }
 
        public async Task OnCommit()
        {
           IsTopLevel = true;
           await  OnCommit(SelectedItem);
        }

        public async Task OnCommit(TModel model)
        {
           await  OnCommit(model, ViewState);
        }

        public  async Task OnCommit(TModel model, ViewState viewState)
        {
            if (!await ValidateAsync()) return;
            
            if (GenGrid.ParentGrid?.ViewState == ViewState.Create)
                await GenGrid.ParentGrid.CurrentGenPage.OnCommitAndWait();

            await GenGrid.OnCommit(SelectedItem, viewState);

            await CloseIfAllowed();
        }

        public async Task OnCommitAndWait()
        {
            await GenGrid.OnCommit(SelectedItem, ViewState.Update);

            ViewState = ViewState.Update;

            StateHasChanged();
        }


        private ValueTask CloseIfAllowed()
        {
            if (IsTopLevel)
                Close();

            return ValueTask.CompletedTask;
        }
        public virtual void Close()
        {
            GenGrid.OriginalTable.RowEditCancel.Invoke(SelectedItem);

            MudDialog.Close();
        }

       

        internal string GetSubmitTextFromViewState()
        {
            return ViewState switch
            {
                ViewState.Create => GenGrid.CreateText,
                ViewState.Update => GenGrid.UpdateText,
                ViewState.Delete => GenGrid.DeleteText,
                _ => ""
            };
        }


        public new void StateHasChanged()
        {
            base.StateHasChanged();
        }

        public TComponent GetComponent<TComponent>(string bindingField) where TComponent : IGenComponent
        {
            return GenGrid.GetComponent<TComponent>(bindingField);
        }
 
        public void Dispose()
        {
            if (GenGrid.ViewState != ViewState.None)
                Close();

            RefreshParentGrid.InvokeAsync();

            MudDialog.Dispose();
        }

       
    }
}
