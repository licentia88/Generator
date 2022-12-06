using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.Components.Components;

public class GenUI : ComponentBase
{
    [CascadingParameter]
    public MudDialogInstance MudDialog
    {
        get; set;
    }
}

