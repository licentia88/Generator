using Generator.Components.Extensions;
using System.ComponentModel;
using Generator.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Microsoft.AspNetCore.Components.CompilerServices;

namespace Generator.Components.Components;

public partial class GenFileUpload : MudFileUpload<IBrowserFile>, IGenFileUpload
{
    //private MudFileUpload<IBrowserFile> MudFileUpload { get; set; }
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
 
        FullWidth = true;

        AddComponents();
      
        ErrorText = string.IsNullOrEmpty(ErrorText) ? "*" : ErrorText;

        if (Model is null || Model.GetType().Name == "Object") return;
 
    }
 
    private void AddComponents()
    {
     
        ((IGenComponent)this).Parent?.AddChildComponent(this);
    }
 
public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false, params (string Key, object Value)[] valuePairs) => builder =>
{
    Model = model;

    if (Model is null) { return; }

    builder.OpenComponent<MudFileUpload<IBrowserFile>>(0);

    // Configure the FilesChanged event callback
    OnFilesChanged = EventCallback.Factory.Create(this, (InputFileChangeEventArgs arg) =>
    {
        Parent.StateHasChanged();
        if (Parent is INonGenGrid grid)
            grid.CurrentGenPage?.StateHasChanged();
    });

    // Assign the new FilesChanged event callback
    builder.AddAttribute(1, "FilesChanged", (object)RuntimeHelpers.TypeCheck(EventCallback.Factory.Create(this, FilesChanged)));

    // Iterate through the properties and assign them as attributes
    var i = 999;
    foreach (var param in RenderExtensions.GetPropertyParameters(this, ignoreLabels))
    {
        if (param.Value is null) continue;

        var index = i;
        builder.AddAttribute(index, param.Key, param.Value);
        i++;
    }

    // New ActivatorContent for rendering the upload button
    builder.AddAttribute(2, "ActivatorContent", (RenderFragment)(builder2 =>
    {
        builder2.OpenComponent<MudButton>(3);
        builder2.AddAttribute(4, "FullWidth", true);
        builder2.AddAttribute(5, "HtmlTag", "label");
        builder2.AddAttribute(6, "Variant", RuntimeHelpers.TypeCheck(this.Variant));
        builder2.AddAttribute(7, "Color", RuntimeHelpers.TypeCheck(this.Color));
        builder2.AddAttribute(8, "StartIcon", Icons.Material.Filled.CloudUpload); // Optional: Set an icon
        builder2.AddAttribute(9, "ChildContent", (RenderFragment)(builder3 =>
        {
            builder3.AddContent(10, this.Label); // Use the label property for button content
        }));
        builder2.CloseComponent();
    }));

    // SelectedTemplate for handling the file name or empty state
    builder.AddAttribute(11, "SelectedTemplate", (RenderFragment<IBrowserFile>)(context => builder2 =>
    {
        if (context != null)
        {
            builder2.OpenComponent<MudText>(12);
            builder2.AddAttribute(13, "ChildContent", (RenderFragment)(builder3 =>
            {
                builder3.AddContent(14, context.Name); // Display the file name
            }));
            builder2.CloseComponent();

            // Binding the file name to the model
            Model.SetPropertyValue(BindingField, context.Name);
        }
        else
        {
            builder2.OpenComponent<MudText>(15);
            builder2.AddAttribute(16, "ChildContent", (RenderFragment)(builder3 =>
            {
                builder3.AddContent(17, EmptyText); // Use the EmptyText property when no file is selected
            }));
            builder2.CloseComponent();
        }
    }));

    // Finalize the component rendering
    builder.CloseComponent();
};


   
    // public RenderFragment RenderAsComponent(object model, bool ignoreLabels = false,
    //     params (string Key, object Value)[] valuePairs) => builder =>
    //     {
    //
    //         Model = model;
    //
    //         if (Model is null) { return; }
    //
    //
    //         builder.OpenComponent<MudFileUpload<IBrowserFile>>(0);
    //
    //         OnFilesChanged = EventCallback.Factory.Create(this, (InputFileChangeEventArgs arg) =>
    //         {
    //             Parent.StateHasChanged();
    //             if (Parent is INonGenGrid grid)
    //                 grid.CurrentGenPage?.StateHasChanged();
    //         });
    //         builder.AddAttribute(1, "FilesChanged", (object)RuntimeHelpers.TypeCheck(EventCallback.Factory.Create(this,FilesChanged))); ;
    //
    //         var i = 999;
    //         foreach (var param in RenderExtensions.GetPropertyParameters(this, ignoreLabels))
    //         {
    //             if (param.Value is null) continue;
    //
    //             var index = i;
    //
    //             builder.AddAttribute(index, param.Key, param.Value);
    //
    //             i++;
    //         }
    //         
    //         
    //         builder.AddAttribute(2, "ButtonTemplate", (RenderFragment<FileUploadButtonTemplateContext<IBrowserFile>>)((context) => (__builder2) => {
    //             __builder2.OpenComponent<MudButton>(3);
    //             __builder2.AddAttribute(4, "FullWidth", (object)(RuntimeHelpers.TypeCheck(true)));
    //             __builder2.AddAttribute(5, "HtmlTag", (object)("label"));
    //             __builder2.AddAttribute(6, "Variant", RuntimeHelpers.TypeCheck(this.Variant));
    //             __builder2.AddAttribute(7, "Color", RuntimeHelpers.TypeCheck(this.Color));
    //             __builder2.AddAttribute(8, "for", (object)(context.Id));
    //             __builder2.AddAttribute(9, "ChildContent", (RenderFragment)((__builder3) =>
    //             {
    //                 __builder3.AddContent(10, this.Label);
    //             }
    //             ));
    //             __builder2.CloseComponent();
    //         }
    //         ));
    //         builder.AddAttribute(11, "SelectedTemplate", (RenderFragment<IBrowserFile>)((context) => (__builder2) =>
    //         {
    //
    //             if (context != null)
    //             {
    //
    //                 __builder2.OpenComponent<MudText>(12);
    //                 __builder2.AddAttribute(13, "ChildContent", (RenderFragment)((__builder3) => {
    //                     __builder3.AddContent(14, context.Name);
    //                 }
    //                 ));
    //                 __builder2.CloseComponent();
    //
    //                 Model.SetPropertyValue(BindingField, context.Name);
    //             }
    //             else
    //             {
    //                 __builder2.OpenComponent<MudText>(15);
    //                 __builder2.AddAttribute(16, "ChildContent", (RenderFragment)((__builder3) => {
    //                     __builder3.AddContent(17, EmptyText);
    //
    //                 }
    //                 ));
    //                 __builder2.CloseComponent();
    //             }
    //
    //         }
    //         ));
    //     
    //         builder.CloseComponent();
    //
    //          //builder.RenderComponent(this, ignoreLabels);
    //     };
   
    public RenderFragment RenderAsGridComponent(object model)
    {
        return default;
    }

    [Parameter]
    public Func<object, bool> EditorVisibleIf { get; set; }

    [Parameter]
    public Func<object, bool> DisabledIf { get; set; }

    [CascadingParameter(Name = nameof(IGenComponent.Parent))]
    public IPageBase Parent { get; set; }

    [Parameter]
    public string BindingField { get; set; }
    [Parameter] public int Order { get; set; }
    //[Parameter] public bool EditorEnabled { get; set; } = true;
    [Parameter] public bool EditorVisible { get; set; } = true;
    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public bool GridVisible { get; set; } = false;
    [Parameter] public int xs { get; set; }
    [Parameter] public int sm { get; set; }
    [Parameter] public int md { get; set; }
    [Parameter] public int lg { get; set; }
    [Parameter] public int xl { get; set; }
    [Parameter] public int xxl { get; set; }

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public object Model { get; set; }

    [Parameter, EditorRequired]
    public string Label { get; set; }
 
    [Parameter]
    public string EmptyText { get; set; }

    public IBrowserFile InitialValue { get; set; }

    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public bool FullWidth { get; set; }

    [Parameter]
    public Color Color { get; set; } = Color.Inherit;

    [Parameter]
    public Variant Variant { get; set; }

    public bool IsEditorVisible(object model)
    {
        return ((IGenComponent)this).EditorVisibleIf?.Invoke(model) ?? ((IGenComponent)this).EditorVisible;

    }
 
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Model = null;
            InitialValue = null;
            EditorVisibleIf = null;
            DisabledIf = null;
        }

        base.Dispose(disposing);
    }
}
