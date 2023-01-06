using Generator.Components.Args;
using Generator.Components.Enums;
using Generator.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.Components.Components
{
    public partial class GenPage
    {
        [Parameter]
        public GenGrid GenGrid { get; set; }

        public bool PreventClose { get; set; }

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

        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        public bool EnableModelValidation { get; set; }

        public List<IGenComponent> Components { get; set; }

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

        public async Task OnCommit()
        {
            GenGrid.Components.ForEach(x => x.ValidateObject());

            if (GenGrid.HasErrors()) return;

            if (GenGrid.ParentComponent?.ViewState == ViewState.Create)
            {
                await GenGrid.ParentComponent.InvokeCallBackByViewState(ViewModel);
            }
            else
            {
                await GenGrid.InvokeCallBackByViewState(ViewModel);
            }

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
    }
}
