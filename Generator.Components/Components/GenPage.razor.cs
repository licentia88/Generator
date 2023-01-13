using Generator.Components.Args;
using Generator.Components.Enums;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Mapster;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.Components.Components
{
    public partial class GenPage<TModel> : IGenPage<TModel> where TModel : new()
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

        [Parameter]
        public EventCallback ChildSubmit { get; set; }

      
        [Parameter]
        public EventCallback ParentSubmit { get; set; }

        [Parameter]
        public EventCallback<TModel> GridSubmit { get; set; }
 

        [Parameter]
        public TModel SelectedItem { get; set; }

        //[Parameter]
        //public EventCallback<TModel> CommitEventCallback { get; set; }

        //[Parameter]
        //public EventCallback CommitParentEventCallback { get; set; }

 

        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        public bool EnableModelValidation { get; set; }

        public bool PreventClose { get; set; }

        public List<IGenComponent> Components { get; set; }

        [Parameter]
        public EventCallback<IGenView<TModel>> Load { get; set; }


        internal ViewState? ParentViewState  => ((dynamic)GenGrid).ParentComponent?.ViewState;

        protected override Task OnInitializedAsync()
        {
            GenGrid.CurrentGenPage = this;

            if (Load.HasDelegate)
                Load.InvokeAsync(this);

            GetSubmitTextFromViewState();
            return base.OnInitializedAsync();
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            GetSubmitTextFromViewState();
            return base.OnAfterRenderAsync(firstRender);
        }

        //public async Task ChildSubmitEvent()
        //{
        //    await GenGrid.InvokeCallBackByViewState(SelectedItem, ViewState.Update);
        //}

        public async Task OnCommit()
        {
            GenGrid.Components.ForEach(x => x.ValidateObject());

            if (GenGrid.HasErrors()) return;

            
            if (ParentViewState is not null && ParentViewState == ViewState.Create)
            {
                await ParentSubmit.InvokeAsync();

                //((dynamic)GenGrid).ParentComponent.CurrentGenPage.StateHasChanged();

                await OnCommit();
            }
            else
            {
                await GridSubmit.InvokeAsync(SelectedItem);
                //await CommitEventCallback.InvokeAsync(SelectedItem);
            }

            if (!PreventClose)
                Close();


        }

        public virtual void Close()
        {
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

            //StateHasChanged();
        }


        public new void StateHasChanged()
        {
            base.StateHasChanged();
        }

        public TComponent GetComponent<TComponent>(string bindingField) where TComponent : IGenComponent
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

    }
}
