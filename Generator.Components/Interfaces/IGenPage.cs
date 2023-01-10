using Generator.Components.Components;
using MudBlazor;

namespace Generator.Components.Interfaces;

public interface IGenPage<TModel> : IGenView where TModel:new()
{
    public MudDialogInstance MudDialog { get; set; }

    public TModel ViewModel { get; set; }

    public bool EnableModelValidation { get; set; }

    public GenGrid<TModel> GenGrid { get; set; }
}
