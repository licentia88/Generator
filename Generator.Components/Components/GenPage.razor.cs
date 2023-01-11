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

        public object OriginalEditItem { get; set; }

        [Parameter]
        public EventCallback<GenGridArgs<TModel>> Create { get; set; }

        [Parameter]
        public EventCallback<GenGridArgs<TModel>> Update { get; set; }

        [Parameter]
        public EventCallback<GenGridArgs<TModel>> Delete { get; set; }

        [Parameter]
        public EventCallback<GenGridArgs<TModel>> Cancel { get; set; }


        [Parameter]
        public TModel SelectedItem { get; set; }

        //private TModel selectedItem;

        //[Parameter]
        //public TModel SelectedItem { get => selectedItem;
        //    set
        //    {
        //        selectedItem = value;
        //        StateHasChanged();
        //    }
        //}

        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        public bool EnableModelValidation { get; set; }

        public bool PreventClose { get; set; }

        public List<IGenComponent> Components { get; set; }

        [Parameter]
        public EventCallback<IGenView<TModel>> Load { get; set; }

        protected override Task OnInitializedAsync()
        {
            GenGrid.CurrentGenPage = this;

            if (Load.HasDelegate)
                Load.InvokeAsync(this);

            return base.OnInitializedAsync();
        }

        public async Task OnCommit()
        {
            GenGrid.Components.ForEach(x => x.ValidateObject());

            if (GenGrid.HasErrors()) return;

            if (((dynamic)GenGrid).ParentComponent?.ViewState == ViewState.Create)
            {
                await ((dynamic)GenGrid).ParentComponent.InvokeCallBackFromChild();

                ((dynamic)GenGrid).ParentComponent.SelectedItem = ((dynamic)GenGrid).ParentComponent.CurrentGenPage.SelectedItem;

                //Buradan 
                await OnCommit();
            }
            else
            {
                await GenGrid.InvokeCallBackByViewState(SelectedItem);
            }

            if (!PreventClose)
                Close();
        }

        public virtual void Close()
        {
            MudDialog.Close();
        }

        private string GetSubmitTextFromViewState()
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
