using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using Generator.Components.Interfaces;
using Generator.Components.Enums;
using Generator.Components.Args;
using Force.DeepCloner;
using MudBlazor;
using Microsoft.AspNetCore.Components.Forms;

namespace Generator.Components.Components;

public partial class GenSinglePage<TModel> : ComponentBase,IDisposable, ISinglePage<TModel> where TModel : class, new()
{
    [Parameter, AllowNull]
    public RenderFragment GenColumns { get; set; }

    [Parameter,EditorRequired]
    public TModel Model { get; set; }


    public List<(Type type, IGenComponent component)> Components { get; set; } = new();

    [Parameter]
    public ViewState ViewState { get; set; }

    [Parameter, AllowNull]
    public RenderFragment<TModel> GenDetailGrid { get; set; }

   
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
        // Initialize the EditContext with your model
        editContext = new EditContext(Model); 

        OriginalModel = Model?.DeepClone();

        base.OnInitialized();
    }

    private bool _isFirstRender = true;

 
    [Parameter]
    public string CancelText { get; set; } = "Cancel";

    [Parameter]
    public string CreateText { get; set; } = "Create";

    [Parameter]
    public string UpdateText { get; set; } = "Update";

    private string SubmitText => ((ISinglePage<TModel>)this).ViewState == ViewState.Create ? CreateText : UpdateText;

  

    [Parameter]
    public GenGrid<TModel> GenGrid { get; set; }

    [Parameter]
    public Dictionary<string, object> Parameters { get; set; } = new();

    [CascadingParameter(Name = nameof(Parameters))]

    private Dictionary<string, object> _Parameters { get => Parameters; set => Parameters = value; }

    bool IPageBase.IsValid { get; set; }

    public bool GridIsBusy { get; set; }

    [Parameter]
    public Color TemplateColor { get; set; }

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


    async Task OnCommit(bool shouldClose = false)
    {
        GridIsBusy = true;
        if (ViewState == ViewState.Create && Create.HasDelegate)
            await Create.InvokeAsync(new GenArgs<TModel>(Model, null));

        if (ViewState == ViewState.Update && Update.HasDelegate)
            await Update.InvokeAsync(new GenArgs<TModel>(Model, OriginalModel));


        GridIsBusy = false;
        if (shouldClose)
           this.Close();
    }

    void Close()
    {
        ((IPageBase)this).Close();
    }
    void IPageBase.Close() => MudDialog.Close();

    void IPageBase.StateHasChanged()
    {
        base.StateHasChanged();
    }

    bool IPageBase.Validate()
    {
        ((IPageBase)this).IsValid = GenGrid.Validate();
        GenGrid.IsValid = ((IPageBase)this).IsValid;
        StateHasChanged();

        return ((IPageBase)this).IsValid;
    }

    async Task IPageBase.OnCommitAndWait()
    {
        if (((IPageBase)this).IsValid)
            await OnCommit();
    }

    object IPageBase.GetSelectedItem()
    {
        return Model;
    }
    
    private void ReleaseUnmanagedResources()
    {
        // TODO release unmanaged resources here
        if (ViewState != ViewState.None)
        {
            Close();
        }
        
        (GenGrid as INonGenGrid).ForceRenderAll();
 
        ((INonGenGrid)GenGrid).ResetValidations(Components.Select(x => x.component as IGenControl));

        OriginalModel = default;
        MudDialog.Dispose();

    }

    protected virtual void Dispose(bool disposing)
    {
        ReleaseUnmanagedResources();
        if (!disposing) return;
        // MudDialog?.Dispose();
        // GenGrid?.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

